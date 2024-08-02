using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [参考]
//  _: フィールドの値などをインスペクタで横に並べて表示する   http://fantom1x.blog130.fc2.com/blog-entry-419.html

namespace nitou {

    [System.Serializable]
    public class ClampedValue{

        [SerializeField] float _min = 0;
        [SerializeField] float _max = 1;
        [SerializeField] float _value;

        public float Min => _min;
        public float Max => _max;

        public float Value {
            get => _value;
            set => _value = Mathf.Clamp(value, _min, _max);
        }

        private ClampedValue() { }

        public ClampedValue(float min, float max, float value = 0) {
            _min = min;
            _max = Mathf.Max(min,max);  // ※minを基準とする
            
            Value = value;
        }

        public static implicit operator float(ClampedValue clampedValue) {
            return clampedValue.Value;
        }
        
    }
}


/// ----------------------------------------------------------------------------
#if UNITY_EDITOR
namespace nitou.EditorScripts {

    [CustomPropertyDrawer(typeof(ClampedValue))]
    public class ClampedValueEditor : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            // Field
            var minProperty = property.FindPropertyRelative("_min");
            var maxProperty = property.FindPropertyRelative("_max");
            var valueProperty = property.FindPropertyRelative("_value");

            ValidateRange(minProperty, maxProperty);

            //    EditorGUI.BeginProperty(position, label, property);
            using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
                //名前
                Rect fieldRect = EditorGUI.PrefixLabel(position, label);

                /*
                //ラベル
                fieldRect.width *= 1f / 3f; //3つ並べる場合 (n 個のとき、1 / n)
                EditorGUI.indentLevel = 0;
                EditorGUIUtility.labelWidth = 30f;  //ラベル幅(適当)

                //各要素
                //EditorGUI.PropertyField(fieldRect, minProperty, minContent);
                GUI.Label(fieldRect, $"min: {minProperty.floatValue}");

                fieldRect.x += fieldRect.width;
                */
                valueProperty.floatValue = EditorGUI.Slider(fieldRect, valueProperty.floatValue, minProperty.floatValue, maxProperty.floatValue);
                //EditorGUI.PropertyField(fieldRect, valueProperty);
                //valueProperty.floatValue = Mathf.Clamp(newValue, minProperty.floatValue, maxProperty.floatValue);

                /*

                fieldRect.x += fieldRect.width;
                //EditorGUI.PropertyField(fieldRect, maxProperty, maxContent);
                GUI.Label(fieldRect, $"max: {maxProperty.floatValue}");
               */
            }


        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }

        private void ValidateRange(SerializedProperty minProperty,SerializedProperty maxProperty) {

            maxProperty.floatValue = Mathf.Max(minProperty.floatValue, maxProperty.floatValue);
        }
    }
}
#endif
