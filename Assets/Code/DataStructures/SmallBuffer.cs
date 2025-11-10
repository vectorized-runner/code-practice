using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
    [FieldOffset(0)] public fixed byte Buffer[_smallBufferLength];

    [FieldOffset(0)] public T* Data;

    [FieldOffset(8)] public int Count;

    private const int _smallBufferLength = 16; // Only 16 bytes come for free

    public T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (index >= Count || index < 0)
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
        var byteSize = sizeof(T) * items.Length;
        throw new NotImplementedException();
    }
}