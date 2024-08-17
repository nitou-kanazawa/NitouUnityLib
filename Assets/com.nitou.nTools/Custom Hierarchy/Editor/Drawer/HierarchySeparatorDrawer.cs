using UnityEditor;
using UnityEngine;

namespace nitou.Tools.Hierarchy.EditorSctipts {

    public sealed class HierarchySeparatorDrawer : HierarchyDrawer{

        private static Color SeparatorColor => new(0.5f, 0.5f, 0.5f);

        public override void OnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) return;
            if (!gameObject.TryGetComponent<HierarchySeparator>(out _)) return;

            DrawBackground(instanceID, selectionRect);

            var lineRect = selectionRect.AddY(selectionRect.height * 0.5f).AddXMax(14f).SetHeight(1f);
            EditorGUI.DrawRect(lineRect, SeparatorColor);
        }
    }
}