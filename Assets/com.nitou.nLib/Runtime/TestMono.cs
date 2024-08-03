using UnityEngine;
using UnityEditor;

namespace nitou {

    public enum TestType {
        A, B, C
    }


    public class TestMono : MonoBehaviour {
        public TestType testType;
    }


    [CustomEditor(typeof(TestMono))]
    public class TestScriptEditor : Editor {
        public override void OnInspectorGUI() {
            TestMono script = (TestMono)target;

            // 現在のGUIカラーを保存
            Color defaultColor = GUI.backgroundColor;

            EditorGUILayout.BeginHorizontal();

            // Aボタン
            GUI.backgroundColor = (script.testType == TestType.A) ? Color.green : defaultColor;
            if (GUILayout.Button("A")) {
                script.testType = TestType.A;
            }

            // Bボタン
            GUI.backgroundColor = (script.testType == TestType.B) ? Color.green : defaultColor;
            if (GUILayout.Button("B")) {
                script.testType = TestType.B;
            }

            // Cボタン
            GUI.backgroundColor = (script.testType == TestType.C) ? Color.green : defaultColor;
            if (GUILayout.Button("C")) {
                script.testType = TestType.C;
            }

            // GUIカラーを元に戻す
            GUI.backgroundColor = defaultColor;

            EditorGUILayout.EndHorizontal();

            // 変更があった場合にシリアライズされたオブジェクトをマーク
            if (GUI.changed) {
                EditorUtility.SetDirty(target);
            }
        }
    }

}
