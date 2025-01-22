using System;

namespace ICSharpCode.AvalonEdit.Utils
{
    /// <summary>
    /// Represents a string with a segment.
    /// Similar to System.ArraySegment&lt;T&gt;, but for strings instead of arrays.
    /// StringSegment 是一个结构体，用于表示字符串的一个片段。
	/// 它类似于 System.ArraySegment<T>，但用于字符串而不是数组。
    /// </summary>
    public struct StringSegment : IEquatable<StringSegment>
	{
		readonly string text;
		readonly int offset;
		readonly int count;

		/// <summary>
		/// Creates a new StringSegment.
		/// </summary>
		public StringSegment(string text, int offset, int count)
		{
			if (text == null)
				throw new ArgumentNullException("text");
			if (offset < 0 || offset > text.Length)
				throw new ArgumentOutOfRangeException("offset");
			if (offset + count > text.Length)
				throw new ArgumentOutOfRangeException("count");
			this.text = text;
			this.offset = offset;
			this.count = count;
		}

		/// <summary>
		/// Creates a new StringSegment.
		/// </summary>
		public StringSegment(string text)
		{
			if (text == null)
				throw new ArgumentNullException("text");
			this.text = text;
			this.offset = 0;
			this.count = text.Length;
		}

		/// <summary>
		/// Gets the string used for this segment.
		/// </summary>
		public string Text {
			get { return text; }
		}

		/// <summary>
		/// Gets the start offset of the segment with the text.
		/// </summary>
		public int Offset {
			get { return offset; }
		}

		/// <summary>
		/// Gets the length of the segment.
		/// </summary>
		public int Count {
			get { return count; }
		}

		public override bool Equals(object obj)
		{
			if (obj is StringSegment)
				return Equals((StringSegment)obj); // use Equals method below
			else
				return false;
		}

		public bool Equals(StringSegment other)
		{
			// add comparisions for all members here
			return object.ReferenceEquals(this.text, other.text) && offset == other.offset && count == other.count;
		}

		public override int GetHashCode()
		{
			return text.GetHashCode() ^ offset ^ count;
		}

		/// <summary>
		/// Equality operator.
		/// </summary>
		public static bool operator ==(StringSegment left, StringSegment right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Inequality operator.
		/// </summary>
		public static bool operator !=(StringSegment left, StringSegment right)
		{
			return !left.Equals(right);
		}
	}
}
