using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntanPower : MonoBehaviour
{
    public int pointsToCollect = 2; // The number of points required to activate the intangibility power
    private bool isIntangible = false; // Flag to indicate if the player is currently intangible
    private int collectedPoints = 0; // The current number of collected points
    private SpriteRenderer playerSprite; // Reference to the player's sprite renderer component
    
    public float intangibleOpacity = 0.5f; // The opacity to set the player's sprite to when intangible
    
    void Start()
    {
        // Get the player's sprite renderer component
        playerSprite = GetComponent<SpriteRenderer>();
    }
    
    void OnTriggerEnter2D(BoxCollider2D other)
    {
        if (other.gameObject.CompareTag("intanPoints")) // Check if the player collides with a point object
        {
            collectedPoints++; // Increment the collected points counter
            Destroy(other.gameObject); // Destroy the point object
            
            // Check if the required number of points have been collected
            if (collectedPoints >= pointsToCollect && other.gameObject.CompareTag("wall"))
            {
                collectedPoints = 0; // Reset the collected points counter
                if (isIntangible)
                {
                isIntangible = false; // Set the intangible flag to false
                GetComponent<BoxCollider2D>().isTrigger = true;
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f); // Reset the player's sprite opacity
                
                // TODO: Add code to re-enable collision with enemies or other hazards
                }
                // GetComponent<Collider2D>().enabled = false;
                
            }
        }
        else if (other.gameObject.CompareTag("wall")) // Check if the player collides with a wall object
        {
            // Check if the intangibility power is active
            if (isIntangible)
            {
                isIntangible = false; // Set the intangible flag to false
                GetComponent<BoxCollider2D>().isTrigger = true;
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f); // Reset the player's sprite opacity
                
                // TODO: Add code to re-enable collision with enemies or other hazards
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall")) // Check if the player collides with a wall object
        {
            // Check if the required number of points have been collected
            if (collectedPoints >= pointsToCollect)
            {
                // Activate the intangibility power
                isIntangible = true; // Set the intangible flag to true
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<BoxCollider2D>().isTrigger = true;
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, intangibleOpacity); // Set the player's sprite opacity to the intangibleOpacity
                
                

            }
        }
        else if (isIntangible && other.gameObject.CompareTag("Enemy")) // Check if the player is currently intangible and collides with an enemy
        {
            // TODO: Add code to handle enemy collision while intangible
        }
    }
    
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall")) // Check if the player exits the wall object
        {
            // Deactivate the intangibility power
            isIntangible = false; // Set the intangible flag to false
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f); // Reset the player's sprite opacity
            
            // TODO: Add code to re-enable collision with enemies or other hazards
        }
    }
}
