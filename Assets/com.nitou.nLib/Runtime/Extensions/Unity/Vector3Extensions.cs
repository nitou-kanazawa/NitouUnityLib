using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// [参考]
//  PG日誌 : Vector3(構造体)に自分自身の値を変更する拡張メソッドを定義する https://takap-tech.com/entry/2022/12/24/175039
//  コガネブログ:　Vector3 の代入を簡略化する Deconstruction　https://baba-s.hatenablog.com/entry/2019/09/03/230700

namespace nitou {

    /// <summary>
    /// <see cref="Vector3"/>の拡張メソッドクラス
    /// </summary>
    public static partial class Vector3Extensions {

        /// <summary>
        /// デコンストラクタ
        /// </summary>
        public static void Deconstruct(this Vector3 self, out float x, out float y, out float z) {
            x = self.x;
            y = self.y;
            z = self.z;
        }

        /// <summary>
        /// Vector2への変換
        /// </summary>
        public static Vector2 ToVector2(this Vector3 vector) {
                return new Vector2(vector.x, vector.y);
            }

        /// <summary>
        /// 要素同士の割り算
        /// </summary>
        public static Vector3 Divide(this Vector3 vector, Vector3 other) {
            return new Vector3(
                other.x != 0 ? vector.x / other.x : 0,
                other.y != 0 ? vector.y / other.y : 0,
                other.z != 0 ? vector.z / other.z : 0
            );
        }

        /// <summary>
        /// 要素同士の割り算
        /// </summary>
        public static Vector3 Divide(this Vector3 vector, Vector3 other, float defaultValue) {
            return new Vector3(
                other.x != 0 ? vector.x / other.x : defaultValue,
                other.y != 0 ? vector.y / other.y : defaultValue,
                other.z != 0 ? vector.z / other.z : defaultValue
            );
        }


        /// ----------------------------------------------------------------------------
        #region 値の取得

        /// <summary>
        /// 最大の要素の値を取得する
        /// </summary>
        public static float MaxElement(this Vector3 self) {
            return Mathf.Max(self.x, self.y, self.z);
        }

        /// <summary>
        /// 最小の要素の値を取得する
        /// </summary>
        public static float MixElement(this Vector3 self) {
            return Mathf.Min(self.x, self.y, self.z);
        }
        #endregion


        


        /// ----------------------------------------------------------------------------
        // 値の設定

        /// <summary>
        /// X値を設定する拡張メソッド
        /// </summary>
        public static void SetX(ref this Vector3 self, float x) => self.x = x;

        /// <summary>
        /// Y値を設定する拡張メソッド
        /// </summary>
        public static void SetY(ref this Vector3 self, float y) => self.y = y;

        /// <summary>
        /// Z値を設定する拡張メソッド
        /// </summary>
        public static void SetZ(ref this Vector3 self, float z) => self.z = z;


        /// <summary>
        /// X値のみ変更した値を返す拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithX(this Vector3 self, float x) => new Vector3(x, self.y, self.z);

        /// <summary>
        /// Y値のみ変更した値を返す拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithY(this Vector3 self, float y) => new Vector3(self.x, y, self.z);

        /// <summary>
        /// Z値のみ変更した値を返す拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithZ(this Vector3 self, float z) => new Vector3(self.x, self.y, z);


        /// ----------------------------------------------------------------------------
        // 値の加算

        /// <summary>
        /// X値に加算する拡張メソッド
        /// </summary>
        public static void AddX(ref this Vector3 self, float x) => self.x += x;

        /// <summary>
        /// Y値に加算する拡張メソッド
        /// </summary>
        public static void AddY(ref this Vector3 self, float y) => self.y += y;

        /// <summary>
        /// Z値に加算する拡張メソッド
        /// </summary>
        public static void AddZ(ref this Vector3 self, float z) => self.z += z;



        /// ----------------------------------------------------------------------------
        #region 値の変換

        // Clamp

        /// <summary>
        /// <see cref="Vector3.ClampMagnitude(Vector3, float)"/>の拡張メソッド
        /// </summary>
        public static Vector3 ClampMagnitude(this Vector3 self, float maxLength) {
            return Vector3.ClampMagnitude(self, maxLength);
        }

        /// <summary>
        /// <see cref="Vector3.ClampMagnitude(Vector3, float)"/>の拡張メソッド
        /// </summary>
        public static Vector3 ClampMagnitude01(this Vector3 self) {
            return Vector3.ClampMagnitude(self, 1);
        }


        // 簡易計算

        /// <summary>
        /// 半分の値を返す拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Half(this Vector3 self) => self * 0.5f;

        /// <summary>
        /// ２倍の値を返す拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Twice(this Vector3 self) => self * 2f;


        // 簡易計算

        
        #endregion









        public static Vector3 FindMinVector(IEnumerable<Vector3> ptList) {
            // LINQを使用して最小のベクトルを見つける
            return ptList.Aggregate((minVec, nextVec) => Vector3.Min(minVec, nextVec));
        }
    }

}