using System;
using System.Collections.Generic;
using System.Text;

namespace ICSharpCode.AvalonEdit.Utils
{
    /// <summary>
    /// An immutable stack. 一个不可变的栈数据结构
    /// 它的主要作用是提供一种线程安全的、不可变的数据结构，用于存储和操作栈中的元素。
	/// 在多线程环境中，使用不可变的数据结构可以避免竞态条件和数据不一致的问题，
	/// 因为不可变的数据结构在创建后就不能被修改。
	/// 这使得多个线程可以同时访问和操作同一个 ImmutableStack<T> 实例，而不需要进行同步或锁定。
    /// Using 'foreach' on the stack will return the items from top to bottom (in the order they would be popped).
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    [Serializable]
    public sealed class ImmutableStack<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets the empty stack instance.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "ImmutableStack is immutable")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static readonly ImmutableStack<T> Empty = new ImmutableStack<T>();

        readonly T value;
        readonly ImmutableStack<T> next;

        private ImmutableStack()
        {
        }

        private ImmutableStack(T value, ImmutableStack<T> next)
        {
            this.value = value;
            this.next = next;
        }

        // - push() 方法用于将元素推入栈顶
        // - peek() 方法用于查看栈顶元素但不移除它。 
        // - pop() 方法用于移除并返回栈顶元素。 

        /// <summary>
        /// Pushes an item on the stack. This does not modify the stack itself, but returns a new
        /// one with the value pushed.
        /// </summary>
        public ImmutableStack<T> Push(T item)
        {
            return new ImmutableStack<T>(item, this);
        }

        /// <summary>
        /// Gets the item on the top of the stack.
        /// 获取堆栈顶部的项目。
        /// </summary>
        /// <exception cref="InvalidOperationException">The stack is empty.</exception>
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Operation not valid on empty stack.");
            return value;
        }

        /// <summary>
        /// Gets the item on the top of the stack.
        /// Returns <c>default(T)</c> if the stack is empty.
        /// </summary>
        public T PeekOrDefault()
        {
            return value;
        }

        /// <summary>
        /// Gets the stack with the top item removed.
        /// 获取顶部项目被移除的堆栈。
        /// </summary>
        /// <exception cref="InvalidOperationException">The stack is empty.</exception>
        public ImmutableStack<T> Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Operation not valid on empty stack.");
            return next;
        }

        /// <summary>
        /// Gets if this stack is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return next == null; }
        }

        /// <summary>
        /// Gets an enumerator that iterates through the stack top-to-bottom.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            ImmutableStack<T> t = this;
            while (!t.IsEmpty)
            {
                yield return t.value;
                t = t.next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder("[Stack");
            foreach (T val in this)
            {
                b.Append(' ');
                b.Append(val);
            }
            b.Append(']');
            return b.ToString();
        }
    }
}