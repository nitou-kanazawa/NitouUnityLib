#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace nitou.EditorScripts.Drawers {
    using nitou.Inspector;

    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfAttributeDrawer : PropertyDrawer{


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var enableIf = (EnableIfAttribute)attribute;
            bool enabled = GetConditionResult(property, enableIf.ConditionName);

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = wasEnabled;
        }

        private bool GetConditionResult(SerializedProperty property, string conditionName) {
            // 条件がbool型のフィールドの場合
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionName);
            if (conditionProperty != null && conditionProperty.propertyType == SerializedPropertyType.Boolean) {
                return conditionProperty.boolValue;
            }

            // 条件がメソッドの場合
            var targetObject = property.serializedObject.targetObject;
            var conditionMethod = targetObject.GetType().GetMethod(conditionName,
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            if (conditionMethod != null && conditionMethod.ReturnType == typeof(bool)) {
                return (bool)conditionMethod.Invoke(targetObject, null);
            }

            Debug.LogWarning($"EnableIf: Condition '{conditionName}' not found or is not a boolean.");
            return true;
        }

    }
}
#endif