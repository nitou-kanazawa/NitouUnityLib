using UnityEngine.UI;

namespace DG.Tweening {

    /// <summary>
    /// <see cref="Graphic"/>のTween関連の拡張メソッド集
    /// </summary>
    public static class GraphicTweenExtensions {

        /// --------------------------------------------------------------------
        // 出現・消失アニメーション

        /// <summary>
        /// フェードアウトさせる拡張メソッド
        /// </summary>
        public static Tweener DOFadeOut(this Graphic self, float duration) {
            return self.DOFade(0.0F, duration);
        }

        /// <summary>
        /// フェードインさせる拡張メソッド
        /// </summary>
        public static Tweener DOFadeIn(this Graphic canvasGroup, float duration) {
            return canvasGroup.DOFade(1.0F, duration);
        }
    }
}
