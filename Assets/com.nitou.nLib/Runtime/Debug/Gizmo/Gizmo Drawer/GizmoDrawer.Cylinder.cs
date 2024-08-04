using System.Collections.Generic;
using UnityEngine;

// [Ql]
//  Kan‚Ìƒƒ‚’ : ƒMƒYƒ‚‚Å–îˆóA‰~’ŒAƒJƒvƒZƒ‹A‰~AŒÊ‚ğ•`‰æo—ˆ‚é‚æ‚¤‚É‚·‚éGizmoExtensions https://kan-kikuchi.hatenablog.com/entry/GizmoExtensions

namespace nitou.DebugInternal {
    internal static partial class GizmoDrawer {

        /// <summary>
        /// ‰~’Œ
        /// </summary>
        internal static class Cylinder {

            /// <summary>
            /// ‰~Œ`‚ğ’è‹`‚·‚éƒpƒ‰ƒ[ƒ^
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
            #region •`‰æƒƒ\ƒbƒh

            /// <summary>
            /// ‰~’Œ‚ğ•`‰æ‚·‚é
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
            /// ‰~‚ğ•`‰æ‚·‚é
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
            /// ‰~’Œ‚Ì‘¤–Ê•”‚ğ•`‰æ‚·‚é
            /// </summary>
            private static void DrawWireCylinderOuterLines(PlaneType planeType, DiscParam upperDisc, DiscParam lowerDisc) {
                if (upperDisc.segments != lowerDisc.segments) return;

                // À•WŒvZ
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

                // •`‰æi¦ã‰º‰~‚ÌŠe“_‚ğŒ‹‚Ôü•ªj
                Basic.DrawLineSet(upperPoints, lowerPoints);
            }

            /// <summary>
            /// ‰~‚Ì‘¤–Ê•”‚ğ•`‰æ‚·‚é
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

                // •`‰æi¦’ê–Ê(‰~)‚ÌŠe“_‚©‚ç’¸“_‚Ö‚Ìü•ªj
                lowerPoints.ForEach(p => Gizmos.DrawLine(p, top));
            }

            /// <summary>
            /// ‰~”Õ‚ğ•`‰æ‚·‚é
            /// </summary>
            private static void DrawWireDisc(PlaneType planType, DiscParam disc) {

                // À•WŒvZ
                var points = MathUtil.CirclePoints(
                    radius: disc.radius,
                    segments: disc.segments,
                    offset: disc.offset * planType.GetNormal(),
                    isLoop: true,
                    type: planType
                );

                // •`‰æ
                Basic.DrawLines(points);
            }
            #endregion
        }

    }
}
