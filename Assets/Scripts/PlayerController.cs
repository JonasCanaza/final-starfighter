using UnityEngine;

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

    private void ReadInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    private void Movement()
    {
        Vector3 newPosition = transform.position;
        newPosition.x += moveInput * speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, leftScreenLimit, rightScreenLimit);
        transform.position = newPosition;
    }
}