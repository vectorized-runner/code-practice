using System;

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

    private int Capacity => _keys.Length;
    
    public TValue this[TKey key]
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void Add(TKey key, TValue value)
    {
        var hash = key.GetHashCode();
        // Fast modulo. Requirement: Capacity is power of two
        var idx = hash & (Capacity - 1);
        
        throw new NotImplementedException();
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
