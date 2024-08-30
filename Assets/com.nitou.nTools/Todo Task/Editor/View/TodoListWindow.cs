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
        private Vector2 scrollPos;


        /// ----------------------------------------------------------------------------
        // Edtor Method

        [MenuItem(MenuItemName.Prefix.EditorTool + "Todo List")]
        public static void ShowWindow() {
            GetWindow<TodoListWindow>("Todo List");
        }

        private void OnEnable() {
            sharedTaskController = new TaskController(new SharedTaskRepository());
            projectTaskController = new TaskController(new ProjectTaskRepository());
        }

        private void OnGUI() {
            selectedTab = GUILayout.Toolbar(selectedTab, tabs);

            TaskController activeController = selectedTab == 0 ? sharedTaskController : projectTaskController;

            EditorGUILayout.LabelField(tabs[selectedTab], EditorStyles.boldLabel);

            // �X�N���[���r���[�̊J�n
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(200));

            for (int i = 0; i < activeController.Tasks.Count; i++) {
                EditorGUILayout.BeginHorizontal();

                activeController.Tasks[i].isDone = EditorGUILayout.Toggle(activeController.Tasks[i].isDone, GUILayout.Width(20));
                activeController.Tasks[i].name = EditorGUILayout.TextField(activeController.Tasks[i].name);

                if (GUILayout.Button("Remove", GUILayout.Width(60))) {
                    activeController.RemoveTask(i);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView(); // �X�N���[���r���[�̏I��

            GUILayout.FlexibleSpace(); // �E�C���h�E�̎c��X�y�[�X���󂯂�

            // �V�����^�X�N�̒ǉ��ƕۑ��{�^���������ɔz�u
            EditorGUILayout.BeginHorizontal();
            newTaskName = EditorGUILayout.TextField(newTaskName);

            if (GUILayout.Button("Add Task", GUILayout.Width(100))) {
                activeController.AddTask(newTaskName);
                newTaskName = "";
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Save Tasks")) {
                activeController.SaveTasks();
            }
        }

    }
}
#endif