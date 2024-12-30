using System;
using TMPro;

// [参考]
//  qiita: DOTweenでカウントダウン・カウントアップのアニメーション https://qiita.com/RyotaMurohoshi/items/f7312e802f7698e42cd0
//  qiita: Unity1週間ゲームジャム (逆) にて実装した演出をまとめる https://qiita.com/lycoris102/items/30c3faaa6904c441cd71

namespace DG.Tweening {

    /// <summary>
    /// TextMeshProのTweenライブラリクラス
    /// </summary>
    public static partial class TextMeshProTweenExtension {

        /// --------------------------------------------------------------------
        #region 文字列のTween

        /// <summary>
        /// カウントダウン・カウントアップする拡張メソッド
        /// </summary>
        public static Tweener DOTextInt(this TextMeshProUGUI self, int startValue, int endValue, float duration, Func<int, string> convertor) {
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
        public static Tweener DOTextInt(this TextMeshProUGUI self, int startValue, int endValue, float duration) {
            return TextMeshProTweenExtension.DOTextInt(self, startValue, endValue, duration, it => it.ToString());
        }

        /// <summary>
        /// カウントダウン・カウントアップする拡張メソッド
        /// </summary>
        public static Tweener DOTextFloat(this TextMeshProUGUI self, float startValue, float endValue, float duration, Func<float, string> convertor) {
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
        public static Tweener DOTextFloat(this TextMeshProUGUI self, float startValue, float endValue, float duration) {
            return TextMeshProTweenExtension.DOTextFloat(self, startValue, endValue, duration, it => it.ToString());
        }
        #endregion


        /// --------------------------------------------------------------------
        #region その他のTween

        /// <summary>
        /// 文字間隔をTweenする拡張メソッド
        /// </summary>
        public static Tweener DOCharacterSpace(this TextMeshProUGUI self, float endValue, float duration) {
            return DOTween.To(
                 () => self.characterSpacing,
                 value => self.characterSpacing = value,
                 endValue,
                 duration
             );
        }

        /// <summary>
        /// 行間隔をTweenする拡張メソッド
        /// </summary>
        public static Tweener DOLineSpace(this TextMeshProUGUI self, float endValue, float duration) {
            return DOTween.To(
                 () => self.lineSpacing,
                 value => self.lineSpacing = value,
                 endValue,
                 duration
             );
        }
        #endregion
    }
}