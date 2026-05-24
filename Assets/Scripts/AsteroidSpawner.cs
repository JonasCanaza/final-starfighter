using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private int asteroidPoolSize = 20;
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private AsteroidController asteroidPrefab;
    [SerializeField] private Transform asteroidContainer;
    private AsteroidController[] asteroidPool;
    private float timer = 0.0f;

    private Camera mainCamera;
    private SpriteRenderer visual;
    private float asteroidHalfWidth;
    private float asteroidHalfHeight;

    private void Awake()
    {
        InitAsteroids();

        mainCamera = Camera.main;
        visual = asteroidPrefab.GetComponentInChildren<SpriteRenderer>();
        asteroidHalfWidth = visual.bounds.extents.x;
        asteroidHalfHeight = visual.bounds.extents.y;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0.0f;
        }
    }

    private void InitAsteroids()
    {
        asteroidPool = new AsteroidController[asteroidPoolSize];

        for (int i = 0; i < asteroidPoolSize; i++)
        {
            AsteroidController newAsteroid = Instantiate(asteroidPrefab, asteroidContainer);
            newAsteroid.Deactivate();
            asteroidPool[i] = newAsteroid;
        }
    }

    private void SpawnAsteroid()
    {
        AsteroidController newAsteroid = GetAvailableAsteroid();

        if (!newAsteroid)
        {
            return;
        }

        float positionX = GetRandomScreenPositionX(asteroidHalfWidth);
        float positionY = mainCamera.transform.position.y + mainCamera.orthographicSize + asteroidHalfHeight;
        Vector3 newAsteroidPosition = new Vector3(positionX, positionY, 0.0f);

        newAsteroid.transform.position = newAsteroidPosition;
        newAsteroid.Activate();
    }

    private float GetRandomScreenPositionX(float asteroidHalfWidth)
    {
        float screenHeight = mainCamera.orthographicSize * 2.0f;
        float screenWidth = screenHeight * mainCamera.aspect;

        float left = mainCamera.transform.position.x - (screenWidth / 2.0f) + asteroidHalfWidth;
        float right = mainCamera.transform.position.x + (screenWidth / 2.0f) - asteroidHalfWidth;

        return Random.Range(left, right);
    }

    private AsteroidController GetAvailableAsteroid()
    {
        for (int i = 0; i < asteroidPoolSize; i++)
        {
            if (!asteroidPool[i].gameObject.activeInHierarchy)
            {
                return asteroidPool[i];
            }
        }

        return null;
    }
}