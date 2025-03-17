using System;

namespace CodePractice
{
    public unsafe struct LinearAllocator : IAllocator
    {
        public void* Buffer;
        public int Length;
        public int Offset;

        public LinearAllocator(int length)
        {
            // TODO: What should be the alignment?
            Buffer = Util.Malloc(length, 16);
            Length = length;
            Offset = 0;
        }

        public void* Alloc(int size)
        {
            if (size <= 0)
                throw new Exception($"Size must be positive, but it's '{size}'");
            
            if (Offset + size <= Length)
            {
                void* mem = &Buffer[Offset];
                Offset += size;
                Util.MemClear(mem, size);
                return mem;
            }

            throw new Exception($"Allocator is full. Can't allocate '{size}' bytes");
        }

        public void Free()
        {
            throw new System.NotImplementedException();
        }

        public void Resize(int newSize)
        {
            throw new System.NotImplementedException();
        }

        public void Rewind()
        {
            
        }
    }
}