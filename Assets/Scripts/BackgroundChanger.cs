using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public Sprite newBackground;  // The background to switch to
    public SpriteRenderer backgroundRenderer;  // The SpriteRenderer of the background object

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ensure the object is the player
        {
            // Change the sprite of the background
            backgroundRenderer.sprite = newBackground;

            // Deactivate the trigger to prevent repeated activation
            gameObject.SetActive(false);
        }
    }
}
