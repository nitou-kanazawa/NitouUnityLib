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

            // ÉâÉxÉãê›íË
            if (labelStyle == null) {
                labelStyle = new GUIStyle(EditorStyles.boldLabel) {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 11,
                };
            }

            // GameObjectéÊìæ
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) return;
            if (!gameObject.TryGetComponent<HierarchyHeader>(out _)) return;

            // îwåiï`âÊ
            DrawBackground(instanceID, selectionRect);

            // å≈óLï`âÊ
            var headerRect = selectionRect.AddXMax(14f).AddYMax(-1f);
            EditorGUI.DrawRect(headerRect, HeaderColor);
            EditorGUI.LabelField(headerRect, gameObject.name, labelStyle);
        }
    }
}
#endif