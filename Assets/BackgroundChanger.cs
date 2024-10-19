using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public Sprite originalBackground;  // The original background sprite
    public Sprite newBackground;       // The new background sprite
    public SpriteRenderer backgroundRenderer;  // The SpriteRenderer of the background object

    public Transform newTriggerPosition;  // The position to move the trigger when in the new background
    public Transform originalTriggerPosition;  // The original trigger position

    private bool isUsingNewBackground = false;  // Track whether the new background is active
    private bool playerExited = true;  // Track if the player has exited the trigger

    private void Start()
    {
        // Set the trigger to its original position at the start
        transform.position = originalTriggerPosition.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerExited)  // Ensure the object is the player and they exited before entering again
        {
            // Toggle the background
            if (isUsingNewBackground)
            {
                backgroundRenderer.sprite = originalBackground;
                transform.position = originalTriggerPosition.position;  // Move trigger back to original position
            }
            else
            {
                backgroundRenderer.sprite = newBackground;
                transform.position = newTriggerPosition.position;  // Move trigger to new position
            }

            // Switch the flag to the opposite state
            isUsingNewBackground = !isUsingNewBackground;

            // Set playerExited to false to prevent immediate re-triggering
            playerExited = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Mark that the player has exited the trigger
            playerExited = true;
        }
    }
}
