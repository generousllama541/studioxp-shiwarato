using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToChangeScene : MonoBehaviour
{
    [Tooltip("Drag the target scene here (file must be in the build settings)")]
    public Object targetScene; // Drag and drop the target scene here in the Inspector

    private Collider2D objectCollider;

    void Start()
    {
        // Get the Collider2D component (Box or Circle Collider)
        objectCollider = GetComponent<Collider2D>();
        if (objectCollider == null)
        {
            Debug.LogError("No Collider2D component found on this object! Please add a BoxCollider2D or CircleCollider2D.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && objectCollider != null)
        {
            // Cast a ray from the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // If the ray hits this object
            if (hit.collider != null && hit.collider == objectCollider)
            {
                ChangeScene();
            }
        }
    }

    private void ChangeScene()
    {
        if (targetScene != null)
        {
            SceneManager.LoadScene(targetScene.name);
        }
        else
        {
            Debug.LogError("Target scene is not assigned!");
        }
    }
}
