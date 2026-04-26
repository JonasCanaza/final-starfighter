using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private float speed = 15.0f;

    void Update()
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);
    }
}