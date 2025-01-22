using System;
using System.Diagnostics;

using System.Text;

namespace ICSharpCode.AvalonEdit.Utils;

// Class used to represent a node in the tree.
// There are three types of nodes:
// Concat nodes: height>0, left!=null, right!=null, contents==null
// Leaf nodes: height==0, left==null, right==null, contents!=null
// Function nodes: height==0, left==null, right==null, contents==null, are of type FunctionNode<T>

// 用来表示树中节点的类。
// 有三种类型的节点：
// 连接节点：高度 > 0，左 != null,右 != null, contents == null 
// 叶节点：高度 == 0，左 == null，右 == null, contents != null 
// 功能节点：高度 == 0，左 == null，右 == null，内容 == null，类型为 FunctionNode

[Serializable]
class RopeNode<T>
{
    internal const int NodeSize = 256;

    internal static readonly RopeNode<T> emptyRopeNode = new RopeNode<T> { isShared = true, contents = new T[RopeNode<T>.NodeSize] };

    // Fields for pointers to sub-nodes. Only non-null for concat nodes (height>=1)
    // 指向子节点的指针字段。只有连接节点非空（高度>=1）
    internal RopeNode<T> left, right;

    // specifies whether this node is shared between multiple ropes 
    //指定该节点是否在多个rope之间共享，
    internal volatile bool isShared;

    //the total length of all text in this subtree
    // 此子树中所有文本的总长度
    internal int length;

    // the height of this subtree: 0 for leaf nodes; 1+max(left.height,right.height) for concat nodes
    // 这个子树的高度：对于叶节点为0；1+max（left.height,right.height）用于连接节点
    internal byte height;

    // The character data. Only non-null for leaf nodes (height=0) that aren't function nodes.
    // 字符数据。只有非功能节点的叶节点（高度=0）为非空。
    internal T[] contents;

    /// <summary>
    /// 右边减去左边的高度
    /// </summary>
    internal int Balance
    {
        get { return right.height - left.height; }
    }

    // note: 校验数据
    /// <summary>
    /// CheckInvariants 函数的作用是检查 RopeNode 类的内部状态是否满足一些不变量（invariants）
    /// 不变量是指在程序执行过程中，某些条件始终保持为真的属性。
    /// 在这个特定的上下文中，CheckInvariants 函数用于验证 RopeNode 对象的结构和属性是否符合预期，以确保数据的一致性和正确性。
    /// </summary>
    [Conditional("DATACONSISTENCYTEST")] // 数据一致性检验
    internal void CheckInvariants()
    {
        if (height == 0)
        {
            Debug.Assert(left == null && right == null);
            if (contents == null)
            {
                Debug.Assert(this is FunctionNode<T>);
                Debug.Assert(length > 0);
                Debug.Assert(isShared);
            }
            else
            {
                Debug.Assert(contents != null && contents.Length == NodeSize);
                Debug.Assert(length >= 0 && length <= NodeSize);
            }
        }
        else
        {
            Debug.Assert(left != null && right != null);
            Debug.Assert(contents == null);
            Debug.Assert(length == left.length + right.length);
            Debug.Assert(height == 1 + Math.Max(left.height, right.height));
            Debug.Assert(Math.Abs(this.Balance) <= 1);

            // this is an additional invariant that forces the tree to combine small leafs to prevent excessive memory usage:
            Debug.Assert(length > NodeSize);
            // note that this invariant ensures that all nodes except for the empty rope's single node have at least length 1

            if (isShared)
                Debug.Assert(left.isShared && right.isShared);
            left.CheckInvariants();
            right.CheckInvariants();
        }
    }

    internal RopeNode<T> Clone()
    {
        if (height == 0)
        {
            // 如果一个函数节点需要克隆，我们将对其求值。
            // If a function node needs cloning, we'll evaluate it.
            if (contents == null)
                return GetContentNode().Clone();
            T[] newContents = new T[NodeSize];
            contents.CopyTo(newContents, 0);
            return new RopeNode<T>
            {
                length = this.length,
                contents = newContents
            };
        }
        else
        {
            return new RopeNode<T>
            {
                left = this.left,
                right = this.right,
                length = this.length,
                height = this.height
            };
        }
    }

