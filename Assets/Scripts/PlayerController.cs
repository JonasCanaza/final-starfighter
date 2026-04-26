using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] float speed = 15.0f;
    private float moveInput;

    private void Update()
    {
        ReadInput();
        Movement();
    }

    private void ReadInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    private void Movement()
    {
        Vector3 newPosition = transform.position;
        newPosition.x += moveInput * speed * Time.deltaTime;
        transform.position = newPosition;
    }
}