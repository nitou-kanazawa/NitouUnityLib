using UnityEngine;

namespace nitou {

    /// <summary>
    /// 平面
    /// </summary>
    public enum PlaneType {
        XY,
        YZ,
        ZX,
    }

    /// <summary>
    /// <see cref="Plane"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class PlaneExtensions {



    }


    public static class PlaneUtil {

        /// <summary>
        /// 平面に対応した法線ベクトルを取得する
        /// </summary>
        public static Vector3 GetNormal(PlaneType type) {
            return type switch {
                PlaneType.XY => Vector3.forward,
                PlaneType.YZ => Vector3.right,
                PlaneType.ZX => Vector3.up,
                _ => throw new System.NotImplementedException()
            };
        }

    }

}
