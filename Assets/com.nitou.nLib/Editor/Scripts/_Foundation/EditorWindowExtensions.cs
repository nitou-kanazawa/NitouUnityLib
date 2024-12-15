#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace nitou.EditorScripts{

    /// <summary>
    /// <see cref="EditorWindow"/>の基本的な拡張メソッド集
    /// </summary>
    public static class EditorWindowExtensions{


        public static Rect GetPaddingRect(this EditorWindow self, float padding) {
            return self.position.Padding(padding);
        }



    }
}
#endif