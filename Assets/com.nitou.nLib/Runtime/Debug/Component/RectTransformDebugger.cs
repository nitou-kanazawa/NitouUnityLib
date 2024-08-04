using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AnimatedValues;
#if UNITY_EDITOR
using UnityEditor;
using nitou.EditorShared;
#endif

// [参考]
// ねこじゃらシティ: RectTransformのサイズをスクリプトから変更する https://nekojara.city/unity-rect-transform-size

namespace nitou.DebugInternal {

    /// <summary>
    /// <see cref="RectTransform"/>の各プロパティを可視化するためのデバッグ用コンポーネント
    /// </summary>
    public class RectTransformDebugger : DebugComponent<RectTransform> {

        private RectTransform _rectTrans;
        public RectTransform RectTrans => _rectTrans;

        void OnValidate() {
            _rectTrans = gameObject.GetComponent<RectTransform>();
        }
    }


#if UNITY_EDITOR

    [CustomEditor(typeof(RectTransformDebugger))]
    public class RectTransformDebuggerEditor : Editor {

        // 計算用
        private static readonly Vector3[] _corners = new Vector3[4];

        // 描画用
        private AnimBool _localCornersAnim;
        private AnimBool _globalCornersAnim;

        private void OnEnable() {
            _localCornersAnim = new AnimBool(false);
            _globalCornersAnim = new AnimBool(false);
            _localCornersAnim.valueChanged.AddListener(Repaint);
            _globalCornersAnim.valueChanged.AddListener(Repaint);
        }

        public override void OnInspectorGUI() {

            RectTransformDebugger debugger = (RectTransformDebugger)target;
            var rectTrans = debugger.RectTrans;

            if (rectTrans != null) {
                EditorGUILayout.LabelField("RectTransform Properties", EditorStyles.boldLabel);

                DrawPoisitonProperty(rectTrans);
                DrawSizeProperty(rectTrans);
                DrawAnchorPivotProperty(rectTrans);

            } else {
                EditorGUILayout.HelpBox("Please assign a RectTransform.", MessageType.Warning);
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method ()

        /// <summary>
        /// 位置関連のプロパティ
        /// </summary>
        private void DrawPoisitonProperty(RectTransform rectTrans) {
            EditorUtil.GUI.HorizontalLine();

            // Local
            EditorGUILayout.Vector3Field("Local Position", rectTrans.localPosition);
            EditorGUILayout.Vector3Field("Local Rotation", rectTrans.localRotation.eulerAngles);
            EditorGUILayout.Vector3Field("Local Scale", rectTrans.localScale);

            // Global
            EditorGUILayout.Vector3Field("Global Position", rectTrans.position);
            EditorGUILayout.Vector3Field("Global Rotation", rectTrans.rotation.eulerAngles);

            EditorGUILayout.Space();

            // Local Corners
            rectTrans.GetLocalCorners(_corners);
            _localCornersAnim.target = EditorUtil.GUI.FoldoutHeader("Local Corners", _localCornersAnim.target);
            using (var group = new EditorGUILayout.FadeGroupScope(_localCornersAnim.faded))
            using (new EditorGUI.IndentLevelScope())
            using (new EditorGUI.DisabledScope(true)) {
                if (group.visible) {

                    for (int i = 0; i < _corners.Length; i++) {
                        EditorGUILayout.Vector3Field($"Corner {i}", _corners[i]);
                    }
                }
            }

            // World Corners
            EditorGUILayout.LabelField("World Corners");
            rectTrans.GetWorldCorners(_corners);
            using (new EditorGUI.IndentLevelScope())
            using (new EditorGUI.DisabledScope(true)) {
                for (int i = 0; i < _corners.Length; i++) {
                    EditorGUILayout.Vector3Field($"Corner {i}", _corners[i]);
                }
            }

            EditorGUILayout.Space();
        }

        /// <summary>
        /// サイズ関連のプロパティ
        /// </summary>
        private void DrawSizeProperty(RectTransform rectTrans) {
            EditorUtil.GUI.HorizontalLine();
            using (new EditorGUILayout.VerticalScope(Styles.box)) {


                Color labelColor = rectTrans.IsFixedSize() ? Colors.Orange : Colors.Cyan;
                string label = rectTrans.IsFixedSize() ? "Fixed Size" : "Flexible Size";
                using (new EditorUtil.GUIColorScope(labelColor)) {
                    EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
                }
                using (new EditorGUI.IndentLevelScope()) {
                    EditorGUILayout.Vector2Field("Rect Size", rectTrans.rect.size);
                    rectTrans.sizeDelta = EditorGUILayout.Vector2Field("Size Delta", rectTrans.sizeDelta);
                }

            }
            EditorGUILayout.Space();
        }

        private void DrawAnchorPivotProperty(RectTransform rectTrans) {
            EditorUtil.GUI.HorizontalLine();
            EditorGUILayout.Vector2Field("Anchor Min", rectTrans.anchorMin);
            EditorGUILayout.Vector2Field("Anchor Max", rectTrans.anchorMax);
            EditorGUILayout.Vector2Field("Anchored Position", rectTrans.anchoredPosition);
            EditorGUILayout.Vector2Field("Pivot", rectTrans.pivot);
        }


        /// ----------------------------------------------------------------------------
        // Private Method ()



        /// ----------------------------------------------------------------------------
        private static class Styles {

            public static readonly GUIStyle box;

            static Styles() {
                box = new GUIStyle(GUI.skin.box) {

                };
            }

        }
    }
#endif
}