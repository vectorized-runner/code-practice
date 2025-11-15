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
				MemoryUtil.CheckIndexInRange(index, Length);
				return Ptr[index];
			}
			set
			{
				MemoryUtil.CheckIndexInRange(index, Length);
				Ptr[index] = value;
			}
		}

		public MemorySlice(T* ptr, int length)
		{
			MemoryUtil.CheckLength(length);
			Ptr = ptr;
			Length = length;
		}

		public ref T ItemAsRef(int index)
		{
			MemoryUtil.CheckIndexInRange(index, Length);
			return ref Ptr[index];
		}

		public MemorySlice<T> Slice(int start, int length)
		{
			MemoryUtil.CheckLength(length);
			MemoryUtil.CheckIndexInRange(start + length, Length);

			return new MemorySlice<T>(Ptr + start, length);
		}
	}
}