#define DEBUG_CHECKS
using System;

namespace CodePractice
{
	// TODO: This should be allocator-aware (allocator is a field, etc.)
	// TODO: Ref iterator
	public unsafe struct Array<T> : IDisposable, IEquatable<Array<T>> where T : unmanaged
	{
		public T* Ptr;
		public int Length;

		public T this[int index]
		{
			get
			{
				Util.CheckIndexInRange(index, Length);
				return Ptr[index];
			}
			set
			{
				Util.CheckIndexInRange(index, Length);
				Ptr[index] = value;
			}
		}

		public ref T ItemAsRef(int index)
		{
			Util.CheckIndexInRange(index, Length);
			return ref Ptr[index];
		}

		public Array(int length, bool clearMemory = false)
		{
			Util.CheckLength(length);
			Ptr = Util.Malloc<T>(length);
			Length = length;

			if (clearMemory)
			{
				Util.MemClear(Ptr, length);
			}
		}

		public void Dispose()
		{
			if (Ptr != null)
			{
				Util.Free(Ptr);
				this = default;
			}
		}

		public bool Equals(Array<T> other)
		{
			return Ptr == other.Ptr && Length == other.Length;
		}

		public override bool Equals(object obj)
		{
			return obj is Array<T> other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(unchecked((int)(long)Ptr), Length);
		}
	}
}