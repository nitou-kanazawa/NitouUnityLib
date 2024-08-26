using UnityEngine;
using UnityEditor;

namespace nitou.Tools{
    using nitou.EditorShared;

    public class DataClassGeneratorWindow : EditorWindow{

        private string _directoryPath = "";
        private string _resultText = "";

        [MenuItem(MenuItemName.Prefix.Develop + "Test Window")]
        public static void Open() {
            GetWindow<DataClassGeneratorWindow>();
        }


        public void OnGUI() {
            GUILayout.Label("Asset Loader", EditorStyles.boldLabel);

            EditorGUILayout.LabelField(_directoryPath);

            if (GUILayout.Button("Get Path")) {

                var path = AssetPath.FromRelativePath(_directoryPath);
                Debug_.Log(path.ToAssetDatabasePath());
                Debug_.Log(path.ToAbsolutePath());



            }

            GUILayout.Label("Result:", EditorStyles.boldLabel);
            GUILayout.TextArea(_resultText, GUILayout.Height(100));
        }
    }
}
