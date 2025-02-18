using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float speedIncreaseFactor;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Rigidbody2D rb; // Handles physics-based movement
    [SerializeField] private CircleCollider2D circleCollider; // Handles collision detection

    void Start()
    {
        // Ensure Rigidbody2D is assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Ensure CircleCollider2D is assigned
        if (circleCollider == null)
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }

        // Set collision detection mode to Continuous
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Initialize the ball with a randomized direction that is not too vertical
        direction = GetRandomDirection();
        rb.linearVelocity = direction * speed;
    }

    void FixedUpdate()
    {
        // Ensure ball maintains consistent speed
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Ball hit the wall");
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            rb.linearVelocity = direction * speed;
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            Debug.Log("Ball hit the paddle");

            // Reflect the ball's movement
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);

            // Add slight randomness to prevent vertical bounces
            direction.x += Random.Range(-0.2f, 0.2f);
            direction = direction.normalized;

            // Gradually increase speed
            speed = speed + speedIncreaseFactor;

            rb.linearVelocity = direction * speed;
        }
    }

    private Vector2 GetRandomDirection()
    {
        Vector2 randomDir;
        do
        {
            randomDir = Random.insideUnitCircle.normalized;
        }
        while (Mathf.Abs(randomDir.x) < 0.4f); // Ensure it has enough horizontal movement
        return randomDir;
    }
}
