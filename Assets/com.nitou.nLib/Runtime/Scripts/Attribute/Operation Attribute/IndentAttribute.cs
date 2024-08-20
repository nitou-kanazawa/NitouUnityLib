using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou.Inspector {

    /// <summary>
	/// ƒCƒ“ƒfƒ“ƒg‚ğİ’è‚·‚éInspector‘®«
	/// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IndentAttribute : MultiPropertyAttribute {

        public readonly int IndentLevel;
        public IndentAttribute(int indentLevel = 1) => this.IndentLevel = indentLevel;

#if UNITY_EDITOR
        public override void OnPreGUI(Rect position, SerializedProperty property) {
            EditorGUI.indentLevel += IndentLevel;
        }

        public override void OnPostGUI(Rect position, SerializedProperty property, bool changed) {
            EditorGUI.indentLevel -= IndentLevel;
        }

        public override bool IsVisible(SerializedProperty property) {
            return true;
        }
#endif
    }
}
