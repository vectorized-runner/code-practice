using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace CodePractice
{
    public struct SmallBufferEnumerator<T> where T : unmanaged
    {
        private readonly SmallBuffer<T> _buffer;
        private int _index;

        public SmallBufferEnumerator(SmallBuffer<T> buffer)
        {
            _buffer = buffer;
            _index = 0;
        }

        // TODO: ??? - does this automatically convert?
    }

// TODO: This
// TODO: AsSpan
// TODO: ForEach
// TODO: IDisposable
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct SmallBuffer<T> where T : unmanaged // Total Size: 16 + 4 = 20 bytes
    {
        [FieldOffset(0)] public fixed byte Buffer[_smallBufferSize];

        [FieldOffset(0)] public T* Data;

        [FieldOffset(8)] public int Length;

        private const int _smallBufferSize = 16; // Only 16 bytes come for free

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= Length || index < 0)
                {
                    throw new IndexOutOfRangeException($"Index {index} is out of range.");
                }

                // Means that we didn't use the small buffer
                if (Data != null)
                {
                    return Data[index];
                }

                fixed (byte* buf = Buffer)
                {
                    return ((T*)buf)[index];
                }
            }
        }

        public SmallBuffer(Span<T> items)
        {
            var len = items.Length;
            var byteSize = sizeof(T) * len;
            Length = len;

            if (byteSize >= _smallBufferSize)
            {
                fixed (byte* buf = Buffer)
                {
                    for (int i = 0; i < len; i++)
                    {
                        ((T*)buf)[i] = items[i];
                    }
                }

                Data = null;
            }
            else
            {
                Data = (T*)UnsafeUtility.Malloc(byteSize, UnsafeUtility.AlignOf<T>(), Allocator.Persistent);

                for (int i = 0; i < len; i++)
                {
                    Data[i] = items[i];
                }
            }
        }
    }
}