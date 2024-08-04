using System;
using System.Collections.Generic;
using UnityEngine;

// [Ql]
//  github: neuneu9/unity-gizmos-utility https://github.com/neuneu9/unity-gizmos-utility/blob/master/GizmosUtility.cs
//  github: code-beans/GizmoExtensions https://github.com/code-beans/GizmoExtensions/blob/master/src/GizmosExtensions.cs

namespace nitou {
    using nitou.DebugInternal;
    using nitou.DebugFuncition;
    //using ArrowType = nitou.DebugFuncition.ArrowDrawer.ArrowType;

    //using CubeType = nitou.DebugFuncition.CubeDrawer.Type;
    //using CircleType = nitou.MathUtil.PlaneType;

    /// <summary>
    /// Gizmo•`‰æ‚ÉŠÖ‚·‚é”Ä—p‹@”\‚ğ’ñ‹Ÿ‚·‚éƒ‰ƒCƒuƒ‰ƒŠ (¦ƒtƒ@ƒT[ƒhƒNƒ‰ƒX)
    /// </summary>
    public static class Gizmos_ {

        private static Color DefaultColor => Colors.Gray;

        /// ----------------------------------------------------------------------------
        #region 3D}Œ`

        /// <summary>
        /// ü•ª‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawRay(Vector3 pos, Vector3 direction, Color color) {
            using (new GizmoUtil.ColorScope(color)) {
                Gizmos.DrawRay(pos, direction);
            }
        }

