using System;
using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace CodePractice
{
    // TODO: Linear Allocator user-provided initial buffer
    // TODO: Linear Allocator grows or not?
    // TODO: Typed Linear allocator?
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

        public void* Alloc(int size)
        {
            MemoryUtil.CheckAllocSize(size);

            if (Allocated + size <= Length)
            {
                var bufferAsBytePtr = (byte*)Buffer;
                void* mem = &bufferAsBytePtr[Allocated];
                Allocated += size;
                MemoryUtil.MemClear(mem, size);
                return mem;
            }

            return null;
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