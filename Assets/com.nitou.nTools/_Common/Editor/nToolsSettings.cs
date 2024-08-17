using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace nitou.Tools.Shared {
    using nitou.Tools.Hierarchy;

    /// <summary>
    /// Alchemy project-level settings
    /// </summary>
    public sealed class nToolsSettings : ScriptableObject {

        static nToolsSettings _instance;
        
        static readonly string SettingsPath = "ProjectSettings/ToolsSettings.json";
        static readonly string SettingsMenuName = "Project/nTools";


        [SerializeField] HierarchyObjectMode hierarchyObjectMode = HierarchyObjectMode.RemoveInBuild;
        [SerializeField] bool showHierarchyToggles;
        [SerializeField] bool showComponentIcons;
        [SerializeField] bool showTreeMap;
        [SerializeField] Color treeMapColor = new(0.53f, 0.53f, 0.53f, 0.45f);
        [SerializeField] bool showSeparator;
        [SerializeField] bool showRowShading;
        [SerializeField] Color separatorColor = new(0.19f, 0.19f, 0.19f, 0f);
        [SerializeField] Color evenRowColor = new(0f, 0f, 0f, 0.07f);
        [SerializeField] Color oddRowColor = Color.clear;

        public HierarchyObjectMode HierarchyObjectMode => hierarchyObjectMode;
        public bool ShowHierarchyToggles => showHierarchyToggles;
        public bool ShowComponentIcons => showComponentIcons;
        public bool ShowTreeMap => showTreeMap;
        public Color TreeMapColor => treeMapColor;
        public bool ShowSeparator => showSeparator;
        public bool ShowRowShading => showRowShading;
        public Color SeparatorColor => separatorColor;
        public Color EvenRowColor => evenRowColor;
        public Color OddRowColor => oddRowColor;


        /// <summary>
        /// Get a cached instance. If the cache does not exist, returns a newly created one.
        /// </summary>
        public static nToolsSettings GetOrCreateSettings() {
            if (_instance != null) return _instance;

            if (File.Exists(SettingsPath)) {
                _instance = CreateInstance<nToolsSettings>();
                JsonUtility.FromJsonOverwrite(File.ReadAllText(SettingsPath), _instance);
            } else {
                _instance = CreateInstance<nToolsSettings>();
            }

            return _instance;
        }

        /// <summary>
        /// Save the settings to a file.
        /// </summary>
        public static void SaveSettings() {
            File.WriteAllText(SettingsPath, JsonUtility.ToJson(_instance, true));
        }

        [SettingsProvider]
        internal static SettingsProvider CreateSettingsProvider() {

            return new SettingsProvider(SettingsMenuName, SettingsScope.Project) {
                label = "Nitou Tools",
                keywords = new HashSet<string>(new[] { "Nitou, Inspector, Hierarchy" }),
                guiHandler = searchContext => {
                    var serializedObject = new SerializedObject(GetOrCreateSettings());

                    using (new EditorGUILayout.HorizontalScope()) {
                        GUILayout.Space(10f);
                        using (new EditorGUILayout.VerticalScope()) {
                            EditorGUILayout.LabelField("Hierarchy", EditorStyles.boldLabel);

                            using (var changeCheck = new EditorGUI.ChangeCheckScope()){
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchyObjectMode"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("showHierarchyToggles"), new GUIContent("Show Toggles"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("showComponentIcons"));
                                var showTreeMap = serializedObject.FindProperty("showTreeMap");
                                EditorGUILayout.PropertyField(showTreeMap);
                                if (showTreeMap.boolValue) {
                                    EditorGUI.indentLevel++;
                                    EditorGUILayout.PropertyField(serializedObject.FindProperty("treeMapColor"), new GUIContent("Color"));
                                    EditorGUI.indentLevel--;
                                }

                                var showSeparator = serializedObject.FindProperty("showSeparator");
                                EditorGUILayout.PropertyField(showSeparator, new GUIContent("Show Row Separator"));
                                if (showSeparator.boolValue) {
                                    EditorGUI.indentLevel++;
                                    EditorGUILayout.PropertyField(serializedObject.FindProperty("separatorColor"), new GUIContent("Color"));
                                    EditorGUI.indentLevel--;
                                    var showRowShading = serializedObject.FindProperty("showRowShading");
                                    EditorGUILayout.PropertyField(showRowShading);
                                    if (showRowShading.boolValue) {
                                        EditorGUI.indentLevel++;
                                        EditorGUILayout.PropertyField(serializedObject.FindProperty("evenRowColor"));
                                        EditorGUILayout.PropertyField(serializedObject.FindProperty("oddRowColor"));
                                        EditorGUI.indentLevel--;
                                    }
                                }

                                if (changeCheck.changed) {
                                    serializedObject.ApplyModifiedProperties();
                                    SaveSettings();
                                }
                            }
                        }
                    }
                },
            };
        }
    }
}