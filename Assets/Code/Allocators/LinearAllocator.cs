using UnityEngine;

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
            Debug.Assert(size >= 0);

            if (Offset + size <= Length)
            {
                byte* bufferAsBytePtr = (byte*)Buffer;
                void* mem = &bufferAsBytePtr[Offset];
                Offset += size;
                Util.MemClear(mem, size);
                return mem;
            }

            return null;
        }

        public void Free(void* ptr)
        {
            // No-op
        }

        public void FreeAll()
        {
            Offset = 0;
        }

        public void Resize(int newSize)
        {
            throw new System.NotImplementedException();
        }
    }
}