    internal RopeNode<T> CloneIfShared()
    {
        return isShared ? Clone() : this;
    }

    /// <summary>
    /// 发布？公开？
    /// </summary>
    internal void Publish()
    {
        if (!isShared)
        {
            left?.Publish();
            right?.Publish();
            isShared = true;
        }
    }

    /// <summary>
    /// 通过数组创建一个 RopeNode<T> 对象
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    internal static RopeNode<T> CreateFromArray(T[] arr, int index, int length)
    {
        if (length == 0)
        {
            return emptyRopeNode;
        }
        RopeNode<T> node = CreateNodes(length);
        return node.StoreElements(0, arr, index, length);
    }

    /// <summary>
    /// 创建节点
    /// </summary>
    /// <param name="totalLength"></param>
    /// <returns></returns>
    internal static RopeNode<T> CreateNodes(int totalLength)
    {
        // 这里可能是这意思：
        // 如果totalLength = 0，leafCount = 0；
        // 如果 totalLength 小于等于 NodeSize，那么leafCount就是1，
        // 否则就是totalLength / NodeSize
        int leafCount = (totalLength + NodeSize - 1) / NodeSize;
        return CreateNodes(leafCount, totalLength);
    }

    static RopeNode<T> CreateNodes(int leafCount, int totalLength)
    {
        Debug.Assert(leafCount > 0);
        Debug.Assert(totalLength > 0);
        RopeNode<T> result = new RopeNode<T>();
        result.length = totalLength;
        if (leafCount == 1)
        {
            result.contents = new T[NodeSize];
        }
        else
        {
            int rightSide = leafCount / 2;
            int leftSide = leafCount - rightSide;
            int leftLength = leftSide * NodeSize;
            result.left = CreateNodes(leftSide, leftLength);
            result.right = CreateNodes(rightSide, totalLength - leftLength);
            result.height = (byte)(1 + Math.Max(result.left.height, result.right.height));
        }
        return result;
    }

    /// <summary>
    /// Balances this node and recomputes the 'height' field.
    /// This method assumes that the children of this node are already balanced and have an up-to-date 'height' value.
    /// 平衡该节点并重新计算‘height’字段。
    /// 此方法假设该节点的子节点已经平衡并且具有最新的‘height’值。
    /// </summary>
    internal void Rebalance()
    {
        // Rebalance() shouldn't be called on shared nodes - it's only called after modifications!
        // Rebalance（）不应该在共享节点上被调用——它只会在修改后被调用！
        Debug.Assert(!isShared);
        // leaf nodes are always balanced (we don't use 'height' to detect leaf nodes here
        // because Balance is supposed to recompute the height).
        // 叶节点总是平衡的（这里我们不使用‘height’来检测叶节点，因为Balance应该重新计算高度)
        if (left == null)
            return;

        // ensure we didn't miss a MergeIfPossible step
        // 确保我们没有错过一个 MergeIfPossible 步骤
        Debug.Assert(this.length > NodeSize);

        // We need to loop until it's balanced. Rotations might cause two small leaves to combine to a larger one,
        // which changes the height and might mean we need additional balancing steps.
        // 我们需要循环，直到它达到平衡。旋转可能会使两个小叶子结合成一个大叶子，
        // 它改变了高度，可能意味着我们需要额外的平衡步骤。
        while (Math.Abs(this.Balance) > 1)
        {
            // AVL balancing
            // note: because we don't care about the identity of concat nodes, this works a little different than usual
            // tree rotations: in our implementation, the "this" node will stay at the top, only its children are rearranged
            // 注意：因为我们不关心连接节点的身份，所以这与通常的工作方式略有不同
            // 树的旋转：在我们的实现中，“this”节点将保持在顶部，只有它的子节点被重新排列
            if (this.Balance > 1)
            {
                if (right.Balance < 0)
                {
                    right = right.CloneIfShared();
                    right.RotateRight();
                }
                this.RotateLeft();
                // 如果‘this’的不平衡值大于2，我们将把一些不平衡值移到左节点；所以要重新平衡。
                // If 'this' was unbalanced by more than 2, we've shifted some of the inbalance to the left node; so rebalance that.
                this.left.Rebalance();
            }
            else if (this.Balance < -1)
            {
                if (left.Balance > 0)
                {
                    left = left.CloneIfShared();
                    left.RotateLeft();
                }
                this.RotateRight();
                // If 'this' was unbalanced by more than 2, we've shifted some of the inbalance to the right node; so rebalance that.
                this.right.Rebalance();
            }
        }

        Debug.Assert(Math.Abs(this.Balance) <= 1);
        this.height = (byte)(1 + Math.Max(left.height, right.height));
    }

