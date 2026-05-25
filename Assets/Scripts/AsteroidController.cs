using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class AsteroidController : Entity
{
    [Header("Movement Settings")]
    [SerializeField] private float minSpeed = 3.0f;
    [SerializeField] private float maxSpeed = 5.0f;
    private float currentSpeed;

    private void Update()
    {
        transform.position -= Vector3.up * (currentSpeed * Time.deltaTime);

        if (MainCamera.IsBelowBottomEdge(transform.position.y, HalfHeight))
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Deactivate();
        }
    }

    public override void Activate()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);

        base.Activate();
    }
}