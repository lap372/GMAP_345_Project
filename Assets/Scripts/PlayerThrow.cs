using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Assign the projectile prefab in the Inspector
    [SerializeField] private float throwForce = 10f; // Force with which the projectile is thrown
    private Player player; // Reference to the Player component

    private void Start()
    {
        player = GetComponent<Player>(); // Get the Player component
        if (player == null)
        {
            Debug.LogError("Player component not found!");
        }
    }

    private void Update()
    {
        // Check for input to throw the kitchen object
        if (Input.GetKeyDown(KeyCode.O))
        {
            ThrowKitchenObject();
        }
    }

    private void ThrowKitchenObject()
    {
        // Check if the player has a kitchen object
        if (player.HasKitchenObject())
        {
            // Get the kitchen object and its visual representation
            KitchenObject kitchenObject = player.GetKitchenObject();
            GameObject kitchenObjectVisual = kitchenObject.gameObject; // Assuming the kitchen object itself is the visual

            // Remove the kitchen object from the player
            player.ClearKitchenObject();

            // Hide the visual representation
            kitchenObjectVisual.SetActive(false); // Hiding the visual

            // Spawn the projectile at the player's hold point
            Transform spawnPoint = player.GetKitchenObjectFollowTransform();
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

            // Add force to the projectile in the direction the player is facing
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody component!");
            }
        }
    }
}
