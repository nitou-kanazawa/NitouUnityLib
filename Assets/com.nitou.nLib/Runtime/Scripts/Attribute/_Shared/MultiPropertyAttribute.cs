using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [参考]
//  LIGHT11 複数のカスタムPropertyAttributeを作るとPropertyDrawerが競合する問題とその対応策 https://light11.hatenadiary.com/entry/2021/08/16/201543

namespace nitou.Inspector {

    public interface IAttributePropertyDrawer {
#if UNITY_EDITOR
        void OnGUI(Rect position, SerializedProperty property, GUIContent label);

        float GetPropertyHeight(SerializedProperty property, GUIContent label);
#endif
    }


    public abstract class MultiPropertyAttribute : PropertyAttribute {
        public MultiPropertyAttribute[] Attributes;
        public IAttributePropertyDrawer[] PropertyDrawers;

#if UNITY_EDITOR
        /// <summary>
        /// プロパティ描画の前処理
        /// </summary>
        public virtual void OnPreGUI(Rect position, SerializedProperty property) {}

        /// <summary>
        /// プロパティ描画の後処理
        /// </summary>
        public virtual void OnPostGUI(Rect position, SerializedProperty property, bool changed) {}

        // [NOTE] アトリビュートのうち一つでもfalseだったらそのGUIは非表示になる
        public virtual bool IsVisible(SerializedProperty property) {
            return true;
        }
#endif
    }



#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MultiPropertyAttribute), true)]
    public class MultiPropertyAttributeDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            var attributes = GetAttributes();
            var propertyDrawers = GetPropertyDrawers();

            // 非表示の場合
            if (attributes.Any(attr => !attr.IsVisible(property))) {
                return;
            }

            // 前処理
            foreach (var attr in attributes) {
                attr.OnPreGUI(position, property);
            }

            // 描画
            using (var ccs = new EditorGUI.ChangeCheckScope()) {
                if (propertyDrawers.Length == 0) {
                    EditorGUI.PropertyField(position, property, label);
                } else {
                    // ※最もorderが高いものを描画
                    propertyDrawers.Last().OnGUI(position, property, label);
                }

                // 後処理
                foreach (var attr in attributes.Reverse()) {
                    attr.OnPostGUI(position, property, ccs.changed);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var attributes = GetAttributes();
            var propertyDrawers = GetPropertyDrawers();

            // 非表示の場合
            if (attributes.Any(attr => !attr.IsVisible(property))) {
                return -EditorGUIUtility.standardVerticalSpacing;
            }

            var height = propertyDrawers.Length == 0
                ? base.GetPropertyHeight(property, label)
                : propertyDrawers.Last().GetPropertyHeight(property, label);
            return height;
        }

        private MultiPropertyAttribute[] GetAttributes() {
            var attr = (MultiPropertyAttribute)attribute;

            if (attr.Attributes == null) {
                attr.Attributes = fieldInfo
                    .GetCustomAttributes(typeof(MultiPropertyAttribute), false)
                    .Cast<MultiPropertyAttribute>()
                    .OrderBy(x => x.order)
                    .ToArray();
            }

            return attr.Attributes;
        }

        private IAttributePropertyDrawer[] GetPropertyDrawers() {
            var attr = (MultiPropertyAttribute)attribute;

            if (attr.PropertyDrawers == null) {
                attr.PropertyDrawers = fieldInfo
                    .GetCustomAttributes(typeof(MultiPropertyAttribute), false)
                    .OfType<IAttributePropertyDrawer>()
                    .OrderBy(x => ((MultiPropertyAttribute)x).order)
                    .ToArray();
            }

            return attr.PropertyDrawers;
        }
    }
#endif

}