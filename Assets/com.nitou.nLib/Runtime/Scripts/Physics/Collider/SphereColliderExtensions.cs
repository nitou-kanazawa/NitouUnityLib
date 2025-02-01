﻿using System.Collections.Generic;
using UnityEngine;

namespace nitou {

    /// <summary>
    /// <see cref="SphereCollider"/>の基本的な拡張メソッド集
    /// </summary>
    public static class SphereColliderExtensions {

        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// グローバル座標に変換したコライダー中心座標を取得する拡張メソッド
        /// </summary>
        public static Vector3 GetWorldCenter(this SphereCollider self) {
            return self.transform.TransformPoint(self.center);
        }

        /// <summary>
        /// 親階層を考慮した半径を取得する拡張メソッド
        /// </summary>
        public static float GetScaledRadius(this SphereCollider sphere) {
            // (※Sphereコライダーは常に球形を維持して，半径に各軸の最大スケールが適用される)
            return sphere.radius * MathUtils.Max(sphere.transform.lossyScale);
        }


        /// ----------------------------------------------------------------------------


        /// <summary>
        /// 指定座標が<see cref="SphereCollider"/>の内部に含まれるか判定する拡張メソッド
        /// </summary>
        public static bool Contains(this SphereCollider sphere, Vector3 point) {

            var localPoint = sphere.transform.InverseTransformPoint(point);
            var scaledRadius = sphere.GetScaledRadius();

            return localPoint.sqrMagnitude <= scaledRadius * scaledRadius;
        }


    }

    public struct SphereData {
        public readonly Vector3 position;
        public readonly float radius;

        public SphereData(Vector3 position, float radius) {
            this.position = position;
            this.radius = Mathf.Max(0, radius);
        }
    }

}
