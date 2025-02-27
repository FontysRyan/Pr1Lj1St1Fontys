using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxTransformPostionY = 3.3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private string inputAxis = "Vertical";
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector3 startPosition = new Vector3(-8.5f, 0f, 0f);
    [SerializeField] private float moveInput;

    private void Start()
    {
        InitializePosition();
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void InitializePosition()
    {
        rb.position = startPosition; // Set the Rigidbody2D's position to the start position
    }

    private void ReadInput()
    {
        moveInput = Input.GetAxis(inputAxis);
    }

    private void ApplyMovement()
    {
        Vector2 newPosition = rb.position + Vector2.up * moveInput * speed * Time.fixedDeltaTime; // Calculate new position based on input and speed (aanpassen)
        newPosition.y = Mathf.Clamp(newPosition.y, -maxTransformPostionY, maxTransformPostionY); // don't go out of bounds
        rb.MovePosition(newPosition);
    }

    public void ResetPosition()
    {
        rb.position = startPosition;
    }
}
