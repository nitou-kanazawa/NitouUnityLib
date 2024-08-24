#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace nitou.Tools.TodoTask {

    /// <summary>
    /// <see cref="TodoTask"/>‚ÌƒRƒ“ƒeƒi
    /// </summary>
    public interface ITaskRepository {

        List<TodoTask> LoadTasks();
        void SaveTasks(List<TodoTask> tasks);
    }


    public class SharedTaskRepository : ITaskRepository {        
        private const string SharedTasksKey = "SharedTasks";

        public List<TodoTask> LoadTasks() {    
            return EditorPrefs.HasKey(SharedTasksKey)
                ? JsonUtility.FromJson<List<TodoTask>>(EditorPrefs.GetString(SharedTasksKey))
                : new List<TodoTask>();
        }

        public void SaveTasks(List<TodoTask> tasks) {
            EditorPrefs.SetString(SharedTasksKey, JsonUtility.ToJson(tasks));
        }
    }


    public class ProjectTaskRepository : ITaskRepository {
        private string projectKey = "ProjectTasks_" + Application.productName;

        public List<TodoTask> LoadTasks() {
            return EditorPrefs.HasKey(projectKey)
                ? JsonUtility.FromJson<List<TodoTask>>(EditorPrefs.GetString(projectKey))
                : new List<TodoTask>();
        }

        public void SaveTasks(List<TodoTask> tasks) {
            EditorPrefs.SetString(projectKey, JsonUtility.ToJson(tasks));
        }
    }
}
#endif