using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace CodePractice
{
    // public struct SmallBufferEnumerator<T> where T : unmanaged
    // {
    //     private readonly SmallBuffer<T> _buffer;
    //     private int _index;
    //
    //     public SmallBufferEnumerator(SmallBuffer<T> buffer)
    //     {
    //         _buffer = buffer;
    //         _index = 0;
    //     }
    //
    //     // TODO: ??? - does this automatically convert?
    // }

// TODO: This
// TODO: AsSpan
// TODO: ForEach
// TODO: IDisposable
    // Can't make this Generic!, implementation is blocked by C# compiler:
    // System.TypeLoadException : Generic Type Definition failed to init, due to: Generic class cannot have explicit layout.
    // https://github.com/dotnet/runtime/issues/43486
    // https://github.com/dotnet/runtime/issues/97526
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public unsafe struct SmallBufferInt
        // <T> where T : unmanaged // Total Size: 16 + 4 = 20 bytes
    {
        [FieldOffset(0)]
        public fixed byte Buffer[_smallBufferSize];

        // Implementation is blocked, C# doesn't allow this:
        // [FieldOffset(0)] public T* Data;

        [FieldOffset(0)]
        public int* Data;
        
        [FieldOffset(_smallBufferSize)]
        public int Length;

        private const int _smallBufferSize = 16; // Only 16 bytes come for free
        
        public int this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= Length || index < 0)
                {
                    throw new IndexOutOfRangeException($"Index {index} is out of range.");
                }

                var byteSize = sizeof(int) * Length;
                if (byteSize <= _smallBufferSize)
                {
                    fixed (byte* buf = Buffer)
                    {
                        return ((int*)buf)[index];
                    }
                }

                return Data[index];
            }
        }

        public SmallBufferInt(Span<int> items)
        {
            var len = items.Length;
            var byteSize = sizeof(int) * len;
            Length = len;
            Data = null;

            if (byteSize <= _smallBufferSize)
            {
                fixed (byte* buf = Buffer)
                {
                    for (int i = 0; i < len; i++)
                    {
                        ((int*)buf)[i] = items[i];
                    }
                }
            }
            else
            {
                Data = (int*)UnsafeUtility.Malloc(byteSize, UnsafeUtility.AlignOf<int>(), Allocator.Persistent);
                
                for (int i = 0; i < len; i++)
                {
                    Data[i] = items[i];
                }
            }

            if (Length != len)
            {
                throw new Exception($"Overwritten on length, Length is {Length}");
            }
        }
    }
}