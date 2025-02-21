using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float speedIncreaseFactor = 0.5f;
    [SerializeField] private Vector2 direction = Vector2.zero;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D circleCollider;

    void Start()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        direction = GetRandomDirection();
        if (speed > maxSpeed || speed < minSpeed)
        {
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed); // Clamp speed to min and max values
        }
    }

    void Update()
    {

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
        if (speed < 10)
        {
            direction.y += UnityEngine.Random.Range(-0.2f, 0.2f);
            direction = direction.normalized;

        }
        else
        {
            direction.y += UnityEngine.Random.Range(-0.4f, 0.4f);
            direction = direction.normalized;

        }
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
            randomDir = UnityEngine.Random.insideUnitCircle.normalized;
        }
        while (Mathf.Abs(randomDir.x) < 0.5f || Mathf.Abs(randomDir.y) > 0.7f); // Avoid too vertical movement, the absolute value mathf.abs of both -3.5 and 3.5 is 3.5.
        return randomDir;
    }
}
