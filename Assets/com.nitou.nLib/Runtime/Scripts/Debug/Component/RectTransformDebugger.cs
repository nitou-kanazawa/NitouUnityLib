using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;
using nitou.EditorShared;
#endif

// [�Q�l]
// �˂������V�e�B: RectTransform�̃T�C�Y���X�N���v�g����ύX���� https://nekojara.city/unity-rect-transform-size

namespace nitou.DebugInternal {

    /// <summary>
    /// <see cref="RectTransform"/>�̊e�v���p�e�B���������邽�߂̃f�o�b�O�p�R���|�[�l���g
    /// </summary>
    [RequireComponent(typeof(RequireComponent))]
    internal class RectTransformDebugger : DebugComponent<RectTransform> {

        private RectTransform _rectTrans;
        public RectTransform RectTrans => _rectTrans;

        public bool showGui = true;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void OnValidate() {
            _rectTrans = gameObject.GetComponent<RectTransform>();
        }

#if UNITY_EDITOR
        private void OnGUI() {
            if (_rectTrans == null || !showGui) return;

            var rect = _rectTrans.GetScreenRect();

            EditorUtil.ScreenGUI.Box(rect);
            EditorUtil.ScreenGUI.AuxiliaryLine(rect.position, 2f, Colors.Gray);
            EditorUtil.ScreenGUI.Label(rect.position, rect.position.ToString());
        }
#endif
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(RectTransformDebugger))]
    internal class RectTransformDebuggerEditor : Editor {

        // �v�Z�p
        private static readonly Vector3[] _corners = new Vector3[4];

        // �`��p
        private AnimBool _positionAnim;
        private AnimBool _localCornersAnim;
        private AnimBool _globalCornersAnim;

        private void OnEnable() {
            EditorApplication.update += Repaint;

            _positionAnim = new AnimBool(false);
            _localCornersAnim = new AnimBool(false);
            _globalCornersAnim = new AnimBool(false);
            _positionAnim.valueChanged.AddListener(Repaint);
            _localCornersAnim.valueChanged.AddListener(Repaint);
            _globalCornersAnim.valueChanged.AddListener(Repaint);
        }

        private void OnDisable() {
            EditorApplication.update -= Repaint;
        }

        public override void OnInspectorGUI() {

            var instance = target as RectTransformDebugger;
            var rectTrans = instance.RectTrans;

            EditorUtil.GUI.MonoBehaviourField<RectTransformDebugger>(instance);

            if (rectTrans != null) {
                EditorGUILayout.LabelField("RectTransform Properties", EditorStyles.boldLabel);

                // Info
                DrawRectSizeInfo(rectTrans);

                // Properties
                DrawPoisitonProperty(rectTrans);
                DrawSizeProperty(rectTrans);
                DrawAnchorPivotProperty(rectTrans);

            } else {
                EditorGUILayout.HelpBox("Please assign a RectTransform.", MessageType.Warning);
            }
        }



        /// ----------------------------------------------------------------------------
        // Private Method (Inspector Drawer)

        private void DrawRectSizeInfo(RectTransform rectTransform) {
            using (new EditorGUILayout.VerticalScope(Styles.box))
            using (new EditorGUI.DisabledScope(true)) {
                // �r���[�|�[�g���W
                EditorGUILayout.Vector2Field("Viewport", rectTransform.GetViewportPos());
            }

        }

        /// <summary>
        /// �ʒu�֘A�̃v���p�e�B
        /// </summary>
        private void DrawPoisitonProperty(RectTransform rectTrans) {

            using (var group = new EditorUtil.GUI.FoldoutGroupScope("Transform Info", _positionAnim)) {
                if (group.Visible) {

                    // Local
                    rectTrans.localPosition = EditorGUILayout.Vector3Field("Local Position", rectTrans.localPosition);
                    rectTrans.eulerAngles = EditorGUILayout.Vector3Field("Local Rotation", rectTrans.localRotation.eulerAngles);
                    rectTrans.localScale = EditorGUILayout.Vector3Field("Local Scale", rectTrans.localScale);

                    EditorUtil.GUI.HorizontalLine();

                    // Global
                    rectTrans.position = EditorGUILayout.Vector3Field("Global Position", rectTrans.position);
                    rectTrans.eulerAngles = EditorGUILayout.Vector3Field("Global Rotation", rectTrans.rotation.eulerAngles);
                }
            }

            //EditorGUILayout.Space();

            // Local Corners
            rectTrans.GetLocalCorners(_corners);
            using (var group = new EditorUtil.GUI.FoldoutGroupScope("Local Corners", _localCornersAnim)) {
                if (group.Visible) {
                    for (int i = 0; i < _corners.Length; i++) {
                        EditorGUILayout.Vector3Field($"Corner {i}", _corners[i]);
                    }
                }
            }

            // World Corners
            rectTrans.GetWorldCorners(_corners);
            using (var group = new EditorUtil.GUI.FoldoutGroupScope("World Corners", _globalCornersAnim)) {
                if (group.Visible) {
                    for (int i = 0; i < _corners.Length; i++) {
                        EditorGUILayout.Vector3Field($"Corner {i}", _corners[i]);
                    }
                }
            }

            EditorGUILayout.Space();
        }

        /// <summary>
        /// �T�C�Y�֘A�̃v���p�e�B
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
                    EditorGUILayout.Vector2Field("Rect Pos", rectTrans.rect.position);
                    EditorGUILayout.Vector2Field("Rect Size", rectTrans.rect.size);
                    rectTrans.sizeDelta = EditorGUILayout.Vector2Field("Size Delta", rectTrans.sizeDelta);
                }

            }
            EditorGUILayout.Space();
        }

        /// <summary>
        /// �A���J�[�֘A�̃v���p�e�B
        /// </summary>
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