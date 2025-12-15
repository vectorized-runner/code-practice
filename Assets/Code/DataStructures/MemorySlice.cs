#define DEBUG_CHECKS
using System;

namespace CodePractice
{
	// TODO-MemorySlice: CopyTo
	// TODO-MemorySlice: Ref iterator
	public unsafe struct MemorySlice<T> where T : unmanaged
	{
		public T* Ptr;
		public int Length;
		
		public T this[int index]
		{
			get
			{
				AllocatorHelper.CheckIndexInRange(index, Length);
				return Ptr[index];
			}
			set
			{
				AllocatorHelper.CheckIndexInRange(index, Length);
				Ptr[index] = value;
			}
		}

		public MemorySlice(T* ptr, int length)
		{
			AllocatorHelper.CheckLength(length);
			Ptr = ptr;
			Length = length;
		}

		public ref T ItemAsRef(int index)
		{
			AllocatorHelper.CheckIndexInRange(index, Length);
			return ref Ptr[index];
		}

		public MemorySlice<T> Slice(int start, int length)
		{
			AllocatorHelper.CheckLength(length);
			AllocatorHelper.CheckIndexInRange(start + length, Length);

			return new MemorySlice<T>(Ptr + start, length);
		}
	}
}