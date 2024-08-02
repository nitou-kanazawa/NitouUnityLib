using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [参考]
// ねこじゃらシティ: RectTransformのサイズをスクリプトから変更する https://nekojara.city/unity-rect-transform-size

namespace nitou.DebugScripts{

    /// <summary>
    /// <see cref="RectTransform"/>の各プロパティを可視化するためのデバッグ用コンポーネント
    /// </summary>
    public class RectTransformDebugger : MonoBehaviour {

        private RectTransform _rectTrans;
        public RectTransform RectTrans => _rectTrans;

        void OnValidate() {
            _rectTrans = gameObject.GetComponent<RectTransform>();
        }
    }


#if UNITY_EDITOR

    [CustomEditor(typeof(RectTransformDebugger))]
    public class RectTransformDebuggerEditor : Editor{

        // 内部処理用
        private static readonly Vector3[] _corners = new Vector3[4];


        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            RectTransformDebugger debugger = (RectTransformDebugger)target;
            var rectTrans = debugger.RectTrans;

            if (rectTrans != null) {
                EditorGUILayout.LabelField("RectTransform Properties", EditorStyles.boldLabel);

                DrawPoisitonProperty(rectTrans);

                EditorGUILayout.Space();
                DrawSizeProperty(rectTrans);

                EditorGUILayout.Space();
                
                EditorGUILayout.Vector2Field("Anchored Position", rectTrans.anchoredPosition);
                EditorGUILayout.Vector2Field("Pivot", rectTrans.pivot);
                EditorGUILayout.Vector2Field("Anchor Min", rectTrans.anchorMin);
                EditorGUILayout.Vector2Field("Anchor Max", rectTrans.anchorMax);
            } else {
                EditorGUILayout.HelpBox("Please assign a RectTransform.", MessageType.Warning);
            }
        }


        // ----------------------------------------------------------------------------
        // Private Method ()

        private static void DrawPoisitonProperty(RectTransform rectTrans) {
            EditorGUILayout.Space();
            
            // Local
            EditorGUILayout.Vector3Field("Local Position", rectTrans.localPosition);
            EditorGUILayout.Vector3Field("Local Rotation", rectTrans.localRotation.eulerAngles);
            EditorGUILayout.Vector3Field("Local Scale", rectTrans.localScale);

            // Global
            EditorGUILayout.Vector3Field("Global Position", rectTrans.position);
            EditorGUILayout.Vector3Field("Global Rotation", rectTrans.rotation.eulerAngles);


            EditorGUILayout.Space();

            // Local Corners
            EditorGUILayout.LabelField("Local Corners");
            rectTrans.GetLocalCorners(_corners);
            for (int i = 0; i < _corners.Length; i++) {
                EditorGUILayout.Vector3Field($"Corner {i}", _corners[i]);
            }

            // World Corners
            EditorGUILayout.LabelField("World Corners");
            rectTrans.GetWorldCorners(_corners);
            for (int i = 0; i < _corners.Length; i++) {
                EditorGUILayout.Vector3Field($"Corner {i}", _corners[i]);
            }
        }

        private static void DrawSizeProperty(RectTransform rectTrans) {

            EditorGUILayout.Vector2Field("Rect Size", rectTrans.rect.size);
            rectTrans.sizeDelta = EditorGUILayout.Vector2Field("Size Delta", rectTrans.sizeDelta);

        }


    }
#endif
}