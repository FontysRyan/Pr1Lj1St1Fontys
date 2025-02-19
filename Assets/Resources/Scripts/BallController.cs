using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float speedIncreaseFactor;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D circleCollider;

    void Start()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        direction = GetRandomDirection();
        ApplyVelocity();
    }

    void FixedUpdate()
    {
        ApplyVelocity();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ReflectBall(collision);
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            ReflectBall(collision);
            AdjustBounce();
            IncreaseSpeed();
        }
    }

    private void ApplyVelocity()
    {
        rb.linearVelocity = direction * speed;
    }

    private void ReflectBall(Collision2D collision)
    {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        ApplyVelocity();
    }

    private void AdjustBounce()
    {
        // prevent going straight up or down
        direction.x += Random.Range(-0.2f, 0.2f);
        direction = direction.normalized;
    }

    private void IncreaseSpeed()
    {
        speed = Mathf.Clamp(speed + speedIncreaseFactor, minSpeed, maxSpeed);
    }

    private Vector2 GetRandomDirection()
    {
        Vector2 randomDir;
        do
        {
            // Generate a random direction vector within a unit circle and normalize it
            randomDir = Random.insideUnitCircle.normalized;
        }
        while (Mathf.Abs(randomDir.x) < 0.4f || Mathf.Abs(randomDir.y) > 0.8f); // Avoid too vertical movement
        return randomDir;
    }
}
