using System;
using UnityEngine;

// [参考] 
//  github: augustdominik/SimpleMinMaxSlider https://github.com/augustdominik/SimpleMinMaxSlider/blob/master/Assets/SimpleMinMaxSlider/Scripts/Editor/MinMaxSliderDrawer.cs

namespace nitou.Inspector {

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MinMaxSliderAttribute : PropertyAttribute {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public MinMaxSliderAttribute(float min, float max) {
            Min = min;
            Max = max;
        }
    }

}


#if UNITY_EDITOR
namespace nitou.EditorScripts.Drawers {
    using nitou.Inspector;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderDrawer : PropertyDrawer, IAttributePropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            var minMaxAttribute = (MinMaxSliderAttribute)attribute;
            var propertyType = property.propertyType;

            label.tooltip = $"{minMaxAttribute.Min.ToString("F2")} to {minMaxAttribute.Max.ToString("F2")}";

            //PrefixLabel returns the rect of the right part of the control. It leaves out the label section. We don't have to worry about it. Nice!
            Rect controlRect = EditorGUI.PrefixLabel(position, label);

            // スライダーの描画
            if (propertyType == SerializedPropertyType.Vector2) {
                DrawVector2Slider(controlRect, property, minMaxAttribute.Min, minMaxAttribute.Max);

            } else if (propertyType == SerializedPropertyType.Vector2Int) {
                DrawVector2IntSlider(controlRect, property, minMaxAttribute.Min, minMaxAttribute.Max);
            }

        }

        /// <summary>
        /// <see cref="Vector2"/>用のMinMaxスライダーを描画する
        /// </summary>
        private void DrawVector2Slider(Rect rect, SerializedProperty property, float min, float max) {
            Rect[] splittedRect = SplitRect(rect, 3);

            EditorGUI.BeginChangeCheck();

            Vector2 vector = property.vector2Value;
            float minVal = vector.x;
            float maxVal = vector.y;

            //F2 limits the float to two decimal places (0.00).
            GUI.SetNextControlName(property.name + "_min");
            minVal = EditorGUI.FloatField(splittedRect[0], float.Parse(minVal.ToString("F2")));
            maxVal = EditorGUI.FloatField(splittedRect[2], float.Parse(maxVal.ToString("F2")));

            EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal, min, max);

            if (minVal < min) minVal = min;
            if (maxVal > max) maxVal = max;

            vector = new Vector2(minVal > maxVal ? maxVal : minVal, maxVal);

            if (EditorGUI.EndChangeCheck()) {
                property.vector2Value = vector;
            }
        }

        /// <summary>
        /// <see cref="Vector2Int"/>用のMinMaxスライダーを描画する
        /// </summary>
        private void DrawVector2IntSlider(Rect rect, SerializedProperty property, float min, float max) {
            Rect[] splittedRect = SplitRect(rect, 3);

            EditorGUI.BeginChangeCheck();

            Vector2Int vector = property.vector2IntValue;
            float minVal = vector.x;
            float maxVal = vector.y;

            minVal = EditorGUI.FloatField(splittedRect[0], minVal);
            maxVal = EditorGUI.FloatField(splittedRect[2], maxVal);

            EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal, min, max);

            if (minVal < min) minVal = min;
            if (maxVal > max) maxVal = max;

            vector = new Vector2Int(Mathf.FloorToInt(minVal > maxVal ? maxVal : minVal), Mathf.FloorToInt(maxVal));

            if (EditorGUI.EndChangeCheck()) {
                property.vector2IntValue = vector;
            }
        }

        private Rect[] SplitRect(Rect rectToSplit, int n) {


            Rect[] rects = new Rect[n];

            for (int i = 0; i < n; i++) {

                rects[i] = new Rect(rectToSplit.position.x + (i * rectToSplit.width / n), rectToSplit.position.y, rectToSplit.width / n, rectToSplit.height);

            }

            int padding = (int)rects[0].width - 40;
            int space = 5;

            rects[0].width -= padding + space;
            rects[2].width -= padding + space;

            rects[1].x -= padding;
            rects[1].width += padding * 2;

            rects[2].x += padding + space;


            return rects;
        }
    }

}
#endif