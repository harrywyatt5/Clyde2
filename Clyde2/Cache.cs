namespace Clyde2;

public class Cache<T>
{
    private int _maxSize;
    private Queue<T> _queue;

    public Cache(int maxSize)
    {
        _maxSize = maxSize;
        _queue = new Queue<T>();
    }

    public Cache(int initialSize, int maxSize)
    {
        _maxSize = maxSize;
        _queue = new Queue<T>(initialSize);
    }

    private void EnsureCacheSize()
    {
        if (_queue.Count > _maxSize)
        {
            _queue.Dequeue();
        }
    }

    public void CacheElement(T element)
    {
        _queue.Enqueue(element);
        EnsureCacheSize();
    }

    public IEnumerable<T> ToEnumerable() => _queue;
}
