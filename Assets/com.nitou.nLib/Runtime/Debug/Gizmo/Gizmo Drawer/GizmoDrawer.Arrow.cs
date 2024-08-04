using System.Collections.Generic;
using UnityEngine;

namespace nitou.DebugInternal{
    internal static partial class GizmoDrawer{

        /// <summary>
        /// 矢印
        /// </summary>
        internal static class Arrow {

            public enum ArrowType {
                Line,
                Solid
            }

            private const int CIRCLE_SEGMENTS = 8;


            /*

            /// <summary>
            /// 位置，方向を指定して矢印を描画する
            /// </summary>
            public static void DrawRayArrow(ArrowType type, Vector3 pos, Vector3 direction,
                float arrowHeadLength = 0.08f, float arrowHeadAngle = 20.0f) {

                // Arrow shaft
                Gizmos.DrawRay(pos, direction);

                // 長さ０なら終了
                if (direction == Vector3.zero) return;

                // Arrow head
                switch (type) {
                    case ArrowType.Line:
                        var (right, left) = CalcArrowHeadDirection(direction, arrowHeadAngle);
                        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
                        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
                        break;

                    case ArrowType.Solid:
                        float radius = 0.025f;
                        CylinderDrawer.DrawWireCone(
                            MathUtil.PlaneType.XY,
                            pos + direction,
                            Quaternion.LookRotation(direction),
                            radius,
                            arrowHeadLength,
                            CIRCLE_SEGMENTS
                        );
                        break;
                }

            }

            /// <summary>
            /// ２点を指定して矢印を描画する
            /// </summary>
            public static void DrawLineArrow(ArrowType type, Vector3 from, Vector3 to,
                float arrowHeadLength = 0.2f, float arrowHeadAngle = 20.0f) {

                // Arrow shaft
                Gizmos.DrawLine(from, to);

                // 長さ０なら終了
                if (Mathf.Approximately(Vector3.Distance(from, to), 0f)) return;

                // Arrow head
                var direction = to - from;
                switch (type) {
                    case ArrowType.Line:
                        var (right, left) = CalcArrowHeadDirection(direction, arrowHeadAngle);
                        Gizmos.DrawRay(to, right * arrowHeadLength);
                        Gizmos.DrawRay(to, left * arrowHeadLength);
                        break;

                    case ArrowType.Solid:
                        float radius = 0.025f;
                        CylinderDrawer.DrawWireCone(
                            MathUtil.PlaneType.XY,
                            from + direction,
                            Quaternion.LookRotation(direction),
                            radius,
                            arrowHeadLength,
                            CIRCLE_SEGMENTS
                        );
                        break;
                }


            }


            */


            /// ----------------------------------------------------------------------------
            // Private Method

            /// <summary>
            /// 矢印先端の２方向を計算する
            /// </summary>
            private static (Vector3 right, Vector3 left) CalcArrowHeadDirection(Vector3 direction, float arrowHeadAngle) =>
                (
                    right: Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward,
                    left: Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward
                );
        }
    }
}
