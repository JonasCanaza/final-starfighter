using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : Entity
{
    [Header("Movement Setting")]
    [SerializeField] private float speed = 15.0f;
    private float moveInput;

    [Header("Shooting Settings")]
    [SerializeField] private int bulletPoolSize = 10;
    [SerializeField] private float fireCooldown = 0.1f;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform bulletContainer;
    private ObjectPool<BulletController> bulletPool;
    private float lastFireTime;

    [Header("Clip References")]
    [SerializeField] private AudioClip shotClip;

    protected override void Awake()
    {
        base.Awake();

        bulletPool = new ObjectPool<BulletController>(bulletPrefab, bulletPoolSize, bulletContainer);
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

        float leftLimit = MainCamera.GetLeftEdge() + HalfWidth;
        float rightLimit = MainCamera.GetRightEdge() - HalfWidth;

        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);
        transform.position = newPosition;
    }

    private bool CanFire() => Time.time >= lastFireTime + fireCooldown;

    private void Shoot()
    {
        BulletController newBullet = bulletPool.GetAvailable();

        if (!newBullet)
        {
            return;
        }

        AudioManager.Instance.PlaySFX(shotClip);
        newBullet.transform.position = firePoint.position;
        newBullet.Activate();

        lastFireTime = Time.time;
    }
}