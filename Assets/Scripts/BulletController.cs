using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class BulletController : Entity
{
    [Header("Movement Setting")]
    [SerializeField] private float speed = 15.0f;

    private Camera mainCamera;

    protected override void Awake()
    {
        base.Awake();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);

        if (IsBulletOffScreen())
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

    private bool IsBulletOffScreen()
    {
        float topScreenEdge = mainCamera.transform.position.y + mainCamera.orthographicSize + HalfHeight;

        return transform.position.y > topScreenEdge;
    }
}