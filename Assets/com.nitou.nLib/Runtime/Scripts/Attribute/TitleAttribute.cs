using System;
using UnityEngine;

namespace nitou.Inspector {

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
	public sealed class TitleAttribute : PropertyAttribute {
		public string TitleText { get; }

		public TitleAttribute(string titleText) {
			TitleText = titleText;
		}

	}

}

#if UNITY_EDITOR
namespace nitou.Inspector.Drawer {
    using UnityEditor;

    [CustomPropertyDrawer(typeof(TitleAttribute))]
    public class TitleDrawer : PropertyDrawer {

        private const int SPACE_HEIGHT = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var titleAttribute = (TitleAttribute)attribute;

            // タイトルの高さ
            var titlePosition = position.SetHeight(EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(titlePosition, titleAttribute.TitleText, EditorStyles.boldLabel);

            // 下線を描画
            Rect linePosition = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, 1);
            EditorGUI.DrawRect(linePosition, Color.gray);

            // 元のフィールドを描画する位置を調整
            Rect propertyPosition = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + SPACE_HEIGHT, position.width, position.height);

            // ここで新しいラベルを生成して使用
            GUIContent propertyLabel = new GUIContent(property.displayName);
            EditorGUI.PropertyField(propertyPosition, property, propertyLabel, true);
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            // タイトル、下線、プロパティのスペースを考慮して高さを計算
            return EditorGUIUtility.singleLineHeight + SPACE_HEIGHT + base.GetPropertyHeight(property, label);
        }
    }
}
#endif