        /// <summary>
        /// ü•ª‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawLine(Vector3 from, Vector3 to, Color color) {
            using  (new GizmoUtil.ColorScope(color)) {
                Gizmos.DrawLine(from, to);
            }
        }

        /// <summary>
        /// ü•ª‚ğ•`‰æ‚·‚é
        /// </summary>
        //public static void DrawLine(LineSegment2 segment, Color color) {
        //    using (GizmoScope.ColorScope(color)) {
        //        Gizmos.DrawLine(segment.start, segment.end);
        //    }
        //}

        /// <summary>
        /// ü•ª‚ğ•`‰æ‚·‚é
        /// </summary>
        //public static void DrawLine(LineSegment3 segment, Color color) {
        //    using (GizmoScope.ColorScope(color)) {
        //        Gizmos.DrawLine(segment.start, segment.end);
        //    }
        //}

        /// <summary>
        /// Ü‚êü‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawLines(IReadOnlyList<Vector3> points, Color color) {
            using (new GizmoUtil.ColorScope(color)) {
                GizmoDrawer.Basic.DrawLines(points);
            }
        }
        #endregion


        /*


        /// ----------------------------------------------------------------------------
        #region 3D}Œ` (Ray)

        /// <summary>
        /// –îˆó‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawRayArrow(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f) =>
            ArrowDrawer.DrawRayArrow_ForGizmo(ArrowType.Solid, pos, direction, arrowHeadLength, arrowHeadAngle);

        /// <summary>
        /// –îˆó‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawRayArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f) {
            using (GizmoScope.ColorScope(color)) {
                ArrowDrawer.DrawRayArrow_ForGizmo(ArrowType.Solid, pos, direction, arrowHeadLength, arrowHeadAngle);
            }
        }

        // ----- 

        /// <summary>
        /// –îˆó‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawLineArrow(Vector3 from, Vector3 to, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f) =>
            ArrowDrawer.DrawLineArrow_ForGizmo(ArrowType.Solid, from, to, arrowHeadLength, arrowHeadAngle);

        /// <summary>
        /// –îˆó‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawLineArrow(Vector3 from, Vector3 to, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f) {
            using (GizmoScope.ColorScope(color)) {
                ArrowDrawer.DrawLineArrow_ForGizmo(ArrowType.Solid, from, to, arrowHeadLength, arrowHeadAngle);
            }
        }

        /// <summary>
        /// –îˆó‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawLineArrow(LineSegment2 segment, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f) {
            using (GizmoScope.ColorScope(color)) {
                ArrowDrawer.DrawLineArrow_ForGizmo(ArrowType.Solid, segment.start, segment.end, arrowHeadLength, arrowHeadAngle);
            }
        }

        /// <summary>
        /// –îˆó‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawLineArrow(LineSegment3 segment, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f) {
            using (GizmoScope.ColorScope(color)) {
                ArrowDrawer.DrawLineArrow_ForGizmo(ArrowType.Solid, segment.start, segment.end, arrowHeadLength, arrowHeadAngle);
            }
        }
        #endregion




        /// ----------------------------------------------------------------------------
        #region 3D}Œ` (Arc)

        /// <summary>
        /// ‰~‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCircle(Vector3 center, float radius) =>
            BasicShapeDrawer.DrawCircle(CircleType.ZX, center, Quaternion.identity, radius);

        /// <summary>
        /// ‰~‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCircle(Vector3 center, float radius, Color color) {
            using (GizmoScope.ColorScope(color)) {
                BasicShapeDrawer.DrawCircle(CircleType.ZX, center, Quaternion.identity, radius);
            }
        }

        /// <summary>
        /// ‰~‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCircle(Vector3 center, float radius, CircleType type, Color color) {
            using (GizmoScope.ColorScope(color)) {
                BasicShapeDrawer.DrawCircle(type, center, Quaternion.identity, radius);
            }
        }

        #endregion

        /// ----------------------------------------------------------------------------
        #region 3D}Œ` (Cube)

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCube(Vector3 center, Vector3 size) =>
            CubeDrawer.DrawCube(CubeType.WireCube, center, Quaternion.identity, size);

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCube(Vector3 center, Vector3 size, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CubeDrawer.DrawCube(CubeType.WireCube, center, Quaternion.identity, size);
            }
        }

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCube(Vector3 center, Quaternion rotation, Vector3 size) =>
            CubeDrawer.DrawCube(CubeType.WireCube, center, rotation, size);

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCube(Vector3 center, Quaternion rotation, Vector3 size, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CubeDrawer.DrawCube(CubeType.WireCube, center, rotation, size);
            }
        }

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCube(Transform transform, Vector3 size) =>
            CubeDrawer.DrawCube(CubeType.WireCube, transform.position, transform.rotation, size);

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCube(Transform transform, Vector3 size, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CubeDrawer.DrawCube(CubeType.WireCube, transform.position, transform.rotation, size);
            }
        }

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCube(BoxCollider collider) =>
            CubeDrawer.DrawCube(CubeType.WireCube, collider.GetWorldCenter(), collider.transform.rotation, collider.size);

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCube(BoxCollider collider, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CubeDrawer.DrawCube(CubeType.WireCube, collider.bounds.center, collider.transform.rotation, collider.size);
            }
        }

        // -----

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawCube(Vector3 center, Vector3 size) =>
            CubeDrawer.DrawCube(CubeType.Cube, center, Quaternion.identity, size);

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawCube(Vector3 center, Vector3 size, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CubeDrawer.DrawCube(CubeType.Cube, center, Quaternion.identity, size);
            }
        }

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawCube(Vector3 center, Quaternion rotation, Vector3 size) =>
            CubeDrawer.DrawCube(CubeType.Cube, center, rotation, size);

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawCube(Vector3 center, Quaternion rotation, Vector3 size, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CubeDrawer.DrawCube(CubeType.Cube, center, rotation, size);
            }
        }

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawCube(Transform transform, Vector3 size) =>
            CubeDrawer.DrawCube(CubeType.Cube, transform.position, transform.rotation, size);

        /// <summary>
        /// ƒLƒ…[ƒu‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawCube(Transform transform, Vector3 size, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CubeDrawer.DrawCube(CubeType.Cube, transform.position, transform.rotation, size);
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 3D}Œ` (Sphere)

        /// <summary>
        /// ‹…‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireSphere(Vector3 position, float radius, Color color) {
            using (GizmoScope.ColorScope(color)) {
                Gizmos.DrawWireSphere(position, radius);
            }
        }

        /// <summary>
        /// ‹…‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawSphere(Vector3 position, float radius, Color color) {
            using (GizmoScope.ColorScope(color)) {
                Gizmos.DrawSphere(position, radius);
            }
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region 3D}Œ` (Cylinder)

        /// <summary>
        /// ‰~’Œ‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCylinder(Vector3 center, float radius, float height) =>
            CylinderDrawer.DrawWireCylinder(CircleType.ZX, center, Quaternion.identity, radius, height);

        /// <summary>
        /// ‰~’Œ‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCylinder(Vector3 center, float radius, float height, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CylinderDrawer.DrawWireCylinder(CircleType.ZX, center, Quaternion.identity, radius, height);
            }
        }

        /// <summary>
        /// ‰~’Œ‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCylinder(Transform transform, float radius, float height) =>
            CylinderDrawer.DrawWireCylinder(CircleType.ZX, transform.position, transform.rotation, radius, height);

        /// <summary>
        /// ‰~’Œ‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCylinder(Transform transform, float radius, float height, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CylinderDrawer.DrawWireCylinder(CircleType.ZX, transform.position, transform.rotation, radius, height);
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 3D}Œ` (Cone)

        /// <summary>
        /// ‰~‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCone(Vector3 center, float radius, float height) =>
            CylinderDrawer.DrawWireCone(CircleType.ZX, center, Quaternion.identity, radius, height);

        /// <summary>
        /// ‰~‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCone(Transform transform, float radius, float height) =>
            CylinderDrawer.DrawWireCone(CircleType.ZX, transform.position, transform.rotation, radius, height);

        /// <summary>
        /// ‰~‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawWireCone(Transform transform, float radius, float height, Color color) {
            using (GizmoScope.ColorScope(color)) {
                CylinderDrawer.DrawWireCone(CircleType.ZX, transform.position, transform.rotation, radius, height);
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 3D}Œ` (Mesh)

        /// <summary>
        /// ‹…‚ğ•`‰æ‚·‚é
        /// </summary>
        public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Color color) {
            using (GizmoScope.ColorScope(color)) {
                Gizmos.DrawMesh(mesh, position, rotation);
            }
        }

        #endregion





        */
    }

}

