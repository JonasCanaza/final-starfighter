using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class BulletController : Entity
{
    [Header("Movement Setting")]
    [SerializeField] private float speed = 15.0f;

    [Header("Clip Reference")]
    [SerializeField] private AudioClip impactClip;

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
            AudioManager.Instance.PlaySFX(impactClip);
            Deactivate();
        }
    }
}