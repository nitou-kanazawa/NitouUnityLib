using UnityEngine;

namespace nitou {
    public partial class Shapes {

        /// <summary>
        /// 直方体の形状を表すインスタンス
        /// </summary>
        [System.Serializable]
        public class Box : Volume {

            /// <summary>
            /// サイズ
            /// </summary>
            public Vector3 size = Vector3.zero;


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Box(Vector3 position, Quaternion rotation, Vector3 size)
                : base(position, rotation) {
                this.size = size;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Box(Transform transform, Vector3 size)
                : base(transform) {
                this.size = size;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Box(BoxCollider collider) {
                this.position = collider.GetWorldCenter();
                this.eulerAngle = collider.transform.rotation.eulerAngles;
                this.size = collider.GetScaledSize();
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            private Box(){}

            public override string ToString() {
                return $"[Box] position: {position}, rotation: {eulerAngle}, size: {size}";
            }



            /// ----------------------------------------------------------------------------
            // Public Method




        }

    }


    public static partial class BoxColliderExtensions {

        /// <summary>
        /// 形状情報を取得する
        /// </summary>
        //public static Shapes.Box GetVolume(this BoxCollider self) {

        //    // ワールド座標系での位置，回転，サイズ
        //    Vector3 worldCenter = self.transform.TransformPoint(self.center);
        //    Quaternion worldRotation = self.transform.rotation;
        //    Vector3 worldSize = Vector3.Scale(self.transform.lossyScale, self.size);

        //    // Boxのプロパティを設定
        //    var boxShape = new Shapes.Box(worldCenter, worldRotation, worldSize);
        //    return boxShape;
        //}
    }
}
