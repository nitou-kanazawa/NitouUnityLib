using System.Collections.Generic;
using UnityEngine;

// [参考]
//  Hatena: Gizmo を描画する際に回転させる https://hacchi-man.hatenablog.com/entry/2021/05/23/220000

namespace nitou.DebugFuncition {

    public static class CubeDrawer{

        public enum Type {
            WireCube,
            Cube,
        }

        /// ----------------------------------------------------------------------------
        #region 描画メソッド

        /// <summary>
		/// キューブを描画する
		/// </summary>
		public static void DrawCube(Type type, Vector3 center, Quaternion rotation, Vector3 size) {

            if (rotation.Equals(default)) {
                rotation = Quaternion.identity;
            }

            var matrix = Matrix4x4.TRS(center, rotation, size);
            using ( new GizmoUtil.MatrixScope(matrix)){
                
                // 描画処理
                switch (type) {
                    case Type.WireCube:
                        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                        break;

                    case Type.Cube:
                        Gizmos.DrawCube(Vector3.zero, Vector3.one);
                        break;
                }
            }
        }   
        #endregion
    }
}
