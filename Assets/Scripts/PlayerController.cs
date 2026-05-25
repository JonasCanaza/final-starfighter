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
    private BulletController[] bulletPool;
    private float lastFireTime;

    protected override void Awake()
    {
        base.Awake();

        InitBullets();
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

    private void InitBullets()
    {
        bulletPool = new BulletController[bulletPoolSize];

        for (int i = 0; i < bulletPoolSize; i++)
        {
            BulletController newBullet = Instantiate(bulletPrefab, bulletContainer);
            newBullet.Deactivate();
            bulletPool[i] = newBullet;
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

    private bool CanFire()
    {
        return Time.time >= lastFireTime + fireCooldown;
    }

    private void Shoot()
    {
        BulletController newBullet = GetAvailableBullet();

        if (!newBullet)
        {
            return;
        }

        newBullet.transform.position = firePoint.position;
        newBullet.Activate();

        lastFireTime = Time.time;
    }

    private BulletController GetAvailableBullet()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            if (!bulletPool[i].gameObject.activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return null;
    }
}