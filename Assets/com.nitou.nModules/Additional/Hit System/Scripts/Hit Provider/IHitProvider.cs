using UnityEngine;

namespace nitou.HitSystem{

    /// <summary>
    /// 攻撃判定を与えるオブジェクト
    /// </summary>
    public interface IHitProvider<TData> where TData : HitData{

        /// <summary>
        /// ヒットデータを生成する
        /// </summary>
        public TData CreateHitData(IHitReciver<TData> reciver);
    }
}
