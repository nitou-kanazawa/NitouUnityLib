using System.Collections.Generic;
using UnityEngine;

namespace nitou{

    /// <summary>
    /// <see cref="Mathf"/>に足りない機能を提供するユーティリティクラス
    /// </summary>
    public static class MathUtil{

        /// ----------------------------------------------------------------------------
        #region 要素の取得

        /// <summary>
        /// 要素の最大値を返す
        /// </summary>
        public static float Max(Vector2 vector) {
            return Mathf.Max(vector.x, vector.y);
        }

        /// <summary>
        /// 要素の最大値を返す
        /// </summary>
        public static float Max(Vector3 vector) {
            return Mathf.Max(vector.x, vector.y, vector.z);
        }

        /// <summary>
        /// 要素の最小値を返す
        /// </summary>
        public static float Min(Vector2 vector) {
            return Mathf.Min(vector.x, vector.y);
        }

        /// <summary>
        /// 要素の最小値を返す
        /// </summary>
        public static float Min(Vector3 vector) {
            return Mathf.Min(vector.x, vector.y, vector.z);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 円形座標

        private const int MIN_SEGMENT = 3;

        /// <summary>
        /// 円上の座標を取得する
        /// </summary>
        public static List<Vector3> CirclePoints(float radius = 1f, int segments = 20,
            Vector3 offset = default, bool isLoop = true,
            PlaneType type = PlaneType.ZX) {

            var pointCount = Mathf.Max(segments, MIN_SEGMENT);
            var deltaAngle = (Mathf.PI * 2) / pointCount;

            // ※360度の点も含めたい場合は＋１
            if (isLoop) pointCount++;

            // 点列の計算
            var points = new List<Vector3>();
            for (int i = 0; i < pointCount; i++) {
                points.Add(CirclePoint(radius, i * deltaAngle, type) + offset);
            }
            return points;
        }

        /// <summary>
        /// 円上の座標を取得する
        /// </summary>
        public static Vector3 CirclePoint(float radius, float angle, PlaneType type = PlaneType.ZX) {
         return type switch {
                PlaneType.XY => new Vector3(
                    x: radius * Mathf.Cos(angle),
                    y: radius * Mathf.Sin(angle),
                    z: 0f),
                PlaneType.YZ => new Vector3(
                    x: 0f,
                    y: radius * Mathf.Cos(angle),
                    z: radius * Mathf.Sin(angle)),
                PlaneType.ZX => new Vector3(
                    x: radius * Mathf.Sin(angle),
                    y: 0f,
                    z: radius * Mathf.Cos(angle)),
                _ => throw new System.NotImplementedException()
            };
        }      
        #endregion


        /// ----------------------------------------------------------------------------
        #region 角度変換

        /// <summary>
        /// 2次元ベクトルから角度(radian)へ変換する拡張メソッド
        /// </summary>
        public static float VectorToRad(this Vector2 vector) {
            return Mathf.Atan2(vector.y, vector.x);
        }

        /// <summary>
        /// 角度(radian)から2次元ベクトルへ変換する拡張メソッド
        /// </summary>
        public static Vector2 RadToVector(this float radian) {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        /// <summary>
        /// 2次元ベクトルから角度(degree)へ変換する拡張メソッド
        /// </summary>
        public static float VectorToDeg(this Vector2 vector) {
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// 角度(degree)から2次元ベクトルへ変換する拡張メソッド
        /// </summary>
        public static Vector2 DegToVector(this float degree) {
            return new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
        }

        #endregion

    }

}
