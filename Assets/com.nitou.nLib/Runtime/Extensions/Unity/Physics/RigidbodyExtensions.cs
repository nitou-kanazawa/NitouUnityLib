using UnityEngine;

namespace nitou {

    /// <summary>
    /// <see cref="Rigidbody"/>の基本的な拡張メソッド集
    /// </summary>
    public static class RigidbodyExtensions {

        /// ----------------------------------------------------------------------------
        #region Basic

        /// <summary>
        /// ローカル座標を取得する拡張メソッド
        /// </summary>
        public static Vector3 GetLocalPosition(this Rigidbody self) {
            return self.transform.parent.InverseTransformPoint(self.position);
        }

        /// <summary>
        /// ローカル回転を取得する拡張メソッド
        /// </summary>
        public static Quaternion GetLocalRotation(this Rigidbody self) {
            return Quaternion.Inverse(self.transform.parent.rotation) * self.rotation;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Move

        /// <summary>
        /// Local座標を指定した移動メソッド
        /// </summary>
        public static void MoveLocalPosition(this Rigidbody self, Vector3 localPosition) {
            var position = self.transform.parent.TransformPoint(localPosition);
            self.MovePosition(position);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void MovePositionRelative(this Rigidbody self, Vector3 deltaPosition) {
            self.MovePosition(self.position + deltaPosition);
        }

        /// <summary>
        /// Local座標を指定した回転メソッド
        /// </summary>
        public static void MoveLocalRotaion(this Rigidbody self, Quaternion localRotation) {
            var rotation = self.transform.parent.rotation * localRotation;
            self.MoveRotation(rotation);
        }
        #endregion


        /// <summary>
        /// 速度をリセットする
        /// </summary>
        public static void ResetVelocity(this Rigidbody self) {
            self.velocity = Vector3.zero;
            self.angularVelocity = Vector3.zero;
        }
    }
}
