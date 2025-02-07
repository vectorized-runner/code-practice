#define DEBUG_CHECKS
using System;

namespace Code
{
	// TODO: This should be allocator-aware (allocator is a field, etc.)
	// TODO: Ref iterator
	public unsafe struct Array<T> : IDisposable where T : unmanaged
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

		public Array(int length)
		{
			Util.CheckLength(length);
			Ptr = Util.Malloc<T>(length);
			Length = length;
		}

		public void Dispose()
		{
			if (Ptr != null)
			{
				Util.Free(Ptr);
				this = default;
			}
		}
	}
}