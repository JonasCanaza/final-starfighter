using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float minSpeed = 3.0f;
    [SerializeField] private float maxSpeed = 5.0f;
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.position -= Vector3.up * (currentSpeed * Time.deltaTime);
    }
}