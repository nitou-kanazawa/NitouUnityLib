using System;
using UniRx;
using UnityEngine.UI;

namespace nitou {

    /// <summary>
    /// Sliderの拡張メソッドクラス
    /// </summary>
    public static class SliderExtensions {

        /// ----------------------------------------------------------------------------
        // イベントの登録

        /// <summary>
        /// イベント登録を簡略化する拡張メソッド
        /// </summary>
        public static IDisposable SetOnValueChangedDestination(this Slider self, Action<float> onValueChanged) {
            return self.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(self);
        }

    }
}
