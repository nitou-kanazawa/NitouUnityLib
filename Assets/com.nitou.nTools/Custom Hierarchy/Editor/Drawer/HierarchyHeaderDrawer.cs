#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace nitou.Tools.Hierarchy.EditorSctipts {

    public sealed class HierarchyHeaderDrawer : HierarchyDrawer {

        static Color HeaderColor => EditorGUIUtility.isProSkin ? new(0.45f, 0.45f, 0.45f, 0.5f) : new(0.55f, 0.55f, 0.55f, 0.5f);
        static GUIStyle labelStyle;


        /// ----------------------------------------------------------------------------
        // Public Method

        public override void OnGUI(int instanceID, Rect selectionRect) {

            // ���x���ݒ�
            if (labelStyle == null) {
                labelStyle = new GUIStyle(EditorStyles.boldLabel) {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 11,
                };
            }

            // GameObject�擾
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) return;
            if (!gameObject.TryGetComponent<HierarchyHeader>(out _)) return;

            // �w�i�`��
            DrawBackground(instanceID, selectionRect);

            // �ŗL�`��
            var headerRect = selectionRect.AddXMax(14f).AddYMax(-1f);
            EditorGUI.DrawRect(headerRect, HeaderColor);
            EditorGUI.LabelField(headerRect, gameObject.name, labelStyle);
        }
    }
}
#endif