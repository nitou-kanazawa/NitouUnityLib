#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace nitou.Tools.TodoTask {

    public class TodoListWindow : EditorWindow {
        private TaskController sharedTaskController;
        private TaskController projectTaskController;
        private string newTaskName = "";
        private int selectedTab = 0;
        private readonly string[] tabs = { "Shared Tasks", "Project Tasks" };

        [MenuItem("Tools/ToDo List")]
        public static void ShowWindow() {
            GetWindow<TodoListWindow>("ToDo List");
        }

        private void OnEnable() {
            sharedTaskController = new TaskController(new SharedTaskRepository());
            projectTaskController = new TaskController(new ProjectTaskRepository());
        }

        private void OnGUI() {
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);

            TaskController activeController = selectedTab == 0 ? sharedTaskController : projectTaskController;

            EditorGUILayout.LabelField(tabs[selectedTab], EditorStyles.boldLabel);

            for (int i = 0; i < activeController.Tasks.Count; i++) {
                EditorGUILayout.BeginHorizontal();

                activeController.Tasks[i].isDone = EditorGUILayout.Toggle(activeController.Tasks[i].isDone, GUILayout.Width(20));
                activeController.Tasks[i].name = EditorGUILayout.TextField(activeController.Tasks[i].name);

                if (GUILayout.Button("Remove", GUILayout.Width(60))) {
                    activeController.RemoveTask(i);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            newTaskName = EditorGUILayout.TextField(newTaskName);

            if (GUILayout.Button("Add Task", GUILayout.Width(100))) {
                activeController.AddTask(newTaskName);
                newTaskName = "";
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            if (GUILayout.Button("Save Tasks")) {
                activeController.SaveTasks();
            }
        }
    }

}
#endif