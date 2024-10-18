#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.SceneManagement;


// [参考]
//  zenn: UXMLテンプレートでObjectFieldを使うときのタイプ指定方法 https://zenn.dev/murnana/articles/unity-uielements-objectfield


/// <summary>
/// 編集作業時のシーン切り替え機能を提供するEditorWindow
/// </summary>
public sealed class SceneSwitcherEditorWindow : EditorWindow {

    [SerializeField] VisualTreeAsset _rootVisualTreeAsset;

    // 
    private List<SceneAsset> _scenes;
    private ScrollView _scrollView;


    private static string FilePath => $"{Application.persistentDataPath}/_sceneLauncher.sav";


    /// ----------------------------------------------------------------------------
    // EditorWindow Method

    [MenuItem("Tools/Nitou/New Scene Switcher")]
    private static void Open() {
        SceneSwitcherEditorWindow wnd = GetWindow<SceneSwitcherEditorWindow>();
        wnd.titleContent = new GUIContent("New SceneSwitcher");
    }

    private void OnEnable() {
        if (_scenes == null) {
            _scenes = new List<SceneAsset>();
            Load();
        }
    }

    private void CreateGUI() {

        if(_rootVisualTreeAsset is null) {
            Debug.LogError("Error : root uxml file is null.");
            return;
        }

        _rootVisualTreeAsset.CloneTree(rootVisualElement);

        // 現在シーンの追加
        var addButton = rootVisualElement.Q<Button>("add_current_btn");
        addButton.clicked += AddCurrentScene;

        // ScrollView for Scene list
        _scrollView = new ScrollView();
        _scrollView.style.flexGrow = 1;
        _scrollView.style.flexShrink = 0;
        _scrollView.style.width = new StyleLength(new Length(100, LengthUnit.Percent));

        var content = rootVisualElement.Q<VisualElement>("Content");
        content.Add(_scrollView);

        // 初期リストの表示
        RefreshSceneList();
    }


    /// ----------------------------------------------------------------------------
    // Private Method

    private void AddCurrentScene() {
        Debug.Log("Add Current");

        var scene = EditorSceneManager.GetActiveScene();
        if (scene != null && scene.path != null &&
            _scenes.Find(s => AssetDatabase.GetAssetPath(s) == scene.path) == null) {

            // シーンアセットを取得
            var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
            if (asset != null && !_scenes.Contains(asset)) {
                _scenes.Add(asset);
                Save();
            }
        }
    }

    private void RefreshSceneList() {
        _scrollView.Clear();

        for (var i = 0; i < _scenes.Count; ++i) {
            var scene = _scenes[i];
            var path = AssetDatabase.GetAssetPath(scene);

            // 行要素
            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            row.style.width = new StyleLength(new Length(100, LengthUnit.Percent));  // 横幅を100%に設定


            var removeButton = new Button(() => {
                _scenes.Remove(scene);
                Save();
                RefreshSceneList();
            }) { text = "X" };
            removeButton.style.width = 20;
            row.Add(removeButton);

            var pingButton = new Button(() => {
                EditorGUIUtility.PingObject(scene);
            }) { text = "O" };
            pingButton.style.width = 20;
            row.Add(pingButton);

            var moveUpButton = new Button(() => {
                if (i > 0) {
                    var temp = _scenes[i];
                    _scenes[i] = _scenes[i - 1];
                    _scenes[i - 1] = temp;
                    Save();
                    RefreshSceneList();
                }
            }) { text = i > 0 ? "↑" : "　" };
            moveUpButton.style.width = 20;
            row.Add(moveUpButton);

            // 
            var sceneButton = new Button(() => {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
                    EditorSceneManager.OpenScene(path);
                }
            }) { text = Path.GetFileNameWithoutExtension(path)};
            sceneButton.style.flexGrow = 1;
            sceneButton.SetEnabled(!EditorApplication.isPlaying);
            row.Add(sceneButton);

            _scrollView.Add(row);
        }
    }


    /// ----------------------------------------------------------------------------
    // Private Method

    private void Save() {
        var guids = new List<string>();
        foreach (var scene in _scenes) {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(scene, out string guid, out long _)) {
                guids.Add(guid);
            }
        }

        var content = string.Join("\n", guids.ToArray());
        File.WriteAllText(FilePath, content);
    }

    private void Load() {
        _scenes.Clear();
        if (File.Exists(FilePath)) {
            string content = File.ReadAllText(FilePath);
            foreach (var guid in content.Split(new char[] { '\n' })) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
                if (scene != null)
                    _scenes.Add(scene);
            }
        }
    }
}

#endif