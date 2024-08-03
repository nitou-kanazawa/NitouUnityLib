#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [参考]
//  hatena: EditorWindow で GUIStyle を使う際の注意 https://hacchi-man.hatenablog.com/entry/2020/03/17/220000
//  hatena: 色別の GUIStyle をキャッシュするクラス https://hacchi-man.hatenablog.com/entry/2020/08/16/220000

namespace nitou.EditorShared {
    public static partial class EditorUtil {
        public static partial class Styles {

            public static GUIStyle folderHeader;


            /// <summary>
            /// コンストラクタ
            /// </summary>
            static Styles(){
                
                folderHeader = new GUIStyle("ShurikenModuleTitle") {
                    font = new GUIStyle(EditorStyles.label).font,
                    fontSize = 12,
                    border = new RectOffset(15, 7, 4, 4),
                    fixedHeight = 22,
                    contentOffset = new Vector2(20f, -2f),
                };


            }

            public static GUIStyle XmlText() {
                return new GUIStyle(EditorStyles.label) {
                    alignment = TextAnchor.UpperLeft,
                    richText = true,
                };
            }




        }

    }
}

#endif