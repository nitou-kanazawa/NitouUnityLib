using System.Collections.Generic;
using UnityEngine;
using Alchemy.Inspector;

namespace nitou.HitSystem {
    using nitou.BachProcessor;

    /// <summary>
    /// 入れ子構造の範囲オブジェクト
    /// </summary>
    [DisallowMultipleComponent]
    public partial class NestedField : ComponentBase {

        [Title("Range")]
        [SerializeField] float _radius1 = 5;
        [SerializeField] float _radius2 = 10;

        [Title("Color")]
        [SerializeField] Color _color1 = Colors.GreenYellow;
        [SerializeField] Color _color2 = Colors.Red;


        [DisableInPlayMode]
        [SerializeField, Indent] protected LayerMask _hitLayer;

        [Space]

        [ReadOnly]
        [SerializeField] float _count1 = 0;
        [ReadOnly]
        [SerializeField] float _count2 = 0;

        public readonly List<GameObject> FirstFieldObjects = new();
        public readonly List<GameObject> SecondFieldObjects = new();



        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void OnEnable() {
            NestedFieldSystem.Register(this, UpdateTiming.Update);
            InitializeBufferOfCollidedCollision();      // ※キャッシュをクリア
        }

        protected virtual void Reset() {
            _hitLayer = LayerMaskUtil.Only("Default");
        }

        private void OnDisable() {
            NestedFieldSystem.Unregister(this, UpdateTiming.Update);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// Initializes a list of colliders that this component has collided with.
        /// </summary>
        private void InitializeBufferOfCollidedCollision() {}

        /// <summary>
        /// 各フレームでの検出開始時の初期化処理
        /// </summary>
        private void PrepareFrame() {
            FirstFieldObjects.Clear();
            SecondFieldObjects.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnUpdate(in Collider[] hitColliders) {

            // Field 1
            var count1 = CalculateSphereCast(hitColliders, _radius1);
            for (int index = 0; index < count1; index++) {

                var hit = hitColliders[index];
                var hitObject = hit.GetHitObject(CachingTarget.Collider);

                // コライダーを登録する
                FirstFieldObjects.Add(hitObject);
            }

            // Field 2
            var count2 = CalculateSphereCast(hitColliders, _radius2);
            for (int index = 0; index < count2; index++) {

                var hit = hitColliders[index];
                var hitObject = hit.GetHitObject(CachingTarget.Collider);

                // 内部領域に含まれているならスキップ
                if (FirstFieldObjects.Contains(hitObject)) continue;

                // コライダーを登録する
                SecondFieldObjects.Add(hitObject);
            }

            _count1 = FirstFieldObjects.Count;
        }

        /// <summary>
        /// ボックス範囲でコライダーを取得する
        /// </summary>
        private int CalculateBoxCast(Collider[] hitColliders, Vector3 size) {
            var count = Physics.OverlapBoxNonAlloc(
                transform.position,
                size.Half(),
                hitColliders,
                transform.rotation,
                _hitLayer,
                QueryTriggerInteraction.Ignore);
            return count;
        }

        /// <summary>
        /// 球体範囲でコライダーを取得する
        /// </summary>
        private int CalculateSphereCast(Collider[] hitColliders, float radius) {
            var count = Physics.OverlapSphereNonAlloc(
                transform.position,
                radius,
                hitColliders,
                _hitLayer,
                QueryTriggerInteraction.Ignore);
            return count;
        }

        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void OnDrawGizmos() {

            //Gizmos_.DrawWireCircle(transform.position, _radius1, _color1);
            //Gizmos_.DrawWireCircle(transform.position, _radius2, _color2);

            foreach (var obj in FirstFieldObjects) {
                var pos = obj.transform.position;
                var upPos = pos + Vector3.up * 3;
                Gizmos_.DrawLine(pos, upPos, _color1);
                Gizmos_.DrawSphere(upPos, 0.2f, _color1);
            }

            foreach (var obj in SecondFieldObjects) {
                var pos = obj.transform.position;
                var upPos = pos + Vector3.up * 3;
                Gizmos_.DrawLine(pos, upPos, _color2);
                Gizmos_.DrawSphere(upPos, 0.2f, _color2);
            }

            //if (!Application.isPlaying) return;
            //if (StateMachine.CurrentState != this) return;

            //Gizmos_.DrawLine(transform.position, target.position, Colors.Blue);
        }
#endif
    }
}
