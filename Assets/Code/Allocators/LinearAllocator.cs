using System;
using Unity.Collections.LowLevel.Unsafe;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace CodePractice
{
	public unsafe struct LinearAllocator : IAllocator, IDisposable
	{
		public void* Buffer;
		public int Length;
		public int Allocated;

		public LinearAllocator(int length)
		{
			Buffer = MemoryUtil.Malloc(length);
			Length = length;
			Allocated = 0;
		}

		public T* Alloc<T>(int count = 1) where T : unmanaged
		{
			return (T*)Alloc(UnsafeUtility.SizeOf<T>() * count);
		}

		public void* Alloc(int size)
		{
			MemoryUtil.CheckAllocSize(size);

			if (Allocated + size > Length)
			{
				throw new Exception(
					$"Allocated Size {size} is out of LinearAllocator bounds. Allocated {Allocated} Length {Length}");
			}

			var bufferAsBytePtr = (byte*)Buffer;
			void* mem = &bufferAsBytePtr[Allocated];
			Allocated += size;
			MemoryUtil.MemClear(mem, size);
			return mem;
		}

		public void Clear()
		{
			Allocated = 0;
		}

		public void Free()
		{
			Dispose();
		}

		public void Resize(int newSize)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			if (Buffer != null)
			{
				MemoryUtil.Free(Buffer);
			}

			this = default;
		}
	}
}