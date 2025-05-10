using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropdownMenuController : MonoBehaviour, IPointerClickHandler
{
    // Reference to the dropdown menu and the UI panel containing the options
    public GameObject dropdownMenu;
    public Button[] optionButtons;  // Array of buttons for the options
    public Text displayText;        // Text to show the selected option (optional)

    private bool isMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the dropdown menu is hidden initially
        dropdownMenu.SetActive(false);

        // Add listeners to each button in the dropdown
        foreach (Button button in optionButtons)
        {
            button.onClick.AddListener(() => OnOptionClicked(button));
        }
    }

    // This method is called when the box collider is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleDropdownMenu();
    }

    // Toggle the dropdown menu visibility without removing the box collider
    void ToggleDropdownMenu()
    {
        isMenuOpen = !isMenuOpen;
        dropdownMenu.SetActive(isMenuOpen);
    }

    // Handle option button click
    void OnOptionClicked(Button clickedButton)
    {
        string optionText = clickedButton.GetComponentInChildren<Text>().text;

        // Optionally, display the selected option text somewhere (e.g., a UI text)
        if (displayText != null)
        {
            displayText.text = "You selected: " + optionText;
        }

        // Close the menu after an option is clicked
        ToggleDropdownMenu();
    }
}