    void RotateLeft()
    {
        Debug.Assert(!isShared);

        /* Rotate tree to the left
			 * 
			 *       this               this
			 *       /  \               /  \
			 *      A   right   ===>  left  C
			 *           / \          / \
			 *          B   C        A   B
			 */
        RopeNode<T> a = left;
        RopeNode<T> b = right.left;
        RopeNode<T> c = right.right;
        // reuse right concat node, if possible
        this.left = right.isShared ? new RopeNode<T>() : right;
        this.left.left = a;
        this.left.right = b;
        this.left.length = a.length + b.length;
        this.left.height = (byte)(1 + Math.Max(a.height, b.height));
        this.right = c;

        this.left.MergeIfPossible();
    }

    void RotateRight()
    {
        Debug.Assert(!isShared);

        /* Rotate tree to the right
			 * 
			 *       this             this
			 *       /  \             /  \
			 *     left  C   ===>    A  right
			 *     / \                   /  \
			 *    A   B                 B    C
			 */
        RopeNode<T> a = left.left;
        RopeNode<T> b = left.right;
        RopeNode<T> c = right;
        // reuse left concat node, if possible
        this.right = left.isShared ? new RopeNode<T>() : left;
        this.right.left = b;
        this.right.right = c;
        this.right.length = b.length + c.length;
        this.right.height = (byte)(1 + Math.Max(b.height, c.height));
        this.left = a;

        this.right.MergeIfPossible();
    }

    void MergeIfPossible()
    {
        Debug.Assert(!isShared);

        if (this.length <= NodeSize)
        {
            // Convert this concat node to leaf node.
            // We know left and right cannot be concat nodes (they would have merged already),
            // but they could be function nodes.
            // 将此连接节点转换为叶节点。
            // 我们知道左和右不能是连接节点（它们已经合并了），但它们可以是功能节点。
            this.height = 0;
            int lengthOnLeftSide = this.left.length;
            if (this.left.isShared)
            {
                this.contents = new T[NodeSize];
                left.CopyTo(0, this.contents, 0, lengthOnLeftSide);
            }
            else
            {
                // must be a leaf node: function nodes are always marked shared
                // 必须是叶节点：功能节点总是被标记为共享
                Debug.Assert(this.left.contents != null);
                // steal buffer from left side
                // 从左侧窃取缓冲区
                this.contents = this.left.contents;
#if DEBUG
                // In debug builds, explicitly mark left node as 'damaged' - but no one else should be using it because it's not shared.
                // 在调试版本中，显式地将左节点标记为‘已损坏’ -但其他人不应该使用它，因为它不是共享的。
                this.left.contents = Empty<T>.Array;
#endif
            }
            this.left = null;
            right.CopyTo(0, this.contents, lengthOnLeftSide, this.right.length);
            this.right = null;
        }
    }

