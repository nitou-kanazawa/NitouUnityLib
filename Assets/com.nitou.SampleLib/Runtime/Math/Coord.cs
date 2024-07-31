using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou {

    /// <summary>
    /// Transformの"position"と"rotation"のみを扱うデータ構造体
    /// </summary>
    public struct Coord {

        public Vector3 position;
        public Quaternion rotation;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Coord(Vector3 position, Quaternion rotation) {
            this.position = position;
            this.rotation = rotation;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Coord(Transform transform) {
            this.position = transform.position;
            this.rotation = transform.rotation;
        }
    }


    public static partial class TransformExtension {

        public static void SetPositionAndRotation(this Transform self, Coord coord) =>
            self.SetPositionAndRotation(coord.position, coord.rotation);

    }
}
