using UnityEngine;

namespace nitou {
    public static partial class Shapes {

        /// <summary>
        /// ���̂̌`���\���C���X�^���X
        /// </summary>
        [System.Serializable]
        public class Sphere : ShapeBase{

            /// <summary>
            /// ���a
            /// </summary>
            public float radius = 1f;


            /// ----------------------------------------------------------------------------
            // Public Method

            public Sphere(Vector3 position, Quaternion rotation, float radius)
                : base(position, rotation) {
                this.radius = radius;
            }

            private Sphere() { }

            public override string ToString() {
                return $"[Sphere] position: {position}, rotation: {eulerAngle}, radius: {radius}";
            }
        }
    }
}