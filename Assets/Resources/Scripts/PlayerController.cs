using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxTransformPostionY;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private string inputAxis = "Vertical"; // Input axis name
    [SerializeField] private float speed = 5f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis(inputAxis);
        Move(moveInput);
    }

    public void Move(float moveInput)
    {
        Vector2 newPosition = rb.position + Vector2.up * moveInput * speed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -maxTransformPostionY, maxTransformPostionY); // kijkt ofdat je niet over de position gaat van maxTransformPostionY
        rb.MovePosition(newPosition);
    }

    public void ResetPosition()
    {
        rb.position = startPosition; // terugzetten naar de startpositie
    }
}
