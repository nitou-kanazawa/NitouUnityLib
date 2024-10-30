using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou.Inspector {

    /// <summary>
	/// 判定用の変数、またはメソッドに応じてEnable状態を設定するInspector属性
	/// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class EnableIfAttribute : MultiPropertyAttribute {

        public string ConditionName { get; private set; }
        public EnableIfAttribute(string conditionName) => this.ConditionName = conditionName;

#if UNITY_EDITOR
        public override void OnPreGUI(Rect position, SerializedProperty property) {
            var isEnabled = GetConditionResult(property);
            GUI.enabled &= isEnabled;
        }

        public override void OnPostGUI(Rect position, SerializedProperty property, bool changed) {
            GUI.enabled = true;
        }

        public override bool IsVisible(SerializedProperty property) {
            return true;
        }

        private bool GetConditionResult(SerializedProperty property) {
            // 条件がbool型のフィールドの場合
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(ConditionName);
            if (conditionProperty != null && conditionProperty.propertyType == SerializedPropertyType.Boolean) {
                return conditionProperty.boolValue;
            }

            // 条件がメソッドの場合
            var targetObject = property.serializedObject.targetObject;
            var conditionMethod = targetObject.GetType().GetMethod(ConditionName,
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            if (conditionMethod != null && conditionMethod.ReturnType == typeof(bool)) {
                return (bool)conditionMethod.Invoke(targetObject, null);
            }

            Debug.LogWarning($"EnableIf: Condition '{ConditionName}' not found or is not a boolean.");
            return true;
        }
#endif
    }
}
