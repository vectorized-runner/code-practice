using System;
using Unity.Mathematics;

// Optimization Tricks:
// SoA data storage (keys and values separate): Don't load value in RAM unnecessarily
// Open Addressing + Linear Probing: Simple, cache locality
// Metadata (Swiss table-style probing): Branchless search
public class HashMap<TKey, TValue> where TKey : IEquatable<TKey>
{
    private TKey[] _keys;
    private TValue[] _values;
    // [empty | deleted | first 7 bits of hashcode]
    // Empty < 0, Full >= 0, Tombstone -> Not negative, 0xFE, 0xFF is used as sentinel
    private byte[] _metadataArray;
    private int _length;
    
    public int Length => _length;
    public int Capacity => _keys.Length;
    
    // TODO: Branchless search
    // TODO: Single contiguous block allocation?
    // TODO: backshift deletion
    // TODO: SIMD probing window
    // TODO: Implement resize

    private const float _maxLoadFactor = 0.9f;
    private const int _minCapacity = 4;

    public HashMap(int initialCapacity = _minCapacity)
    {
        if (initialCapacity < 0)
        {
            throw new Exception("InitialCapacity can't be negative.");
        }

        var capacity = math.ceilpow2(math.max(_minCapacity, initialCapacity));
        _keys = new TKey[capacity];
        _values = new TValue[capacity];
        _metadataArray = new byte[capacity];
    }
    
    public TValue this[TKey key]
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    private void Resize()
    {
    }

    public void Add(TKey key, TValue value)
    {
        var hash = key.GetHashCode();
        // Fast modulo. Requirement: Capacity is power of two
        var capacity = Capacity;
        var mod = capacity - 1;
        var idx = hash & mod;

        var loadFactor = (float)_length / capacity;
        if (loadFactor >= _maxLoadFactor)
        {
            Resize(); // TODO:
        }
        
        const byte empty = 0x00;
        const byte tombstone = 0xFE;

        while (_metadataArray[idx] != empty)
        {
            idx = (idx + 1) & mod; // wrap around
        }

        _metadataArray[idx] = (byte)(hash & 0xFF);
        _keys[idx] = key;
        _values[idx] = value;
    }

    public bool TryAdd(TKey key, TValue value)
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        throw new NotImplementedException();
    }

    public bool Remove(TKey key)
    {
        throw new NotImplementedException();
    }
}
