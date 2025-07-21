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
				Util.CheckIndexInRange(index, Length);
				return Ptr[index];
			}
			set
			{
				Util.CheckIndexInRange(index, Length);
				Ptr[index] = value;
			}
		}

		public Span(T* ptr, int length)
		{
			Util.CheckLength(length);
			Ptr = ptr;
			Length = length;
		}

		public ref T ItemAsRef(int index)
		{
			Util.CheckIndexInRange(index, Length);
			return ref Ptr[index];
		}

		public Span<T> Slice(int start, int length)
		{
			Util.CheckLength(length);
			Util.CheckIndexInRange(start + length, Length);

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