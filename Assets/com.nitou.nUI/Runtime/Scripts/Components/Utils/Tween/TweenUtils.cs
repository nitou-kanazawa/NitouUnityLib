using UnityEngine;

// [参考]
//  コガネブログ: DOTweenでTime.timeScaleを無視する方法 https://baba-s.hatenablog.com/entry/2016/11/17/100000#google_vignette

namespace DG.Tweening {

    /// <summary>
    /// <see cref="Tween"/>に関する汎用メソッド集
    /// </summary>
    public static class TweenUtils{

        /// <summary>
        /// SetUpdate(true)の糖衣構文.
        /// </summary>
        public static T IgnoreTimeScale<T>(this T t) 
            where T : Tween {
            return t.SetUpdate(true);
        }

    }
}
