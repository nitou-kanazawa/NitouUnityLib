using UniRx;
using UnityEngine;

namespace nitou.HitSystem{
    using nitou.Inspector;

    /// <summary>
    /// ヒットを与えるオブジェクト
    /// </summary>
    public abstract class HitVolume<TData> : MonoBehaviour, IHitProvider<TData>
        where TData : HitData {

        [DisableInPlayMode]
        [SerializeField, Indent] protected LayerMask _hitLayer;

        protected Subject<TData> _onHitApplySubject = new();

        /// <summary>
        /// コライダー
        /// </summary>
        public abstract Collider Collider { get; }
                
        /// <summary>
        /// ヒット時の通知
        /// </summary>
        public System.IObservable<TData> OnHitApplay => _onHitApplySubject;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 

        protected virtual void Reset() {
            _hitLayer = LayerMaskUtil.OnlyDefault();
        }

        protected void OnEnable() {
            if (Collider != null) {
                Collider.enabled = true;
                Collider.isTrigger = true;
            }
        }

        protected void OnDisable() {
            if (Collider != null) {
                Collider.enabled = false;
            }
        }

        protected void OnDestroy() {
            _onHitApplySubject.Dispose();
            _onHitApplySubject = null;
        }

        protected void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<IHitReciver<TData>>(out var reciver)
                && other.IsInLayerMask(_hitLayer)) {

                // ヒット処理
                var data = CreateHitData(reciver);
                reciver.OnReciveHit(data);

                // イベント通知
                _onHitApplySubject.OnNext(data);
            }
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// ヒットデータを生成する
        /// </summary>
        public abstract TData CreateHitData(IHitReciver<TData> reciver);

    }
}
