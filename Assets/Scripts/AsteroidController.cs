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

    private void Start()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.position -= Vector3.up * (currentSpeed * Time.deltaTime);

        if (IsAsteroidOffScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool IsAsteroidOffScreen()
    {
        float bottomScreenEdge = mainCamera.transform.position.y - mainCamera.orthographicSize - visual.bounds.extents.y;

        return transform.position.y < bottomScreenEdge;
    }
}