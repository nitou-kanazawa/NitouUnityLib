using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// [参考]
//  コガネブログ: Vector2 の代入を簡略化する Deconstruction https://baba-s.hatenablog.com/entry/2019/09/03/230900

namespace nitou {

    /// <summary>
    /// <see cref="Vector2"/>の基本的な拡張メソッド集
    /// </summary>
    public static class Vector2Extensions {

        /// <summary>
        /// デコンストラクタ
        /// </summary>
        public static void Deconstruct(this Vector2 self, out float x, out float y) {
            x = self.x;
            y = self.y;
        }


        /// ----------------------------------------------------------------------------
        #region 値の取得

        /// <summary>
        /// 最大の要素の値を取得する
        /// </summary>
        public static float MaxElement(this Vector2 self) {
            return Mathf.Max(self.x, self.y);
        }

        /// <summary>
        /// 最小の要素の値を取得する
        /// </summary>
        public static float MixElement(this Vector2 self) {
            return Mathf.Min(self.x, self.y);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // 簡易計算

        public static float Angle(this Vector2 self, Vector2 other) {
            return Vector2.Angle(self, other);
        }

        public static float SignedAngle(this Vector2 self, Vector2 other) {
            return Vector2.SignedAngle(self, other);
        }

        public static float Angle90(this Vector2 self, Vector2 other) {
            var degree = Vector2.Angle(self, other);
            return (degree <= 90) ? degree : degree -90;
        }


        /// ----------------------------------------------------------------------------
        // 簡易計算

        /// <summary>
        /// 半分の値を返す
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Half(this Vector2 self) => self * 0.5f;

        /// <summary>
        /// ２倍の値を返す
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Twice(this Vector2 self) => self * 2f;

    }
}
