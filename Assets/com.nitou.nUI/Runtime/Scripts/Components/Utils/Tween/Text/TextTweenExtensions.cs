using System;
using UnityEngine.UI;

// [参考]
//  qiita: DOTweenでカウントダウン・カウントアップのアニメーション https://qiita.com/RyotaMurohoshi/items/f7312e802f7698e42cd0

namespace DG.Tweening {

    /// <summary>
    /// TextのTweenライブラリクラス
    /// </summary>
    public static partial class TextTweenExtensions {

        /// --------------------------------------------------------------------
        #region 文字列のTween

        /// <summary>
        /// カウントダウン・カウントアップする拡張メソッド
        /// </summary>
        public static Tweener DOTextInt(this Text self, int startValue, int endValue, float duration, Func<int, string> convertor) {
            return DOTween.To(
                 () => startValue,
                 it => self.text = convertor(it),
                 endValue,
                 duration
             );
        }

        /// <summary>
        /// カウントダウン・カウントアップする拡張メソッド
        /// </summary>
        public static Tweener DOTextInt(this Text self, int startValue, int endValue, float duration) {
            return TextTweenExtensions.DOTextInt(self, startValue, endValue, duration, it => it.ToString());
        }

        /// <summary>
        /// カウントダウン・カウントアップする拡張メソッド
        /// </summary>
        public static Tweener DOTextFloat(this Text self, float startValue, float endValue, float duration, Func<float, string> convertor) {
            return DOTween.To(
                 () => startValue,
                 it => self.text = convertor(it),
                 endValue,
                 duration
             );
        }

        /// <summary>
        /// カウントダウン・カウントアップする拡張メソッド
        /// </summary>
        public static Tweener DOTextFloat(this Text self, float startValue, float endValue, float duration) {
            return TextTweenExtensions.DOTextFloat(self, startValue, endValue, duration, it => it.ToString());
        }
        #endregion

    }

}
