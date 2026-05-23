using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private AsteroidController asteroidPrefab;
    [SerializeField] private float spawnInterval = 1.0f;
    private float timer = 0.0f;

    private Camera mainCamera;
    private SpriteRenderer visual;
    private float asteroidHalfWidth;
    private float asteroidHalfHeight;

    private void Awake()
    {
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

    private void SpawnAsteroid()
    {
        float positionX = GetRandomScreenPositionX(asteroidHalfWidth);
        float positionY = mainCamera.transform.position.y + mainCamera.orthographicSize + asteroidHalfHeight;
        Vector3 newAsteroidPosition = new Vector3(positionX, positionY, 0.0f);

        Instantiate(asteroidPrefab, newAsteroidPosition, Quaternion.identity);
    }

    private float GetRandomScreenPositionX(float asteroidHalfWidth)
    {
        float screenHeight = mainCamera.orthographicSize * 2.0f;
        float screenWidth = screenHeight * mainCamera.aspect;

        float left = mainCamera.transform.position.x - (screenWidth / 2.0f) + asteroidHalfWidth;
        float right = mainCamera.transform.position.x + (screenWidth / 2.0f) - asteroidHalfWidth;

        return Random.Range(left, right);
    }
}