using System;
using UnityEngine;

namespace nitou.Inspector {

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumToggleAttribute : PropertyAttribute {

        public EnumToggleAttribute() { }
    }

}


#if UNITY_EDITOR
namespace nitou.EditorScripts.Drawers {
    using nitou.Inspector;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(EnumToggleAttribute))]
    public class EnumToggleDrawer : PropertyDrawer, IAttributePropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            if (property.propertyType == SerializedPropertyType.Enum) {
                DrawEnumToggles(position, property, label);
            } else {
                EditorGUI.PropertyField(position, property, label);
            }

        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void DrawEnumToggles(Rect position, SerializedProperty property, GUIContent label) {

            // Enum のフィールドをトグルボタンとして表示
            using (new EditorGUI.PropertyScope(position, label, property)) {

                EditorGUI.LabelField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height), label);

                EditorGUI.BeginChangeCheck();
                int value = property.enumValueIndex;
                string[] enumNames = property.enumDisplayNames;

                int buttonWidth = Mathf.RoundToInt((position.width - EditorGUIUtility.labelWidth) / enumNames.Length);
                for (int i = 0; i < enumNames.Length; i++) {
                    Rect buttonRect = new Rect(position.x + EditorGUIUtility.labelWidth + i * buttonWidth, position.y, buttonWidth, position.height);
                    if (GUI.Toggle(buttonRect, value == i, enumNames[i], "Button")) {
                        value = i;
                    }
                }

                if (EditorGUI.EndChangeCheck()) {
                    property.enumValueIndex = value;
                }

            }
        }
    }


}
#endif