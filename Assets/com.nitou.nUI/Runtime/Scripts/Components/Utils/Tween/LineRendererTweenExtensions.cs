using UnityEngine;

// [参考]
//  コガネブログ: LineRendererでDOFadeを使えるようにする拡張メソッド https://baba-s.hatenablog.com/entry/2023/04/05/102559

namespace DG.Tweening {

    /// <summary>
    /// <see cref="LineRenderer"/>のTween関連の拡張メソッド集
    /// </summary>
    public static partial class LineRendererTweenExtensions {

        /// --------------------------------------------------------------------
        #region Fading Tweens

        /// <summary>
        /// DOFadeの拡張メソッド
        /// </summary>
        public static Tweener DOFade(this LineRenderer self, float endValue, float duration) {

            var startColor = self.startColor;
            var endColor = self.endColor;

            return self.DOColor(
                    startValue: new(startColor, endColor),
                    endValue: new(
                        new Color(startColor.r, startColor.g, startColor.b, endValue),
                        new Color(endColor.r, endColor.g, endColor.b, endValue)
                    ),
                    duration: duration
                );
        }

        /// <summary>
        /// フェードアウトさせる拡張メソッド
        /// </summary>
        public static Tweener DOFadeOut(this LineRenderer self, float duration) =>
            self.DOFade(0.0F, duration);

        /// <summary>
        /// フェードインさせる拡張メソッド
        /// </summary>
        public static Tweener DOFadeIn(this LineRenderer self, float duration) =>
            self.DOFade(1.0F, duration);
        #endregion

        /// --------------------------------------------------------------------
        #region Parameter Tweens

        /// <summary>
        /// <see cref="LineRenderer.widthMultiplier"/>をアニメーションさせる拡張メソッド
        /// </summary>
        public static Tweener DOParam_WidthMultiplier(this LineRenderer self, float endValue, float duration) {
            return DOTween.To(
                () => self.widthMultiplier,
                x => self.widthMultiplier = x,
                endValue,
                duration);
        }

        /// <summary>
        /// <see cref="LineRenderer.textureScale"/>をアニメーションさせる拡張メソッド
        /// </summary>
        public static Tweener DOParam_TextureScale(this LineRenderer self, Vector2 endValue, float duration) {
            return DOTween.To(
                () => self.textureScale,
                x => self.textureScale = x,
                endValue,
                duration);
        }

        #endregion
    }

}