using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace nitou.HitSystem {

    /// <summary>
    /// ヒット受けつけるレシーバー
    /// </summary>
    public abstract class HitReciverBase<TData> : MonoBehaviour, IHitReciver<TData> 
        where TData : HitData{

        /// <summary>
        /// 中心座標
        /// </summary>
        public virtual Vector3 Center => transform.position;

        /// <summary>
        /// ヒット時の通知
        /// </summary>
        public System.IObservable<TData> OnHitRecieved => _onHitRecivedSubject;
        protected Subject<TData> _onHitRecivedSubject = new();

        protected TData _lastHit;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 

        protected void OnDestroy() {
            _onHitRecivedSubject.Dispose();
            _onHitRecivedSubject = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        void IHitReciver<TData>.OnReciveHit(TData hitData) {
            if (!CanHit(hitData)) return;

            OnHitInternal(hitData);

            _onHitRecivedSubject.OnNext(hitData);

            // cache
            _lastHit = hitData;
        }


        /// ----------------------------------------------------------------------------
        // Protected Method 

        /// <summary>
        /// ヒット時の内部処理
        /// </summary>
        protected virtual void OnHitInternal(TData hitData) {}

        /// <summary>
        /// ヒット可能な状態かの確認
        /// </summary>
        protected virtual bool CanHit(TData hitData) => true;


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        protected virtual void OnDrawGizmos() {
            if (_lastHit != null && _lastHit.ElaposedTime <2) {
                _lastHit.DrawGizmo(Colors.Red);
            }
        }
#endif
    }
}