using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou.Inspector {

    /// <summary>
	/// ReadOnlyèÛë‘Ç…Ç∑ÇÈInspectorëÆê´
	/// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ReadOnlyAttribute : MultiPropertyAttribute {

#if UNITY_EDITOR
        public override void OnPreGUI(Rect position, SerializedProperty property) {
            GUI.enabled = false;
        }

        public override void OnPostGUI(Rect position, SerializedProperty property, bool changed) {
            GUI.enabled = true;
        }

        public override bool IsVisible(SerializedProperty property) {
            return true;
        }
#endif
    }
}
