using System.Collections;
using UnityEngine;

public class TomatoEnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Speed of the enemy
    private float originalMoveSpeed; // Store original move speed
    private Coroutine speedReductionCoroutine; // To manage speed reduction timing

    [SerializeField] float boundaryWidth = 10f; // Width of movement boundary
    [SerializeField] float boundaryHeight = 10f; // Height of movement boundary

    [SerializeField] float raycastDistance = 1f; // Distance for raycasting
    private Vector3 targetDirection; // Direction the enemy is currently moving

    [SerializeField] float changeDirectionTime = 2f; // Time before changing direction
    private Renderer enemyRenderer; // Reference to the enemy's Renderer component
    private Color originalColor; // Store original color of the enemy

    void Start()
    {
        originalMoveSpeed = moveSpeed; // Store the original speed
        enemyRenderer = GetComponent<Renderer>(); // Get the Renderer component for color change
        originalColor = enemyRenderer.material.color; // Store the original color
        ChangeDirection();
        InvokeRepeating(nameof(ChangeDirection), changeDirectionTime, changeDirectionTime);
    }

    void Update()
    {
        Move();
        AvoidObstacles();
    }

    void Move()
    {
        // Move the enemy in the target direction
        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);
    }

    void AvoidObstacles()
    {
        RaycastHit hit;

        // Check in the direction of movement
        if (Physics.Raycast(transform.position, targetDirection, out hit, raycastDistance))
        {
            ChangeDirection(); // Change direction if there’s an obstacle
        }

        // Check for boundaries
        if (Mathf.Abs(transform.position.x) > boundaryWidth / 2 || Mathf.Abs(transform.position.z) > boundaryHeight / 2)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        // Generate a random direction
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        targetDirection = new Vector3(randomX, 0, randomZ).normalized;
    }

    public void ReduceSpeed(float percentage, float duration)
    {
        if (speedReductionCoroutine != null)
        {
            StopCoroutine(speedReductionCoroutine); // Stop any existing coroutine
        }
        speedReductionCoroutine = StartCoroutine(ReduceSpeedCoroutine(percentage, duration));
    }

    private IEnumerator ReduceSpeedCoroutine(float percentage, float duration)
    {
        moveSpeed *= (1 - percentage); // Reduce the speed
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        moveSpeed = originalMoveSpeed; // Restore original speed
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) // Ensure the projectile is tagged correctly
        {
            ReduceSpeed(0.80f, 3f); // Reduce speed by 80% for 3 seconds

            // Change the enemy's color to blue
            ChangeColor(Color.blue);

            // Start coroutine to reset color after duration
            StartCoroutine(ResetColor(3f));

            Destroy(collision.gameObject); // Destroy the projectile
        }
    }

    private void ChangeColor(Color newColor)
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = newColor; // Change the enemy's color
        }
    }

    private IEnumerator ResetColor(float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        ChangeColor(originalColor); // Change back to original color
    }

    private void OnDrawGizmos()
    {
        // Draw boundary gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(boundaryWidth, 1, boundaryHeight));
    }
}
    