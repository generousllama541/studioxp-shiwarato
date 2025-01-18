using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; // This is necessary for accessing Editor functionality
#endif

public class ClickToChangeScene : MonoBehaviour
{
    [Tooltip("Drag the target scene here (file must be in the build settings)")]
    public Object targetScene; // Drag and drop the target scene here in the Inspector

    [SerializeField]
    [Tooltip("Check this box to quit the application on click instead of changing the scene.")]
    private bool quitApplication = false; // Adds a checkbox in the Inspector

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
                if (quitApplication)
                {
                    QuitApplication();
                }
                else
                {
                    ChangeScene();
                }
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

    private void QuitApplication()
    {
        Debug.Log("Quitting application.");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stops play mode in the Editor
#else
        Application.Quit(); // Quits the application in a built version
#endif
    }
}
