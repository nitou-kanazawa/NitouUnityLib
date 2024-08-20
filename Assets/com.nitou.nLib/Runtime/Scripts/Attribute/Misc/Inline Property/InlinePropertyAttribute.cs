using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using nitou.EditorShared;
#endif

// [参考]
//  _: InlinePropertyAttribute class  https://odininspector.com/documentation/sirenix.odininspector.inlinepropertyattribute

namespace nitou {

    [AttributeUsage(
        AttributeTargets.Field, 
        Inherited = true, 
        AllowMultiple = false)
    ]
    public class InlinePropertyAttribute : PropertyAttribute {
        public InlinePropertyAttribute() { }
    }
}


#if UNITY_EDITOR
namespace nitou.EditorScripts {

    [CustomPropertyDrawer(typeof(InlinePropertyAttribute))]
    public class InlinePropertyDrawer : PropertyDrawer {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            // プロパティの全てのフィールドの高さを計算
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            /*
            // 全体の高さを計算
            float totalHeight = position.y;

            // ラベルとフィールドの位置を設定
            Rect fieldPosition = new Rect(position.x, totalHeight, position.width, EditorGUIUtility.singleLineHeight);

            // プロパティを再帰的に走査
            SerializedProperty currentProperty = property.Copy();
            SerializedProperty nextSiblingProperty = property.Copy();
            nextSiblingProperty.NextVisible(false); // 最初の次の兄弟プロパティを取得

            // 現在のプロパティを走査し、そのプロパティが兄弟プロパティを越えるまで続ける
            bool enterChildren = true;

            using (new EditorUtil.GUIColorScope(Colors.GreenYellow)) {

                while (currentProperty.NextVisible(enterChildren) && !SerializedProperty.EqualContents(currentProperty, nextSiblingProperty)) {
                    EditorGUI.PropertyField(fieldPosition, currentProperty, true);
                    fieldPosition.y += EditorGUI.GetPropertyHeight(currentProperty, true) + EditorGUIUtility.standardVerticalSpacing;
                    enterChildren = false;
                }
            }

            */
        }
    }

}
#endif