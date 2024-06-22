using System.Collections;

internal class Program
{
    private static void Main(string[] args)
    {
        LRUCache lRUCache = new LRUCache(2);
        lRUCache.Put(2, 1);
        lRUCache.Put(1, 1);
        lRUCache.Put(2, 3);
        lRUCache.Put(4, 1);
        lRUCache.Get(1);
        lRUCache.Get(2);

    }
}

public class LRUCache
{
    private Dictionary<int, int> _keyValue;
    private Dictionary<int, int> _keyAge;
    private Dictionary<int, int> _ageKey;
    private int _capacity;
    private int _age;
    private int _lastDeletedAge;
    public LRUCache(int capacity)
    {
        _keyAge = new Dictionary<int, int>();
        _ageKey = new Dictionary<int, int>();
        _keyValue = new Dictionary<int, int>();
        _capacity = capacity;
        _age = 0;
        _lastDeletedAge = -1;
    }

    public int Get(int key)
    {
        if(_keyValue.TryGetValue(key,out int result))
        {
            _age++;
            _ageKey.Remove(_keyAge[key]);
            _keyAge[key] = _age;
            _ageKey[_age] = key;
            return result;
        }
        return -1;
    }

    public void Put(int key, int value)
    {
        _age++;
        if(_keyValue.ContainsKey(key))
        {
            _keyValue[key] = value;
            _ageKey.Remove(_keyAge[key]);
            _keyAge[key] = _age;
            _ageKey[_age] = key;
        }
        else
        {
            if(_keyValue.Count >= _capacity)
            {
                while(!_ageKey.ContainsKey(_lastDeletedAge))
                {
                    _lastDeletedAge++;
                }
                var keyToDelete = _ageKey[_lastDeletedAge];
                _keyValue.Remove(keyToDelete);
                _keyAge.Remove(keyToDelete);
                _ageKey.Remove(_lastDeletedAge);
            }
            _keyValue.Add(key, value);
            _keyAge.Add(key, _age);
            _ageKey.Add(_age, key);
        }
    }
}