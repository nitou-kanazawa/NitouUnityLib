using UnityEditor;
using UnityEngine;

namespace nitou.Tools.Hierarchy.EditorSctipts {
    using nitou.Tools.Shared;

    public class HierarchyRowSeparatorDrawer : HierarchyDrawer{
        
        public override void OnGUI(int instanceID, Rect selectionRect) {

            var settings = nToolsSettings.GetOrCreateSettings();
            if (!settings.ShowSeparator) return;
            var rect = new Rect {y = selectionRect.y, width = selectionRect.width + selectionRect.x, height = 1, x = 0};

            EditorGUI.DrawRect(rect, settings.SeparatorColor);

            if (!settings.ShowRowShading) return;
            selectionRect.width += selectionRect.x;
            selectionRect.x = 0;
            selectionRect.height -= 1;
            selectionRect.y += 1;
            EditorGUI.DrawRect(selectionRect, Mathf.FloorToInt((selectionRect.y - 4) / 16 % 2) == 0 ? settings.EvenRowColor : settings.OddRowColor);
        }
    }
}