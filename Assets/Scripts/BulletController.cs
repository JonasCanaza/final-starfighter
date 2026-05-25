using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class BulletController : Entity
{
    [Header("Movement Setting")]
    [SerializeField] private float speed = 15.0f;

    private void Update()
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);

        if (MainCamera.IsAboveTopEdge(transform.position.y, HalfHeight))
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            Deactivate();
        }
    }
}