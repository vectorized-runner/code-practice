#define DEBUG_CHECKS
using System;

namespace CodePractice
{
	// TODO: CopyTo
	// TODO: Ref iterator
	public unsafe struct Span<T> where T : unmanaged
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

		public Span(T* ptr, int length)
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

		public Span<T> Slice(int start, int length)
		{
			MemoryUtil.CheckLength(length);
			MemoryUtil.CheckIndexInRange(start + length, Length);

			return new Span<T>(Ptr + start, length);
		}

		public Span<T> Slice(int start)
		{
			// TODO:
			// Util.CheckIndexInRange(start, Length);
			// return 
			throw new NotImplementedException();
		}
	}
}