    /// <summary>
    /// Copies from the array to this node.
    /// 从数组复制到该节点。Array To Rope
    /// </summary>
    internal RopeNode<T> StoreElements(int index, T[] array, int arrayIndex, int count)
    {
        RopeNode<T> result = this.CloneIfShared();
        // result cannot be function node after a call to Clone()
        if (result.height == 0)
        {
            // leaf node:
            Array.Copy(array, arrayIndex, result.contents, index, count);
        }
        else
        {
            // concat node:
            if (index + count <= result.left.length)
            {
                result.left = result.left.StoreElements(index, array, arrayIndex, count);
            }
            else if (index >= this.left.length)
            {
                result.right = result.right.StoreElements(index - result.left.length, array, arrayIndex, count);
            }
            else
            {
                int amountInLeft = result.left.length - index;
                result.left = result.left.StoreElements(index, array, arrayIndex, amountInLeft);
                result.right = result.right.StoreElements(0, array, arrayIndex + amountInLeft, count - amountInLeft);
            }
            result.Rebalance(); // tree layout might have changed if function nodes were replaced with their content
        }
        return result;
    }

    /// <summary>
    /// Copies from this node to the array.
    /// 从该节点复制到数组。Rope To Array
    /// </summary>
    internal void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
        if (height == 0)
        {
            if (this.contents == null)
            {
                // function node
                this.GetContentNode().CopyTo(index, array, arrayIndex, count);
            }
            else
            {
                // leaf node
                Array.Copy(this.contents, index, array, arrayIndex, count);
            }
        }
        else
        {
            // concat node
            if (index + count <= this.left.length)
            {
                this.left.CopyTo(index, array, arrayIndex, count);
            }
            else if (index >= this.left.length)
            {
                this.right.CopyTo(index - this.left.length, array, arrayIndex, count);
            }
            else
            {
                int amountInLeft = this.left.length - index;
                this.left.CopyTo(index, array, arrayIndex, amountInLeft);
                this.right.CopyTo(0, array, arrayIndex + amountInLeft, count - amountInLeft);
            }
        }
    }

    /// <summary>
    /// 设置 <see href="RopeNode"/> 对象的元素
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    internal RopeNode<T> SetElement(int offset, T value)
    {
        RopeNode<T> result = CloneIfShared();
        // result of CloneIfShared() is leaf or concat node
        if (result.height == 0)
        {
            result.contents[offset] = value;
        }
        else
        {
            if (offset < result.left.length)
            {
                result.left = result.left.SetElement(offset, value);
            }
            else
            {
                result.right = result.right.SetElement(offset - result.left.length, value);
            }
            // tree layout might have changed if function nodes were replaced with their content
            // 如果函数节点被替换为它们的内容，树的布局可能会改变
            result.Rebalance();
        }
        return result;
    }

    /// <summary>
    /// 合并两个 RopeNode<T> 对象
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    internal static RopeNode<T> Concat(RopeNode<T> left, RopeNode<T> right)
    {
        if (left.length == 0)
            return right;
        if (right.length == 0)
            return left;

        if (left.length + right.length <= NodeSize)
        {
            left = left.CloneIfShared();
            // left is guaranteed to be leaf node after cloning:
            // - it cannot be function node (due to clone)
            // - it cannot be concat node (too short)
            right.CopyTo(0, left.contents, left.length, right.length);
            left.length += right.length;
            return left;
        }
        else
        {
            RopeNode<T> concatNode = new RopeNode<T>();
            concatNode.left = left;
            concatNode.right = right;
            concatNode.length = left.length + right.length;
            concatNode.Rebalance();
            return concatNode;
        }
    }

    /// <summary>
    /// Splits this leaf node at offset and returns a new node with the part of the text after offset.
    /// 在offset处拆分这个叶节点，并返回一个新的节点，包含 offset 后的部分文本。
    /// </summary>
    RopeNode<T> SplitAfter(int offset)
    {
        Debug.Assert(!isShared && height == 0 && contents != null);
        RopeNode<T> newPart = new RopeNode<T>();
        newPart.contents = new T[NodeSize];
        newPart.length = this.length - offset;
        Array.Copy(this.contents, offset, newPart.contents, 0, newPart.length);
        this.length = offset;
        return newPart;
    }

    /// <summary>
    /// 将 RopeNode 对象插入到指定的位置
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="newElements"></param>
    /// <returns></returns>
    internal RopeNode<T> Insert(int offset, RopeNode<T> newElements)
    {
        if (offset == 0)
        {
            return Concat(newElements, this);
        }
        else if (offset == this.length)
        {
            return Concat(this, newElements);
        }

        // first clone this node (converts function nodes to leaf or concat nodes)
        RopeNode<T> result = CloneIfShared();
        if (result.height == 0)
        {
            // leaf node: we'll need to split this node
            RopeNode<T> left = result;
            RopeNode<T> right = left.SplitAfter(offset);
            return Concat(Concat(left, newElements), right);
        }
        else
        {
            // concat node
            if (offset < result.left.length)
            {
                result.left = result.left.Insert(offset, newElements);
            }
            else
            {
                result.right = result.right.Insert(offset - result.left.length, newElements);
            }
            result.length += newElements.length;
            result.Rebalance();
            return result;
        }
    }

    /// <summary>
    /// 将 Array 插入到指定的位置
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    internal RopeNode<T> Insert(int offset, T[] array, int arrayIndex, int count)
    {
        Debug.Assert(count > 0);

        if (this.length + count < RopeNode<char>.NodeSize)
        {
            RopeNode<T> result = CloneIfShared();
            // result must be leaf node (Clone never returns function nodes, too short for concat node)
            int lengthAfterOffset = result.length - offset;
            T[] resultContents = result.contents;
            for (int i = lengthAfterOffset; i >= 0; i--)
            {
                resultContents[i + offset + count] = resultContents[i + offset];
            }
            Array.Copy(array, arrayIndex, resultContents, offset, count);
            result.length += count;
            return result;
        }
        else if (height == 0)
        {
            // TODO: implement this more efficiently?
            return Insert(offset, CreateFromArray(array, arrayIndex, count));
        }
        else
        {
            // this is a concat node (both leafs and function nodes are handled by the case above)
            RopeNode<T> result = CloneIfShared();
            if (offset < result.left.length)
            {
                result.left = result.left.Insert(offset, array, arrayIndex, count);
            }
            else
            {
                result.right = result.right.Insert(offset - result.left.length, array, arrayIndex, count);
            }
            result.length += count;
            result.Rebalance();
            return result;
        }
    }

    /// <summary>
    /// 移除 RopeNode 的部分对象
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    internal RopeNode<T> RemoveRange(int index, int count)
    {
        Debug.Assert(count > 0);

        // produce empty node when one node is deleted completely
        if (index == 0 && count == this.length)
            return emptyRopeNode;

        int endIndex = index + count;
        RopeNode<T> result = CloneIfShared(); // convert function node to concat/leaf
        if (result.height == 0)
        {
            int remainingAfterEnd = result.length - endIndex;
            for (int i = 0; i < remainingAfterEnd; i++)
            {
                result.contents[index + i] = result.contents[endIndex + i];
            }
            result.length -= count;
        }
        else
        {
            if (endIndex <= result.left.length)
            {
                // deletion is only within the left part
                result.left = result.left.RemoveRange(index, count);
            }
            else if (index >= result.left.length)
            {
                // deletion is only within the right part
                result.right = result.right.RemoveRange(index - result.left.length, count);
            }
            else
            {
                // deletion overlaps both parts
                int deletionAmountOnLeftSide = result.left.length - index;
                result.left = result.left.RemoveRange(index, deletionAmountOnLeftSide);
                result.right = result.right.RemoveRange(0, count - deletionAmountOnLeftSide);
            }
            // The deletion might have introduced empty nodes. Those must be removed.
            if (result.left.length == 0)
                return result.right;
            if (result.right.length == 0)
                return result.left;

            result.length -= count;
            result.MergeIfPossible();
            result.Rebalance();
        }
        return result;
    }

