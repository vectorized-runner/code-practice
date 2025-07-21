#define DEBUG_CHECKS
using System;
using Unity.Collections;

namespace CodePractice
{
	// TODO: This should be allocator-aware (allocator is a field, etc.)
	// TODO: Ref iterator
	public unsafe struct Array<T> : IDisposable, IEquatable<Array<T>> where T : unmanaged
	{
		public T* Ptr;
		public int Length;

		public bool IsCreated => Ptr != null;

		public T this[int index]
		{
			get
			{
				MemoryUtil.CheckIndexInRange(index, Length);
				return Ptr[index];
			}
			set
			{
				MemoryUtil.CheckIndexInRange(index, Length);
				Ptr[index] = value;
			}
		}
		
		public ref T ItemAsRef(int index)
		{
			MemoryUtil.CheckIndexInRange(index, Length);
			return ref Ptr[index];
		}

		public Array(int length, bool clearMemory = false)
		{
			MemoryUtil.CheckLength(length);
			Ptr = MemoryUtil.Malloc<T>(length);
			Length = length;

			if (clearMemory)
			{
				MemoryUtil.MemClear(Ptr, length);
			}
		}

		public void Dispose()
		{
			if (Ptr != null)
			{
				MemoryUtil.Free(Ptr);
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