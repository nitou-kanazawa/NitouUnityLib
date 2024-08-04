using UnityEngine;

namespace nitou {
    public class BoxColliderShapeVisualizer : MonoBehaviour {

        public bool drawGizmo;

        public Shapes.Box _box;


        void OnDrawGizmos() {
            if (!drawGizmo) return;

            // BoxColliderを取得
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider == null) return;

            // GetShapeメソッドを使用してBoxShapeを取得
            Shapes.Box boxShape = boxCollider.GetShape();

            // BoxColliderのGizmo描画（赤色）
            Gizmos.color = Color.red;
            DrawBoxColliderGizmo(boxCollider);

            // BoxShapeのGizmo描画（緑色）
            Gizmos.color = Color.green;
            DrawBoxShapeGizmo(boxShape);
        }

        private void DrawBoxColliderGizmo(BoxCollider boxCollider) {
            // BoxColliderの中心とサイズに基づいてGizmoを描画
            Vector3 worldCenter = boxCollider.transform.TransformPoint(boxCollider.center);
            Vector3 worldSize = Vector3.Scale(boxCollider.size, boxCollider.transform.lossyScale);
            Gizmos.matrix = Matrix4x4.TRS(worldCenter, boxCollider.transform.rotation, worldSize);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }

        private void DrawBoxShapeGizmo(Shapes.Box boxShape) {
            Gizmos.matrix = Matrix4x4.TRS(boxShape.position, boxShape.rotation, boxShape.size);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
