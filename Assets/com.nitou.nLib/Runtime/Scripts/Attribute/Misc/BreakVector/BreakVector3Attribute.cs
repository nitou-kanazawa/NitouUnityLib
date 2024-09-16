using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou.Inspector {

    [System.AttributeUsage(
        System.AttributeTargets.Field,
        AllowMultiple = false,
        Inherited = true
    )]
    public class BreakVector3Attribute : PropertyAttribute {

        public string xLabel, yLabel, zLabel;

        public BreakVector3Attribute(string xLabel, string yLabel, string zLabel) {
            this.xLabel = xLabel;
            this.yLabel = yLabel;
            this.zLabel = zLabel;
        }
    }
}


#if UNITY_EDITOR

namespace nitou.Inspector.EditorScripts {

    [CustomPropertyDrawer(typeof(BreakVector3Attribute))]
    public class BreakVector3AttributeEditor : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var at = attribute as BreakVector3Attribute;

            Rect fieldRect = position;
            fieldRect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.BeginProperty(position, label, property);
            {

                float x = EditorGUI.FloatField(fieldRect, at.xLabel, property.vector3Value.x);
                fieldRect.y += fieldRect.height + 2f;

                float y = EditorGUI.FloatField(fieldRect, at.yLabel, property.vector3Value.y);
                fieldRect.y += fieldRect.height + 2f;

                float z = EditorGUI.FloatField(fieldRect, at.zLabel, property.vector3Value.z);

                // 値の更新
                property.vector3Value = new Vector3(x, y, z);
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return 3f * (EditorGUIUtility.singleLineHeight + 2f);
        }
    }
}

#endif
