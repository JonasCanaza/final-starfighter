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

    private void Awake()
    {
        InitAsteroids();

        mainCamera = Camera.main;
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

        float positionX = GetRandomScreenPositionX(newAsteroid.HalfWidth);
        float positionY = mainCamera.GetTopEdge() + newAsteroid.HalfHeight;
        Vector3 newAsteroidPosition = new Vector3(positionX, positionY, 0.0f);

        newAsteroid.transform.position = newAsteroidPosition;
        newAsteroid.Activate();
    }

    private float GetRandomScreenPositionX(float asteroidHalfWidth)
    {
        float left = mainCamera.GetLeftEdge() + asteroidHalfWidth;
        float right = mainCamera.GetRightEdge() - asteroidHalfWidth;

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