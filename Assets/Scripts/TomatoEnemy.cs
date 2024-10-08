using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TomatoEnemy : MonoBehaviour
{
    public KitchenObjectSO kitchenObjectSO; // Assign this in the inspector
    public event EventHandler OnPlayerGrabObject; // Event for when the player grabs the object
    [SerializeField] float RangedDistance = 5;

    private void Update()
    {
        // Check if the player is within range and press 'P'
        if (PlayerInRange() && Input.GetKeyDown(KeyCode.K))
        {
            // Assuming you have a reference to the player
            Player player = FindObjectOfType<Player>(); // Adjust this based on your player management
            if (player != null && !player.HasKitchenObject()) // Check if player has no kitchen object
            {
                // Spawn the kitchen object (the tomato)
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, transform.position, Quaternion.identity);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                OnPlayerGrabObject?.Invoke(this, EventArgs.Empty); // Notify any listeners
                Destroy(gameObject); // Destroy the Tomato Enemy after interaction
            }
        }
    }

    private bool PlayerInRange()
    {
        // Assuming you have a Player reference, or you can find it by tag
        Player player = FindObjectOfType<Player>(); // Adjust this based on your player management
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= RangedDistance; // Check if within 5 units
        }
        return false;
    }
}
