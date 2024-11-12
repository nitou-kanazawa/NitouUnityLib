using UnityEngine;

namespace DG.Tweening {

    /// <summary>
    /// <see cref="CanvasGroup"/>のTween関連の拡張メソッド集
    /// </summary>
    public static class CanvasGrouopTweenExtensions {

        /// --------------------------------------------------------------------
        #region Fading Tweens

        /// <summary>
        /// フェードアウトさせる拡張メソッド
        /// </summary>
        public static Tweener DOFadeOut(this CanvasGroup self, float duration) {
            return self.DOFade(0.0F, duration);
        }

        /// <summary>
        /// フェードインさせる拡張メソッド
        /// </summary>
        public static Tweener DOFadeIn(this CanvasGroup canvasGroup, float duration) {
            return canvasGroup.DOFade(1.0F, duration);
        }
        #endregion
    }

}