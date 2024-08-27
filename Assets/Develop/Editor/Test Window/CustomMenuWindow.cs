using UnityEditor;
using UnityEngine;

public class CustomMenuWindow : EditorWindow {

    private string[] tabs = { "Log", "Warnings", "Errors" };
    private int selectedTab = 0;

    private bool collapse = false;
    private bool clearOnPlay = true;

    [MenuItem("Window/Custom Menu Window")]
    public static void ShowWindow() {
        GetWindow<CustomMenuWindow>("Custom Menu");
    }

    void OnGUI() {
        // タブメニューの作成
        selectedTab = GUILayout.Toolbar(selectedTab, tabs, GUILayout.MinHeight(20));

        // その下に配置されるボタンやトグル
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        collapse = GUILayout.Toggle(collapse, "Collapse", EditorStyles.toolbarButton);
        clearOnPlay = GUILayout.Toggle(clearOnPlay, "Clear on Play", EditorStyles.toolbarButton);
        if (GUILayout.Button("Clear", EditorStyles.toolbarButton)) {
            Debug.Log("Clear button pressed");
        }
        EditorGUILayout.EndHorizontal();

        // タブごとの表示内容
        switch (selectedTab) {
            case 0:
                GUILayout.Label("Logs are displayed here.");
                break;
            case 1:
                GUILayout.Label("Warnings are displayed here.");
                break;
            case 2:
                GUILayout.Label("Errors are displayed here.");
                break;
        }
    }
}