#if DEBUG
    internal virtual void AppendTreeToString(StringBuilder b, int indent)
    {
        b.AppendLine(ToString());
        indent += 2;
        if (left != null)
        {
            b.Append(' ', indent);
            b.Append("L: ");
            left.AppendTreeToString(b, indent);
        }
        if (right != null)
        {
            b.Append(' ', indent);
            b.Append("R: ");
            right.AppendTreeToString(b, indent);
        }
    }

    public override string ToString()
    {
        if (contents != null)
        {
            char[] charContents = contents as char[];
            if (charContents != null)
                return "[Leaf length=" + length + ", isShared=" + isShared + ", text=\"" + new string(charContents, 0, length) + "\"]";
            else
                return "[Leaf length=" + length + ", isShared=" + isShared + "\"]";
        }
        else
        {
            return "[Concat length=" + length + ", isShared=" + isShared + ", height=" + height + ", Balance=" + this.Balance + "]";
        }
    }

    internal string GetTreeAsString()
    {
        StringBuilder b = new StringBuilder();
        AppendTreeToString(b, 0);
        return b.ToString();
    }
#endif

    /// <summary>
    /// Gets the root node of the subtree from a lazily evaluated function node.
    /// Such nodes are always marked as shared.
    /// GetContentNode() will return either a Concat or Leaf node, never another FunctionNode.
    /// 从延迟计算的函数节点获取子树的根节点。
    /// 此类节点始终标记为共享。
    /// GetContentNode() 将返回 Concat 或 Leaf 节点，而不是另一个 FunctionNode。
    /// </summary>
    internal virtual RopeNode<T> GetContentNode()
    {
        throw new InvalidOperationException("Called GetContentNode() on non-FunctionNode.");
    }
}

