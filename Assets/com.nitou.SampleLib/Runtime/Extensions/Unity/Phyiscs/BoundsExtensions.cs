using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou {

    /// <summary>
    /// <see cref="Bounds"/>の基本的な拡張メソッド集
    /// </summary>
    public static class BoundsExtensions {

        /// <summary>
        /// Gizmoを表示する拡張メソッド
        /// </summary>
        public static void DrawGizmo(this Bounds self){
            Gizmos.DrawCube(self.center, self.size);
        }

        /// <summary>
        /// Gizmoを表示する拡張メソッド
        /// </summary>
        public static void DrawGizmo(this Bounds self, Color color) {
            Gizmos.DrawCube(self.center, self.size);
        }

        /// <summary>
        /// 他の<see cref="Bounds"/>が範囲内に完全に含まれているか確認する拡張メソッド
        /// </summary>
        public static bool Contains(this Bounds self, Bounds other) {

            // AABBの内外判定
            return self.Contains(other.min) && self.Contains(other.max);
        }
    }

}