using UnityEngine;
using System.Collections; // Required for IEnumerator

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnInterval = 2f; // Time interval between spawns
    private Coroutine spawnCoroutine; // Reference to the current spawning coroutine

    private void Start()
    {
        SetActive(true); // Start spawning when initialized
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        if (active)
        {
            // If the spawner is active, start the spawning coroutine
            if (spawnCoroutine == null)
            {
                spawnCoroutine = StartCoroutine(SpawnEnemies()); // Start the coroutine
            }
        }
        else
        {
            // If the spawner is inactive, stop the coroutine
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine); // Stop the existing coroutine
                spawnCoroutine = null; // Reset the reference
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) // Run indefinitely until manually stopped
        {
            Debug.Log("Spawning Enemy"); // Log every spawn
            Instantiate(enemyPrefab, transform.position, Quaternion.identity); // Spawn enemy
            yield return new WaitForSeconds(spawnInterval); // Wait for the next spawn
        }
    }
}
