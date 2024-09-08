using System;
using UniRx;
using UnityEngine.UI;

namespace nitou {

    /// <summary>
    ///  Toggleの拡張メソッドクラス
    /// </summary>
    public static class ToggleExtensions {

        /// ----------------------------------------------------------------------------
        // イベントの登録

        /// <summary>
        /// イベント登録を簡略化する拡張メソッド
        /// </summary>
        public static IDisposable SetOnValueChangedDestination(this Toggle self, Action<bool> onValueChanged) {
            return self.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(self);
        }
    }
}
