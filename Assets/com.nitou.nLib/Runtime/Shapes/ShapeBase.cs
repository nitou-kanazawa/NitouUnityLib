using UnityEngine;

namespace nitou {
    public static partial class Shapes {

        [System.Serializable]
        public abstract class ShapeBase {

            /// <summary>
            /// ローカル座標
            /// </summary>
            public Vector3 position = Vector3.one;

            /// <summary>
            /// 回転（オイラー角）
            /// </summary>
            public Vector3 eulerAngle = Vector3.zero;


            /// ----------------------------------------------------------------------------
            // Property

            /// <summary>
            /// 回転（クォータニオン）
            /// </summary>
            public Quaternion rotation => Quaternion.Euler(eulerAngle);

            /// <summary>
            /// 基準方向（X軸）
            /// </summary>
            public Vector3 right => rotation * Vector3.right;

            /// <summary>
            /// 基準方向（Y軸）
            /// </summary>
            public Vector3 up => rotation * Vector3.up;

            /// <summary>
            /// 基準方向（Z軸）
            /// </summary>
            public Vector3 forward => rotation * Vector3.forward;


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public ShapeBase(Vector3 position, Quaternion rotation) {
                this.position = position;
                this.eulerAngle = rotation.eulerAngles;
            }

            protected ShapeBase() {}

            /// <summary>
            /// ワールド座標系での位置を取得する
            /// </summary>
            public Vector3 GetWorldPosition(Transform transform) {
                return transform.TransformPoint(this.position);
            }

            /// <summary>
            /// ワールド座標系での回転を取得する
            /// </summary>
            public Quaternion GetWorldRotaion(Transform transform) {
                return transform.rotation * Quaternion.Euler(this.eulerAngle);
            }
        }


        
    }


    

}
