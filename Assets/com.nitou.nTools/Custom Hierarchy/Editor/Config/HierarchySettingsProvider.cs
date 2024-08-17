#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace nitou.Tools.Hierarchy.EditorSctipts {
    using nitou.EditorShared;

    public class HierarchySettingsProvider : SettingsProvider {

        // 設定のパス (※第1階層は「Preferences」にする)
        private const string SettingPath = "Project/_Nitou/Hierarchy";

        private Editor _editor;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HierarchySettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords) { }

        /// <summary>
        /// このメソッドが重要です
        /// 独自のSettingsProviderを返すことで、設定項目を追加します
        /// </summary>
        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider() {
            // ※第三引数のkeywordsは、検索時にこの設定項目を引っかけるためのキーワード
            return new HierarchySettingsProvider(SettingPath, SettingsScope.Project, null);
        }



        public override void OnActivate(string searchContext, VisualElement rootElement) {

            var preferences = HierarchySettingsSO.instance;

            // ※ScriptableSingletonを編集可能にする
            preferences.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            // 設定ファイルの標準のインスペクターのエディタを生成
            Editor.CreateCachedEditor(preferences, null, ref _editor);
        }


        public override void OnGUI(string searchContext) {

            EditorGUI.BeginChangeCheck();

            // 設定ファイルの標準インスペクタを表示
            _editor.OnInspectorGUI();

            //EditorGUILayout.LabelField("テストだよ");

            if (EditorGUI.EndChangeCheck()) {
                HierarchySettingsSO.instance.Save();
            }
        }

    }
}
#endif