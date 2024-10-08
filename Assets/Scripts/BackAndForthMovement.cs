using System.Collections;
using UnityEngine;

public class ForwardAndBackwardMovement : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f; // Distance to move forward and backward
    [SerializeField] private float moveSpeed = 5f; // Speed of movement

    private Vector3 startingPosition; // Starting position of the object
    private bool movingForward = true; // Track the direction of movement

    void Start()
    {
        startingPosition = transform.position; // Store the starting position
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Calculate the target position based on current movement direction
        Vector3 targetPosition = movingForward
            ? startingPosition + transform.forward * moveDistance
            : startingPosition;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingForward = !movingForward; // Toggle direction
            Debug.Log("Switched direction"); // Debugging output
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) // Ensure the projectile is tagged correctly
        {
            // Trigger the same movement behavior on collision
            StartCoroutine(ChangeMovement());
            Destroy(collision.gameObject); // Destroy the projectile
        }
    }

    private IEnumerator ChangeMovement()
    {
        // Stop moving for a short duration
        float originalMoveSpeed = moveSpeed;
        moveSpeed = 0f; // Stop the movement
        yield return new WaitForSeconds(1f); // Wait for 1 second
        moveSpeed = originalMoveSpeed; // Resume the movement
    }
}
