using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : Entity
{
    [Header("Movement Setting")]
    [SerializeField] private float speed = 15.0f;
    private float moveInput;
    private bool canPlay = true;

    [Header("Shooting Settings")]
    [SerializeField] private int bulletPoolSize = 10;
    [SerializeField] private float fireCooldown = 0.1f;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform bulletContainer;
    private ObjectPool<BulletController> bulletPool;
    private float lastFireTime;

    [Header("Clips References")]
    [SerializeField] private AudioClip[] shotClips;
    private int lastShotClipIndex = -1;

    protected override void Awake()
    {
        base.Awake();

        bulletPool = new ObjectPool<BulletController>(bulletPrefab, bulletPoolSize, bulletContainer);
    }

    private void OnEnable() => EventBus.Subscribe<GamePausedEvent>(OnGamePaused);

    private void OnDisable() => EventBus.Unsubscribe<GamePausedEvent>(OnGamePaused);

    private void Update()
    {   
        ReadPauseInput();

        if (canPlay)
        {
            ReadGameplayInput();
            Movement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    private void ReadPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBus.Publish(new PauseRequestedEvent());
        }
    }

    private void ReadGameplayInput()
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

        AudioClip shotClip = GetShotClip();

        if (shotClip)
        {
            AudioManager.Instance.PlaySFX(shotClip);
        }

        newBullet.transform.position = firePoint.position;
        newBullet.Activate();

        lastFireTime = Time.time;
    }

    private AudioClip GetShotClip()
    {
        if (shotClips.Length == 0)
        {
            return null;
        }

        int randomClipIndex;

        do
        {
            randomClipIndex = Random.Range(0, shotClips.Length);
        }
        while (randomClipIndex == lastShotClipIndex && shotClips.Length > 1);

        lastShotClipIndex = randomClipIndex;

        return shotClips[randomClipIndex];
    }

    private void OnGamePaused(GamePausedEvent gamePausedEvent) => canPlay = !gamePausedEvent.IsPaused;
}