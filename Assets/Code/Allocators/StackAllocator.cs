using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace CodePractice
{
	public unsafe struct StackAllocator : IDisposable
	{
		public struct StackAllocatorHeader
		{
			public byte _a;
			public byte _b;
		}
		
		public byte* Buffer;
		public int Length;
		public int Allocated;

		private static readonly int _headerSize = sizeof(StackAllocatorHeader);
		
		public StackAllocator(int length, int align = AllocatorHelper.DefaultAlign)
		{
			Buffer = (byte*)AllocatorHelper.Malloc(length, align);
			Length = length;
			Allocated = 0;
		}

		// This is kind
		// I don't care about padding
		// Just store the previous pointer
		// Add some simple tests 
		public void* Alloc(int size, int align, bool clearMemory = AllocatorHelper.DefaultClearMemory)
		{
			// TODO-StackAllocator: 
			throw new NotImplementedException();
		}

		public void Pop()
		{
			// Free the last allocation
		}

		public void Dispose()
		{
			// TODO-StackAllocator release managed resources here
		}
	}
}