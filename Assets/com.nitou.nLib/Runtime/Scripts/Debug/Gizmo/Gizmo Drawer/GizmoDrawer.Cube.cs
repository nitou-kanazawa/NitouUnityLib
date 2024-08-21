using System.Collections.Generic;
using UnityEngine;

namespace nitou.DebugInternal {
    internal static partial class GizmoDrawer {

        /// <summary>
        /// ������
        /// </summary>
        internal static class Cube {

            /// ----------------------------------------------------------------------------
            #region �`�惁�\�b�h

            /// <summary>
            /// ���C���[�L���[�u��`�悷��
            /// </summary>
            public static void DrawWireCube(Vector3 center, Quaternion rotation, Vector3 size) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, size);
                using (new GizmoUtil.MatrixScope(matrix)) {
                    Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                }
            }

            /// <summary>
            /// �L���[�u��`�悷��
            /// </summary>
            public static void DrawCube(Vector3 center, Quaternion rotation, Vector3 size) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, size);
                using (new GizmoUtil.MatrixScope(matrix)) {
                    Gizmos.DrawCube(Vector3.zero, Vector3.one);
                }
            }
            #endregion
        }
    }
}