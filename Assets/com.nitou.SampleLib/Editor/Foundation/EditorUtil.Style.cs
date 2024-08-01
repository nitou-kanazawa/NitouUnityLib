#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [参考]
//  hatena: EditorWindow で GUIStyle を使う際の注意 https://hacchi-man.hatenablog.com/entry/2020/03/17/220000
//  hatena: 色別の GUIStyle をキャッシュするクラス https://hacchi-man.hatenablog.com/entry/2020/08/16/220000

namespace nitou.EditorShared {
    public static partial class EditorUtil {
        public static partial class Style {




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