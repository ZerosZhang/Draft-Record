using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ICSharpCode.AvalonEdit.Utils
{
    // note: Rope 类的设计目的是在保持高效的同时，提供与传统字符串操作类似的接口。
    // note: 通过将文本分割成多个小块，称之为 RepoNode，并使用树状结构来组织这些节点
    /// <summary>
    /// A kind of List&lt;T&gt;, but more efficient for random insertions/removal.
    /// Also has cheap Clone() and SubRope() implementations.
    /// </summary>
    /// <remarks>
    /// 此类不是线程安全：
    /// 1. 多个并发写操作或与读取同时写入会导致不确定的行为。
    /// 2. 同时读取是安全的。
    /// 3. 但是，即使它们与原始绳索共享数据，绳索的克隆也可以安全地使用。
    /// </remarks>
    [Serializable]
    public sealed class Rope<T> : IList<T>, ICloneable
    {
        internal RopeNode<T> root;

        internal Rope(RopeNode<T> root)
        {
            this.root = root;
            root.CheckInvariants();
        }


        public Rope()
        {
            this.root = RopeNode<T>.emptyRopeNode;
            root.CheckInvariants();
        }

        /// <summary>
        /// 通过特定的输入序列创建一个新的 Rope。
        /// This operation runs in O(N).
        /// </summary>
        /// <exception cref="ArgumentNullException">input is null.</exception>
        public Rope(IEnumerable<T> input)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            Rope<T> inputRope = input as Rope<T>;
            if (inputRope != null)
            {
                // clone ropes instead of copying them
                inputRope.root.Publish();
                this.root = inputRope.root;
            }
            else
            {
                if (input is string text)
                {
                    // if a string is IEnumerable<T>, then T must be char
                    ((Rope<char>)(object)this).root = CharRope.InitFromString(text);
                }
                else
                {
                    T[] arr = ToArray(input);
                    this.root = RopeNode<T>.CreateFromArray(arr, 0, arr.Length);
                }
            }
            this.root.CheckInvariants();
        }

        /// <summary>
        /// Creates a rope from a part of the array.
        /// This operation runs in O(N).
        /// </summary>
        /// <exception cref="ArgumentNullException">input is null.</exception>
        public Rope(T[] array, int arrayIndex, int count)
        {
            VerifyArrayWithRange(array, arrayIndex, count);
            this.root = RopeNode<T>.CreateFromArray(array, arrayIndex, count);
            this.root.CheckInvariants();
        }

        /// <summary>
        /// Creates a new rope that lazily initalizes its content.
        /// </summary>
        /// <param name="length">The length of the rope that will be lazily loaded.</param>
        /// <param name="initializer">
        /// The callback that provides the content for this rope.
        /// <paramref name="initializer"/> will be called exactly once when the content of this rope is first requested.
        /// It must return a rope with the specified length.
        /// Because the initializer function is not called when a rope is cloned, and such clones may be used on another threads,
        /// it is possible for the initializer callback to occur on any thread.
        /// </param>
        /// <remarks>
        /// Any modifications inside the rope will also cause the content to be initialized.
        /// However, insertions at the beginning and the end, as well as inserting this rope into another or
        /// using the <see cref="Concat(Rope{T},Rope{T})"/> method, allows constructions of larger ropes where parts are
        /// lazily loaded.
        /// However, even methods like Concat may sometimes cause the initializer function to be called, e.g. when
        /// two short ropes are concatenated.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public Rope(int length, Func<Rope<T>> initializer)
        {
            if (initializer == null)
                throw new ArgumentNullException("initializer");
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "Length must not be negative");
            if (length == 0)
            {
                this.root = RopeNode<T>.emptyRopeNode;
            }
            else
            {
                this.root = new FunctionNode<T>(length, initializer);
            }
            this.root.CheckInvariants();
        }

        static T[] ToArray(IEnumerable<T> input)
        {
            T[] arr = input as T[];
            return arr ?? input.ToArray();
        }

        /// <summary>
        /// Clones the rope.
        /// This operation runs in linear time to the number of rope nodes touched since the last clone was created.
        /// If you count the per-node cost to the operation modifying the rope (doing this doesn't increase the complexity of the modification operations);
        /// the remainder of Clone() runs in O(1).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public Rope<T> Clone()
        {
            // The Publish() call actually modifies this rope instance; but this modification is thread-safe
            // as long as the tree structure doesn't change during the operation.
            root.Publish();
            return new Rope<T>(root);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        /// <summary>
        /// Resets the rope to an empty list.
        /// Runs in O(1).
        /// </summary>
        public void Clear()
        {
            root = RopeNode<T>.emptyRopeNode;
            OnChanged();
        }

        /// <summary>
        /// Gets the length of the rope.
        /// Runs in O(1).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public int Length
        {
            get { return root.length; }
        }

        /// <summary>
        /// Gets the length of the rope.
        /// Runs in O(1).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public int Count
        {
            get { return root.length; }
        }

        /// <summary>
        /// Inserts another rope into this rope.
        /// Runs in O(lg N + lg M), plus a per-node cost as if <c>newElements.Clone()</c> was called.
        /// </summary>
        /// <exception cref="ArgumentNullException">newElements is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">index or length is outside the valid range.</exception>
        public void InsertRange(int index, Rope<T> newElements)
        {
            if (index < 0 || index > this.Length)
            {
                throw new ArgumentOutOfRangeException("index", index, "0 <= index <= " + this.Length.ToString(CultureInfo.InvariantCulture));
            }
            if (newElements == null)
                throw new ArgumentNullException("newElements");
            newElements.root.Publish();
            root = root.Insert(index, newElements.root);
            OnChanged();
        }

        /// <summary>
        /// Inserts new elements into this rope.
        /// Runs in O(lg N + M), where N is the length of this rope and M is the number of new elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">newElements is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">index or length is outside the valid range.</exception>
        public void InsertRange(int index, IEnumerable<T> newElements)
        {
            if (newElements == null)
                throw new ArgumentNullException("newElements");
            Rope<T> newElementsRope = newElements as Rope<T>;
            if (newElementsRope != null)
            {
                InsertRange(index, newElementsRope);
            }
            else
            {
                T[] arr = ToArray(newElements);
                InsertRange(index, arr, 0, arr.Length);
            }
        }

        /// <summary>
        /// Inserts new elements into this rope.
        /// Runs in O(lg N + M), where N is the length of this rope and M is the number of new elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">newElements is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">index or length is outside the valid range.</exception>
        public void InsertRange(int index, T[] array, int arrayIndex, int count)
        {
            if (index < 0 || index > this.Length)
            {
                throw new ArgumentOutOfRangeException("index", index, "0 <= index <= " + this.Length.ToString(CultureInfo.InvariantCulture));
            }
            VerifyArrayWithRange(array, arrayIndex, count);
            if (count > 0)
            {
                root = root.Insert(index, array, arrayIndex, count);
                OnChanged();
            }
        }

        /// <summary>
        /// Appends multiple elements to the end of this rope.
        /// Runs in O(lg N + M), where N is the length of this rope and M is the number of new elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">newElements is null.</exception>
        public void AddRange(IEnumerable<T> newElements)
        {
            InsertRange(this.Length, newElements);
        }

        /// <summary>
        /// Appends another rope to the end of this rope.
        /// Runs in O(lg N + lg M), plus a per-node cost as if <c>newElements.Clone()</c> was called.
        /// </summary>
        /// <exception cref="ArgumentNullException">newElements is null.</exception>
        public void AddRange(Rope<T> newElements)
        {
            InsertRange(this.Length, newElements);
        }

        /// <summary>
        /// Appends new elements to the end of this rope.
        /// Runs in O(lg N + M), where N is the length of this rope and M is the number of new elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        public void AddRange(T[] array, int arrayIndex, int count)
        {
            InsertRange(this.Length, array, arrayIndex, count);
        }

        /// <summary>
        /// Removes a range of elements from the rope.
        /// Runs in O(lg N).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">offset or length is outside the valid range.</exception>
        public void RemoveRange(int index, int count)
        {
            VerifyRange(index, count);
            if (count > 0)
            {
                root = root.RemoveRange(index, count);
                OnChanged();
            }
        }

        /// <summary>
        /// Copies a range of the specified array into the rope, overwriting existing elements.
        /// Runs in O(lg N + M).
        /// </summary>
        public void SetRange(int index, T[] array, int arrayIndex, int count)
        {
            VerifyRange(index, count);
            VerifyArrayWithRange(array, arrayIndex, count);
            if (count > 0)
            {
                root = root.StoreElements(index, array, arrayIndex, count);
                OnChanged();
            }
        }

        /// <summary>
        /// Creates a new rope and initializes it with a part of this rope.
        /// Runs in O(lg N) plus a per-node cost as if <c>this.Clone()</c> was called.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">offset or length is outside the valid range.</exception>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public Rope<T> GetRange(int index, int count)
        {
            VerifyRange(index, count);
            Rope<T> newRope = Clone();
            int endIndex = index + count;
            newRope.RemoveRange(endIndex, newRope.Length - endIndex);
            newRope.RemoveRange(0, index);
            return newRope;
        }

        /*
		#region Equality
		/// <summary>
		/// Gets whether the two ropes have the same content.
		/// Runs in O(N + M).
		/// </summary>
		/// <remarks>
		/// This method counts as a read access and may be called concurrently to other read accesses.
		/// </remarks>
		public bool Equals(Rope other)
		{
			if (other == null)
				return false;
			// quick detection for ropes that are clones of each other:
			if (other.root == this.root)
				return true;
			if (other.Length != this.Length)
				return false;
			using (RopeTextReader a = new RopeTextReader(this, false)) {
				using (RopeTextReader b = new RopeTextReader(other, false)) {
					int charA, charB;
					do {
						charA = a.Read();
						charB = b.Read();
						if (charA != charB)
							return false;
					} while (charA != -1);
					return true;
				}
			}
		}
		
		/// <summary>
		/// Gets whether two ropes have the same content.
		/// Runs in O(N + M).
		/// </summary>
		public override bool Equals(object obj)
		{
			return Equals(obj as Rope);
		}
		
		/// <summary>
		/// Calculates the hash code of the rope's content.
		/// Runs in O(N).
		/// </summary>
		/// <remarks>
		/// This method counts as a read access and may be called concurrently to other read accesses.
		/// </remarks>
		public override int GetHashCode()
		{
			int hashcode = 0;
			using (RopeTextReader reader = new RopeTextReader(this, false)) {
				unchecked {
					int val;
					while ((val = reader.Read()) != -1) {
						hashcode = hashcode * 31 + val;
					}
				}
			}
			return hashcode;
		}
		#endregion
		 */

        /// <summary>
        /// Concatenates two ropes. The input ropes are not modified.
        /// Runs in O(lg N + lg M).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static Rope<T> Concat(Rope<T> left, Rope<T> right)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");
            left.root.Publish();
            right.root.Publish();
            return new Rope<T>(RopeNode<T>.Concat(left.root, right.root));
        }

        /// <summary>
        /// Concatenates multiple ropes. The input ropes are not modified.
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static Rope<T> Concat(params Rope<T>[] ropes)
        {
            if (ropes == null)
                throw new ArgumentNullException("ropes");
            Rope<T> result = new Rope<T>();
            foreach (Rope<T> r in ropes)
                result.AddRange(r);
            return result;
        }

        /// <summary>
        /// RopeCacheEntry 是一个内部结构体，用于在 Rope 类中缓存节点信息。
        /// 它的主要作用是提高对 Rope 数据结构的访问效率，特别是在频繁访问相邻位置的情况下。
        /// </summary>
        internal struct RopeCacheEntry
        {
            /// <summary>
            /// 指向 RopeNode<T> 类型的节点
            /// </summary>
            internal readonly RopeNode<T> node;
            /// <summary>
            /// 表示该节点在 Rope 中的起始索引。
            /// </summary>
            internal readonly int nodeStartIndex;


            // RopeCacheEntry 结构体的主要用途是在 Rope 类的 FindNodeUsingCache 方法中。
            // 这个方法用于查找包含指定索引的节点，并返回一个 ImmutableStack<RopeCacheEntry>，
            // 其中包含从根节点到目标节点的路径。
            // 通过缓存最近访问的节点信息，FindNodeUsingCache 方法可以避免在每次访问时都从根节点开始遍历，从而提高访问效率。
            internal RopeCacheEntry(RopeNode<T> node, int nodeStartOffset)
            {
                this.node = node;
                this.nodeStartIndex = nodeStartOffset;
            }

            /// <summary>
            /// 用于判断指定的索引是否在当前节点的范围内。
            /// 这个方法在 FindNodeUsingCache 方法中用于确定是否需要继续向下遍历。
            /// </summary>
            /// <param name="offset"></param>
            /// <returns></returns>
            internal bool IsInside(int offset)
            {
                return offset >= nodeStartIndex && offset < nodeStartIndex + node.length;
            }
        }

        // 这个变量的主要作用是提高对 Rope 数据结构的访问效率，特别是在频繁访问相邻位置的情况下。
        // 1. 缓存最近访问的节点信息
        // 2. 提高访问效率
        // 3. 支持并发访问
        // 4. 与 FindNodeUsingCache 方法配合使用
        // cached pointer to 'last used node', used to speed up accesses by index that are close together
        // 缓存指向“最后使用的节点”的指针，用于加速通过靠近的索引的访问
        [NonSerialized]
        volatile ImmutableStack<RopeCacheEntry> lastUsedNodeStack;

        internal void OnChanged()
        {
            lastUsedNodeStack = null;

            root.CheckInvariants();
        }

        /// <summary>
        /// 获取/设置单个字符。
        /// Gets/Sets a single character.
        /// Runs in O(lg N) for random access. Sequential read-only access benefits from a special optimization and runs in amortized O(1).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Offset is outside the valid range (0 to Length-1).</exception>
        /// <remarks>
        /// The getter counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public T this[int index]
        {
            get
            {
                // use unsigned integers - this way negative values for index overflow and can be tested for with the same check
                if (unchecked((uint)index >= (uint)this.Length))
                {
                    throw new ArgumentOutOfRangeException("index", index, "0 <= index < " + this.Length.ToString(CultureInfo.InvariantCulture));
                }
                RopeCacheEntry entry = FindNodeUsingCache(index).PeekOrDefault();
                return entry.node.contents[index - entry.nodeStartIndex];
            }
            set
            {
                if (index < 0 || index >= this.Length)
                {
                    throw new ArgumentOutOfRangeException("index", index, "0 <= index < " + this.Length.ToString(CultureInfo.InvariantCulture));
                }
                root = root.SetElement(index, value);
                OnChanged();

                // 这里尝试使用缓存的节点堆栈来实现设置器（未经测试的代码！）。
                // 但是我不使用该代码，因为它很复杂并且无法与更改通知正确集成。
                // 相反，我将使用更容易理解的递归解决方案。
                // 哦，而且它也无法正确处理功能节点。
                /* Here's a try at implementing the setter using the cached node stack (UNTESTED code!).
				 * However I don't use the code because it's complicated and doesn't integrate correctly with change notifications.
				 * Instead, I'll use the much easier to understand recursive solution.
				 * Oh, and it also doesn't work correctly with function nodes.
				ImmutableStack<RopeCacheEntry> nodeStack = FindNodeUsingCache(offset);
				RopeCacheEntry entry = nodeStack.Peek();
				if (!entry.node.isShared) {
					entry.node.contents[offset - entry.nodeStartOffset] = value;
					// missing: clear the caches except for the node stack cache (e.g. ToString() cache?)
				} else {
					RopeNode oldNode = entry.node;
					RopeNode newNode = oldNode.Clone();
					newNode.contents[offset - entry.nodeStartOffset] = value;
					for (nodeStack = nodeStack.Pop(); !nodeStack.IsEmpty; nodeStack = nodeStack.Pop()) {
						RopeNode parentNode = nodeStack.Peek().node;
						RopeNode newParentNode = parentNode.CloneIfShared();
						if (newParentNode.left == oldNode) {
							newParentNode.left = newNode;
						} else {
							Debug.Assert(newParentNode.right == oldNode);
							newParentNode.right = newNode;
						}
						if (parentNode == newParentNode) {
							// we were able to change the existing node (it was not shared);
							// there's no reason to go further upwards
							ClearCacheOnModification();
							return;
						} else {
							oldNode = parentNode;
							newNode = newParentNode;
						}
					}
					// we reached the root of the rope.
					Debug.Assert(root == oldNode);
					root = newNode;
					ClearCacheOnModification();
				}*/
            }
        }

        /// <summary>
        /// 在 Rope 数据结构中查找包含指定索引的节点，并返回一个 ImmutableStack，
        /// 其中包含从根节点到目标节点的路径。
        /// </summary>
        /// <remarks>
        /// 1. 初始化：如果 lastUsedNodeStack 为空，则创建一个新的 ImmutableStack，
        ///           并将根节点作为第一个元素压入栈中。
        /// 2. 查找节点：从 lastUsedNodeStack 中弹出栈顶元素，检查该节点是否包含指定索引。
        ///             如果不包含，则继续弹出栈顶元素，直到找到包含指定索引的节点。
        /// 3. 向下遍历：如果找到的节点是叶子节点或函数节点，则直接返回该节点。
        ///             否则，根据指定索引与当前节点的起始索引的关系，选择左子节点或右子节点，
        ///             并将其压入栈中，然后继续向下遍历。
        /// 4. 更新缓存：在遍历过程中，如果 lastUsedNodeStack 发生了变化，
        ///             则将新的栈赋值给 lastUsedNodeStack，以便下次访问时可以使用缓存。
        /// </remarks>
        /// <param name="index"></param>
        /// <returns></returns>
        internal ImmutableStack<RopeCacheEntry> FindNodeUsingCache(int index)
        {
            Debug.Assert(index >= 0 && index < this.Length);

            // thread safety: fetch stack into local variable
            ImmutableStack<RopeCacheEntry> stack = lastUsedNodeStack;
            ImmutableStack<RopeCacheEntry> oldStack = stack;

            if (stack == null)
            {
                stack = ImmutableStack<RopeCacheEntry>.Empty.Push(new RopeCacheEntry(root, 0));
            }
            while (!stack.PeekOrDefault().IsInside(index))
                stack = stack.Pop();
            while (true)
            {
                RopeCacheEntry entry = stack.PeekOrDefault();
                // check if we've reached a leaf or function node
                if (entry.node.height == 0)
                {
                    if (entry.node.contents == null)
                    {
                        // this is a function node - go down into its subtree
                        entry = new RopeCacheEntry(entry.node.GetContentNode(), entry.nodeStartIndex);
                        // entry is now guaranteed NOT to be another function node
                    }
                    if (entry.node.contents != null)
                    {
                        // this is a node containing actual content, so we're done
                        break;
                    }
                }
                // go down towards leaves
                if (index - entry.nodeStartIndex >= entry.node.left.length)
                    stack = stack.Push(new RopeCacheEntry(entry.node.right, entry.nodeStartIndex + entry.node.left.length));
                else
                    stack = stack.Push(new RopeCacheEntry(entry.node.left, entry.nodeStartIndex));
            }

            // write back stack to volatile cache variable
            // (in multithreaded access, it doesn't matter which of the threads wins - it's just a cache)
            if (oldStack != stack)
            {
                // no need to write when we the cache variable didn't change
                lastUsedNodeStack = stack;
            }

            // this method guarantees that it finds a leaf node
            Debug.Assert(stack.Peek().node.contents != null);
            return stack;
        }

        /// <summary>
        /// 验证给定的起始索引 startIndex 和长度 length 是否在有效范围内。
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal void VerifyRange(int startIndex, int length)
        {
            if (startIndex < 0 || startIndex > this.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "0 <= startIndex <= " + this.Length.ToString(CultureInfo.InvariantCulture));
            }
            if (length < 0 || startIndex + length > this.Length)
            {
                throw new ArgumentOutOfRangeException("length", length, "0 <= length, startIndex(" + startIndex + ")+length <= " + this.Length.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// 验证给定的数组 array、起始索引 arrayIndex 和长度 count 是否在有效范围内。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        /// <param name="count"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal static void VerifyArrayWithRange(T[] array, int arrayIndex, int count)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex", arrayIndex, "0 <= arrayIndex <= " + array.Length.ToString(CultureInfo.InvariantCulture));
            }
            if (count < 0 || arrayIndex + count > array.Length)
            {
                throw new ArgumentOutOfRangeException("count", count, "0 <= length, arrayIndex(" + arrayIndex + ")+count <= " + array.Length.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Creates a string from the rope. Runs in O(N).
        /// </summary>
        /// <returns>A string consisting of all elements in the rope as comma-separated list in {}.
        /// As a special case, Rope&lt;char&gt; will return its contents as string without any additional separators or braces,
        /// so it can be used like StringBuilder.ToString().</returns>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public override string ToString()
        {
            Rope<char> charRope = this as Rope<char>;
            if (charRope != null)
            {
                return charRope.ToString(0, this.Length);
            }
            else
            {
                StringBuilder b = new StringBuilder();
                foreach (T element in this)
                {
                    if (b.Length == 0)
                        b.Append('{');
                    else
                        b.Append(", ");
                    b.Append(element.ToString());
                }
                b.Append('}');
                return b.ToString();
            }
        }

        internal string GetTreeAsString()
        {
#if DEBUG
            return root.GetTreeAsString();
#else
			return "Not available in release build.";
#endif
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Finds the first occurrence of item.
        /// Runs in O(N).
        /// </summary>
        /// <returns>The index of the first occurrence of item, or -1 if it cannot be found.</returns>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public int IndexOf(T item)
        {
            return IndexOf(item, 0, this.Length);
        }

        /// <summary>
        /// Gets the index of the first occurrence the specified item.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <param name="startIndex">Start index of the search.</param>
        /// <param name="count">Length of the area to search.</param>
        /// <returns>The first index where the item was found; or -1 if no occurrence was found.</returns>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public int IndexOf(T item, int startIndex, int count)
        {
            VerifyRange(startIndex, count);

            while (count > 0)
            {
                var entry = FindNodeUsingCache(startIndex).PeekOrDefault();
                T[] contents = entry.node.contents;
                int startWithinNode = startIndex - entry.nodeStartIndex;
                int nodeLength = Math.Min(entry.node.length, startWithinNode + count);
                int r = Array.IndexOf(contents, item, startWithinNode, nodeLength - startWithinNode);
                if (r >= 0)
                    return entry.nodeStartIndex + r;
                count -= nodeLength - startWithinNode;
                startIndex = entry.nodeStartIndex + nodeLength;
            }
            return -1;
        }

        /// <summary>
        /// Gets the index of the last occurrence of the specified item in this rope.
        /// </summary>
        public int LastIndexOf(T item)
        {
            return LastIndexOf(item, 0, this.Length);
        }

        /// <summary>
        /// Gets the index of the last occurrence of the specified item in this rope.
        /// </summary>
        /// <param name="item">The search item</param>
        /// <param name="startIndex">Start index of the area to search.</param>
        /// <param name="count">Length of the area to search.</param>
        /// <returns>The last index where the item was found; or -1 if no occurrence was found.</returns>
        /// <remarks>The search proceeds backwards from (startIndex+count) to startIndex.
        /// This is different than the meaning of the parameters on Array.LastIndexOf!</remarks>
        public int LastIndexOf(T item, int startIndex, int count)
        {
            VerifyRange(startIndex, count);

            var comparer = EqualityComparer<T>.Default;
            for (int i = startIndex + count - 1; i >= startIndex; i--)
            {
                if (comparer.Equals(this[i], item))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Inserts the item at the specified index in the rope.
        /// Runs in O(lg N).
        /// </summary>
        public void Insert(int index, T item)
        {
            InsertRange(index, new[] { item }, 0, 1);
        }

        /// <summary>
        /// Removes a single item from the rope.
        /// Runs in O(lg N).
        /// </summary>
        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);
        }

        /// <summary>
        /// Appends the item at the end of the rope.
        /// Runs in O(lg N).
        /// </summary>
        public void Add(T item)
        {
            InsertRange(this.Length, new[] { item }, 0, 1);
        }

        /// <summary>
        /// Searches the item in the rope.
        /// Runs in O(N).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        /// <summary>
        /// Copies the whole content of the rope into the specified array.
        /// Runs in O(N).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(0, array, arrayIndex, this.Length);
        }

        /// <summary>
        /// Copies the a part of the rope into the specified array.
        /// Runs in O(lg N + M).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            VerifyRange(index, count);
            VerifyArrayWithRange(array, arrayIndex, count);
            this.root.CopyTo(index, array, arrayIndex, count);
        }

        /// <summary>
        /// Removes the first occurrence of an item from the rope.
        /// Runs in O(N).
        /// </summary>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves an enumerator to iterate through the rope.
        /// The enumerator will reflect the state of the rope from the GetEnumerator() call, further modifications
        /// to the rope will not be visible to the enumerator.
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public IEnumerator<T> GetEnumerator()
        {
            this.root.Publish();
            return Enumerate(root);
        }

        /// <summary>
        /// Creates an array and copies the contents of the rope into it.
        /// Runs in O(N).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public T[] ToArray()
        {
            T[] arr = new T[this.Length];
            this.root.CopyTo(0, arr, 0, arr.Length);
            return arr;
        }

        /// <summary>
        /// Creates an array and copies the contents of the rope into it.
        /// Runs in O(N).
        /// </summary>
        /// <remarks>
        /// This method counts as a read access and may be called concurrently to other read accesses.
        /// </remarks>
        public T[] ToArray(int startIndex, int count)
        {
            VerifyRange(startIndex, count);
            T[] arr = new T[count];
            CopyTo(startIndex, arr, 0, count);
            return arr;
        }

        static IEnumerator<T> Enumerate(RopeNode<T> node)
        {
            Stack<RopeNode<T>> stack = new Stack<RopeNode<T>>();
            while (node != null)
            {
                // go to leftmost node, pushing the right parts that we'll have to visit later
                while (node.contents == null)
                {
                    if (node.height == 0)
                    {
                        // go down into function nodes
                        node = node.GetContentNode();
                        continue;
                    }
                    Debug.Assert(node.right != null);
                    stack.Push(node.right);
                    node = node.left;
                }
                // yield contents of leaf node
                for (int i = 0; i < node.length; i++)
                {
                    yield return node.contents[i];
                }
                // go up to the next node not visited yet
                if (stack.Count > 0)
                    node = stack.Pop();
                else
                    node = null;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
