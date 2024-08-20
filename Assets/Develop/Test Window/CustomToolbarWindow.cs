using UnityEditor;
using UnityEngine;

public class CustomToolbarWindow : EditorWindow {

    // Define the toolbar options
    private string[] toolbarOptions = { "Option 1", "Option 2", "Option 3" };
    private int selectedOption = 0;

    // Add a menu item to show the window
    [MenuItem("Window/Custom Toolbar Window")]
    public static void ShowWindow() {
        // Show the window
        GetWindow<CustomToolbarWindow>("Toolbar Example");
    }

    void OnGUI() {
        // Draw the toolbar
        selectedOption = GUILayout.Toolbar(selectedOption, toolbarOptions);

        // Display different content based on the selected toolbar option
        switch (selectedOption) {
            case 0:
                GUILayout.Label("Option 1 Selected");
                break;
            case 1:
                GUILayout.Label("Option 2 Selected");
                break;
            case 2:
                GUILayout.Label("Option 3 Selected");
                break;
        }
    }
}

