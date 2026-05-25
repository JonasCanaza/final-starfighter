using UnityEngine;

public class ObjectPool<T> where T : Entity
{
    private T[] pool;

    public ObjectPool(T prefab, int size, Transform parent)
    {
        pool = new T[size];

        for (int i = 0; i < size; i++)
        {
            T item = Object.Instantiate(prefab, parent);

            item.Deactivate();

            pool[i] = item;
        }
    }

    public T GetAvailable()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }

        return null;
    }
}