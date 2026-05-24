using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected SpriteRenderer Visual { get; private set; }

    public float HalfWidth
    {
        get
        {
            return Visual.bounds.extents.x;
        }
    }

    public float HalfHeight
    {
        get
        {
            return Visual.bounds.extents.y;
        }
    }

    protected virtual void Awake()
    {
        Visual = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
}