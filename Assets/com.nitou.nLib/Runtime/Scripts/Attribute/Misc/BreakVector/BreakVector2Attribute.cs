using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou.Inspector {

    /// <summary>
    /// Vector2の各成分 (x,y) を独立してインスペクタ表示させる属性
    /// </summary>
    [System.AttributeUsage(
        System.AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true
    )]
    public class BreakVector2Attribute : PropertyAttribute {

        public string xLabel, yLabel;

        public BreakVector2Attribute(string xLabel, string yLabel) {
            this.xLabel = xLabel;
            this.yLabel = yLabel;
        }
    }
}


#if UNITY_EDITOR

namespace nitou.Inspector.EditorScripts {

    [CustomPropertyDrawer(typeof(BreakVector2Attribute))]
    public class BreakVector2AttributeEditor : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var at = attribute as BreakVector2Attribute;

            Rect fieldRect = position;
            fieldRect.height = EditorGUIUtility.singleLineHeight;


            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            {
                float x = EditorGUI.FloatField(fieldRect, at.xLabel, property.vector2Value.x);

                fieldRect.y += fieldRect.height + 2f;
                float y = EditorGUI.FloatField(fieldRect, at.yLabel, property.vector2Value.y);

                // 値の更新
                property.vector2Value = new Vector2(x, y);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return 2f * (EditorGUIUtility.singleLineHeight + 2f);
        }
    }
}
#endif
