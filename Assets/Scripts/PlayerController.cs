using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private float speed = 15.0f;
    private float moveInput;

    [Header("References")]
    [SerializeField] private SpriteRenderer visual;
    private Camera mainCamera;
    private float leftScreenLimit;
    private float rightScreenLimit;

    [Header("Shooting Settings")]
    [SerializeField] private float fireCooldown = 0.1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform bulletContainer;
    private float lastFireTime;

    private void Start()
    {
        mainCamera = Camera.main;

        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        float halfPlayerWidth = visual.bounds.extents.x;

        leftScreenLimit = screenLeft + halfPlayerWidth;
        rightScreenLimit = screenRight - halfPlayerWidth;
    }

    private void Update()
    {   
        ReadInput();
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    private void ReadInput()
    {
        // MOVEMENT
        moveInput = Input.GetAxisRaw("Horizontal");

        // SHOOT
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && CanFire())
        {
            Shoot();
        }
    }

    private void Movement()
    {
        Vector3 newPosition = transform.position;
        newPosition.x += moveInput * speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, leftScreenLimit, rightScreenLimit);
        transform.position = newPosition;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity, bulletContainer);
        lastFireTime = Time.time;
    }

    private bool CanFire()
    {
        return Time.time >= lastFireTime + fireCooldown;
    }
}