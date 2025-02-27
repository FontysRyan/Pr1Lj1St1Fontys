using UnityEngine;
using System.Collections;

public class BotController : MonoBehaviour
{
    [SerializeField] private float maxTransformPositionY = 3.3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector3 startPosition = Vector3.zero;  

    private GameObject ballInstance;
    private Coroutine findBallCoroutine;

    void Start()
    {
        InitializePosition();
        StartFindBallRoutine(); 
    }

    void Update()
    {
        if (ballInstance == null)
        {
            StartFindBallRoutine(); // Restart search if ball is lost
            return; // Stop movement if there is no ball
        }

        float moveInput = ballInstance.transform.position.y - transform.position.y;
        Move(moveInput);
    }

    private void InitializePosition()
    {
        rb.position = startPosition; // Set the Rigidbody2D's position to the start position
    }

    private void Move(float moveInput)
    {
        Vector2 newPosition = rb.position + Vector2.up * moveInput * speed * Time.fixedDeltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -maxTransformPositionY, maxTransformPositionY);
        rb.MovePosition(newPosition);
    }

    public void ResetPosition()
    {
        rb.position = startPosition;
    }

    private void StartFindBallRoutine()
    {
        if (findBallCoroutine == null) // Only start if it's not already running
        {
            findBallCoroutine = StartCoroutine(FindBallRoutine());
        }
    }

    private IEnumerator FindBallRoutine()
    {
        while (ballInstance == null) // Only search while there is no ball
        {
            ballInstance = GameObject.FindGameObjectWithTag("Ball");
            yield return new WaitForSeconds(0.5f);
        }

        findBallCoroutine = null; // Reset coroutine reference when ball is found
    }
}
