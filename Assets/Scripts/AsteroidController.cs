using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class AsteroidController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float minSpeed = 3.0f;
    [SerializeField] private float maxSpeed = 5.0f;
    private float currentSpeed;

    private Camera mainCamera;
    private SpriteRenderer visual;

    private void Awake()
    {
        mainCamera = Camera.main;
        visual = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position -= Vector3.up * (currentSpeed * Time.deltaTime);

        if (IsAsteroidOffScreen())
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

    public void Activate()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private bool IsAsteroidOffScreen()
    {
        float bottomScreenEdge = mainCamera.transform.position.y - mainCamera.orthographicSize - visual.bounds.extents.y;

        return transform.position.y < bottomScreenEdge;
    }
}