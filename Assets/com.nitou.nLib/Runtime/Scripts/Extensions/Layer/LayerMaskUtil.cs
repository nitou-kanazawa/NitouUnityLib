using UnityEngine;

// [参考]
//  _: UnityでLayerMaskを操作する方法色々 https://12px.com/blog/2021/11/layermask/
//  Hatena: 物理演算、衝突判定、コライダーの検出などで使うLayerMaskについて https://indie-game-creation-with-unity.hatenablog.com/entry/layer-mask

namespace nitou {

    /// <summary>
    /// LayerMaskに静的メソッドを追加するクラス
    /// </summary>
    public static class LayerMaskUtil {

        /// ----------------------------------------------------------------------------
        // Public Method　(LayerMAskの生成)

        /// <summary>
        /// 全て入ったLayerMask
        /// </summary>
        public static LayerMask AllIn => -1;

        /// <summary>
        /// 空のLayerMask
        /// </summary>
        public static LayerMask Empty => 1;

        /// <summary>
        /// 特定レイヤーのみ入ったLayerMask
        /// </summary>
        public static LayerMask Only(int layer) => 1 << layer;

        /// <summary>
        /// 特定レイヤーのみ入ったLayerMask
        /// </summary>
        public static LayerMask Only(string layerName) => 1 << LayerMask.NameToLayer(layerName);

        /// <summary>
        /// "Default"のみ入ったLayerMask
        /// </summary>
        public static LayerMask OnlyDefault() => Only("Default");   // ※0でも可

    }

}
