using UnityEngine;

// [参考]
//  Kanのメモ帳: ギズモで矢印、円柱、カプセル、円、弧を描画出来るようにするGizmoExtensions https://kan-kikuchi.hatenablog.com/entry/GizmoExtensions

namespace nitou.DebugInternal {
    internal static partial class GizmoDrawer {

        /// <summary>
        /// 円柱
        /// </summary>
        internal static class Cylinder {

            /// <summary>
            /// 円形を定義するパラメータ
            /// </summary>
            private struct DiscParam {
                public float radius;
                public int segments;
                public float offset;

                public DiscParam(float radius, int segments, float offset = default) {
                    this.radius = radius;
                    this.segments = segments;
                    this.offset = offset;
                }
            }


            /// ----------------------------------------------------------------------------
            #region 描画メソッド

            /// <summary>
            /// 円柱を描画する
            /// </summary>
            public static void DrawWireCylinder(PlaneType type, Vector3 center, Quaternion rotation, float radius, float height,int discSegments = 20) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
                using (new GizmoUtil.MatrixScope(matrix)) {

                    var half = height / 2;

                    // Outer lines
                    int outerSegments = 5;
                    DrawWireCylinderOuterLines(
                        type,
                        new DiscParam(radius, outerSegments, half),
                        new DiscParam(radius, outerSegments, -half)
                    );

                    // Disks
                    DrawWireDisc(type, new DiscParam(radius, discSegments, half));
                    DrawWireDisc(type, new DiscParam(radius, discSegments, -half));
                }

            }

            /// <summary>
            /// 円錐を描画する
            /// </summary>
            public static void DrawWireCone(PlaneType type, Vector3 center, Quaternion rotation, float radius, float height,int discSegments = 20) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
                using (new GizmoUtil.MatrixScope(matrix)) {

                    // Outer lines
                    int outerSegments = 5;
                    DrawWireConeOuterLines(type, new DiscParam(radius, outerSegments), height);

                    // Disks
                    DrawWireDisc(type, new DiscParam(radius, discSegments));
                }
            }
            #endregion


            /// ----------------------------------------------------------------------------
            #region Private Method

            /// <summary>
            /// 円柱の側面部を描画する
            /// </summary>
            private static void DrawWireCylinderOuterLines(PlaneType planeType, DiscParam upperDisc, DiscParam lowerDisc) {
                if (upperDisc.segments != lowerDisc.segments) return;

                // 座標計算
                var upperPoints = MathUtil.CirclePoints(
                    radius: upperDisc.radius,
                    segments: upperDisc.segments,
                    offset: upperDisc.offset * planeType.GetNormal(),
                    isLoop: false,
                    type: planeType
                );
                var lowerPoints = MathUtil.CirclePoints(
                    radius: lowerDisc.radius,
                    segments: lowerDisc.segments,
                    offset: lowerDisc.offset * planeType.GetNormal(),
                    isLoop: false,
                    type: planeType
                );

                // 描画（※上下円の各点を結ぶ線分）
                Basic.DrawLineSet(upperPoints, lowerPoints);
            }

            /// <summary>
            /// 円錐の側面部を描画する
            /// </summary>
            private static void DrawWireConeOuterLines(PlaneType type, DiscParam lowerDisc, float height) {

                var top = (lowerDisc.offset + height) * type.GetNormal();
                var lowerPoints = MathUtil.CirclePoints(
                    radius: lowerDisc.radius,
                    segments: lowerDisc.segments,
                    offset: lowerDisc.offset * type.GetNormal(),
                    isLoop: false,
                    type: type
                );

                // 描画（※底面(円)の各点から頂点への線分）
                lowerPoints.ForEach(p => Gizmos.DrawLine(p, top));
            }

            /// <summary>
            /// 円盤を描画する
            /// </summary>
            private static void DrawWireDisc(PlaneType planType, DiscParam disc) {

                // 座標計算
                var points = MathUtil.CirclePoints(
                    radius: disc.radius,
                    segments: disc.segments,
                    offset: disc.offset * planType.GetNormal(),
                    isLoop: true,
                    type: planType
                );

                // 描画
                Basic.DrawLines(points);
            }
            #endregion
        }

    }
}
