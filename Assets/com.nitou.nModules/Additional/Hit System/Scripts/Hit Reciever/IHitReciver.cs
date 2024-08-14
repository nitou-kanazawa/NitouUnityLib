using UnityEngine;

namespace nitou.HitSystem {

    /// <summary>
    /// 攻撃判定を取るオブジェクト
    /// </summary>
    public interface IHitReciver<TData>  where TData : HitData{

        /// <summary>
        /// 中心位置
        /// </summary>
        public Vector3 Center { get; }

        /// <summary>
        /// ヒット時の処理
        /// </summary>
        void OnReciveHit(TData hitData);
    }

}