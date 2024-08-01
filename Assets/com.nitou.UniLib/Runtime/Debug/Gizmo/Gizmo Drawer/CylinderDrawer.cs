using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [éQçl]
//  KanÇÃÉÅÉÇí†: ÉMÉYÉÇÇ≈ñÓàÛÅAâ~íåÅAÉJÉvÉZÉãÅAâ~ÅAå Çï`âÊèoóàÇÈÇÊÇ§Ç…Ç∑ÇÈGizmoExtensions https://kan-kikuchi.hatenablog.com/entry/GizmoExtensions

namespace nitou.DebugFuncition {
    using CircleType = PlaneType;


    /*

    public static class CylinderDrawer {

        /// <summary>
        /// â~å`ÇíËã`Ç∑ÇÈÉpÉâÉÅÅ[É^
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
        #region ï`âÊÉÅÉ\ÉbÉh

        /// <summary>
        /// â~íåÇï`âÊÇ∑ÇÈ
        /// </summary>
        public static void DrawWireCylinder(CircleType type, Vector3 center, Quaternion rotation, float radius, float height,
            int discSegments = 20) {

            if (rotation.Equals(default)) {
                rotation = Quaternion.identity;
            }

            var matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            using (GizmoScope.MatrixScope(matrix)) {

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
        /// â~êçÇï`âÊÇ∑ÇÈ
        /// </summary>
        public static void DrawWireCone(CircleType type, Vector3 center, Quaternion rotation, float radius, float height,
            int discSegments = 20) {

            if (rotation.Equals(default)) {
                rotation = Quaternion.identity;
            }

            var matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            using (GizmoScope.MatrixScope(matrix)) {

                // Outer lines
                int outerSegments = 5;
                DrawWireConeOuterLines(type, new DiscParam(radius, outerSegments), height);

                // Disks
                DrawWireDisc(type, new DiscParam(radius, discSegments));
            }

        }


        /// <summary>
        /// ÉJÉvÉZÉãÇï`âÊÇ∑ÇÈ
        /// </summary>
        public static void DrawWireCapsule(Vector3 center, float radius, float height, Quaternion rotation = default) {

            //var old = Gizmos.matrix;

            //if (rotation.Equals(default)) {
            //    rotation = Quaternion.identity;
            //}
            //Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            //var half = height / 2 - radius;

            ////draw cylinder base
            //DrawWireCylinder(center, rotation, radius, height - radius * 2);

            //// draw upper cap
            //// do some cool stuff with orthogonal matrices
            //var mat = Matrix4x4.Translate(center + rotation * Vector3.up * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.forward));
            //BasicShapeDrawer.DrawWireArc(mat, radius, 180, 20);
            //mat = Matrix4x4.Translate(center + rotation * Vector3.up * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(90, Vector3.forward));
            //BasicShapeDrawer.DrawWireArc(mat, radius, 180, 20);

            //// draw lower cap
            //mat = Matrix4x4.Translate(center + rotation * Vector3.down * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.forward));
            //BasicShapeDrawer.DrawWireArc(mat, radius, 180, 20);
            //mat = Matrix4x4.Translate(center + rotation * Vector3.down * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(-90, Vector3.forward));
            //BasicShapeDrawer.DrawWireArc(mat, radius, 180, 20);

            //Gizmos.matrix = old;
        }
        #endregion



        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// â~íåÇÃë§ñ ïîÇï`âÊÇ∑ÇÈ
        /// </summary>
        private static void DrawWireCylinderOuterLines(CircleType type, DiscParam upperDisc, DiscParam lowerDisc) {
            if (upperDisc.segments != lowerDisc.segments) return;

            var upperPoints = MathUtil.CirclePoints(
                radius: upperDisc.radius,
                segments: upperDisc.segments,
                offset: upperDisc.offset * MathUtil.GetNormal(type),
                includeLast: false,
                type: type
            );
            var lowerPoints = MathUtil.CirclePoints(
                radius: lowerDisc.radius,
                segments: lowerDisc.segments, 
                offset: lowerDisc.offset* MathUtil.GetNormal(type), 
                includeLast: false,
                type: type
            );
            BasicShapeDrawer.DrawLines(upperPoints, lowerPoints);
        }

        /// <summary>
        /// â~êçÇÃë§ñ ïîÇï`âÊÇ∑ÇÈ
        /// </summary>
        private static void DrawWireConeOuterLines(CircleType type, DiscParam lowerDisc, float height) {

            var top = (lowerDisc.offset + height) * MathUtil.GetNormal(type);
            var points = MathUtil.CirclePoints(
                radius: lowerDisc.radius,
                segments: lowerDisc.segments,
                offset: lowerDisc.offset * MathUtil.GetNormal(type),
                includeLast: false,
                type: type
            );

            for (int i = 0; i < points.Count; i++) {
                Gizmos.DrawLine(points[i], top);
            }
        }

        /// <summary>
        /// â~î’Çï`âÊÇ∑ÇÈ
        /// </summary>
        private static void DrawWireDisc(CircleType type, DiscParam disc) {

            var points = MathUtil.CirclePoints(
                radius: disc.radius,
                segments: disc.segments,
                offset: disc.offset * MathUtil.GetNormal(type),
                includeLast: true,
                type: type
            );
            BasicShapeDrawer.DrawLines(points);
        }


    }
    */
}
