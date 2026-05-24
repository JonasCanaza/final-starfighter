using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class BulletController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private float speed = 15.0f;

    private Camera mainCamera;
    private SpriteRenderer visual;

    private void Awake()
    {
        mainCamera = Camera.main;
        visual = GetComponentInChildren<SpriteRenderer>();
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

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private bool IsBulletOffScreen()
    {
        float bottomScreenEdge = mainCamera.transform.position.y + mainCamera.orthographicSize + visual.bounds.extents.y;

        return transform.position.y > bottomScreenEdge;
    }
}