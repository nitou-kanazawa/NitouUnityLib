using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace nitou.DebugInternal {
    internal static partial class GizmoDrawer{

        /// <summary>
        /// 基本図形
        /// </summary>
        internal static class Basic {

            /// ----------------------------------------------------------------------------
            #region 線

            /// <summary>
            /// 点列上に折れ線を描画する
            /// </summary>
            public static void DrawLines(IReadOnlyList<Vector3> points) {
                if (points == null || points.Count <= 1) return;

                Vector3 from = points.First();
                points.Skip(1).ForEach(p => {
                    Gizmos.DrawLine(from, p);
                    from = p;
                });
            }

            /// <summary>
            /// ２グループの各対応点を結ぶ線を描画する
            /// </summary>
            public static void DrawLineSet(IReadOnlyList<Vector3> points1, IReadOnlyList<Vector3> points2) {
                if (points1 == null || points2 == null || points1.Count != points2.Count) return;

                for (int i = 0; i < points1.Count; i++) {
                    Gizmos.DrawLine(points1[i], points2[i]);
                }
            }
            #endregion

        }
    }
}