/// <summary>
/// 功能节点
/// </summary>
/// <typeparam name="T"></typeparam>
sealed class FunctionNode<T> : RopeNode<T>
{
    /// <summary>
    /// 初始化函数
    /// </summary>
    Func<Rope<T>> initializer;

    /// <summary>
    /// 缓存的结果
    /// </summary>
    RopeNode<T> cachedResults;

    public FunctionNode(int length, Func<Rope<T>> initializer)
    {
        Debug.Assert(length > 0);
        Debug.Assert(initializer != null);

        this.length = length;
        this.initializer = initializer;
        // Function nodes are immediately shared, but cannot be cloned.
        // This ensures we evaluate every initializer only once.
        // 功能节点立即共享，但无法克隆。
        // 这确保我们只评估每个初始化器一次。
        this.isShared = true;
    }

    /// <summary>
    /// 获取内容节点
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    internal override RopeNode<T> GetContentNode()
    {
        lock (this)
        {
            if (this.cachedResults == null)
            {
                if (this.initializer == null)
                    throw new InvalidOperationException("Trying to load this node recursively; or: a previous call to a rope initializer failed.");
                Func<Rope<T>> initializerCopy = this.initializer;
                this.initializer = null;
                Rope<T> resultRope = initializerCopy();
                if (resultRope == null)
                    throw new InvalidOperationException("Rope initializer returned null.");
                RopeNode<T> resultNode = resultRope.root;
                resultNode.Publish(); // result is shared between returned rope and the rope containing this function node
                if (resultNode.length != this.length)
                    throw new InvalidOperationException("Rope initializer returned rope with incorrect length.");
                if (resultNode.height == 0 && resultNode.contents == null)
                {
                    // ResultNode is another function node.
                    // We want to guarantee that GetContentNode() never returns function nodes, so we have to
                    // go down further in the tree.
                    this.cachedResults = resultNode.GetContentNode();
                }
                else
                {
                    this.cachedResults = resultNode;
                }
            }
            return this.cachedResults;
        }
    }

#if DEBUG
    internal override void AppendTreeToString(StringBuilder b, int indent)
    {
        RopeNode<T> resultNode;
        lock (this)
        {
            b.AppendLine(ToString());
            resultNode = cachedResults;
        }
        indent += 2;
        if (resultNode != null)
        {
            b.Append(' ', indent);
            b.Append("C: ");
            resultNode.AppendTreeToString(b, indent);
        }
    }

    public override string ToString()
    {
        return "[FunctionNode length=" + length + " initializerRan=" + (initializer == null) + "]";
    }
#endif
}
