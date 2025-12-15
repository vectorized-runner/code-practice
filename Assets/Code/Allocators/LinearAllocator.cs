using System;
using Unity.Collections.LowLevel.Unsafe;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace CodePractice
{
	// TODO-LinearAlloc: Bitshift Alignment hack
	public unsafe struct LinearAllocator : IAllocator, IDisposable
	{
		public byte* Buffer;
		public int Length;
		public int Allocated;

		public LinearAllocator(int length)
		{
			Buffer = (byte*)MemoryUtil.Malloc(length);
			Length = length;
			Allocated = 0;
		}

		// stackalloc constructor
		public LinearAllocator(Span<byte> buffer)
		{
			fixed (byte* ptr = buffer)
			{
				Buffer = ptr;
				Length = buffer.Length;
				Allocated = 0;
			}
		}

		public LinearAllocator(void* buffer, int length)
		{
			Buffer = (byte*)buffer;
			Length = length;
			Allocated = 0;
		}
		
		public T* Alloc<T>(int count = 1, bool clearMemory = MemoryUtil.DefaultClearMemory) where T : unmanaged
		{
			return (T*)Alloc(UnsafeUtility.SizeOf<T>() * count, UnsafeUtility.AlignOf<T>(), clearMemory);
		}

		public void* Alloc(int size, int align = MemoryUtil.DefaultAlign, bool clearMemory = MemoryUtil.DefaultClearMemory)
		{
			MemoryUtil.CheckAllocSize(size);
			
			var currentPtr = &Buffer[Allocated];
			var alignedPtr = MemoryUtil.AlignForward(currentPtr, align);
			var alignBytes = (int)((long)alignedPtr - (long)currentPtr);

			if (Allocated + size + alignBytes > Length)
			{
				throw new Exception(
					$"Allocated Size {size} is out of LinearAllocator bounds. Allocated {Allocated} AlignBytes {alignBytes} Length {Length}");
			}

			Allocated += alignBytes + size;
			if (clearMemory)
			{
				UnsafeUtility.MemClear(alignedPtr, size);
			}

			return alignedPtr;
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
			throw new NotSupportedException();
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