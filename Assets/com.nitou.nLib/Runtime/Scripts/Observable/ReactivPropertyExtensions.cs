using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine.UI;
using TMPro;

// [参考]
//  qiita: UniRx入門　~ データバインディングとUnityイベント関数の購読 ~ https://qiita.com/su10/items/6d7fd792d4b553454a4f
//  Hatena: UniRx: SubscribeWithState は Subscribe より効率がよい https://noriok.hatenadiary.jp/entry/2018/09/17/144930

namespace nitou {



    /// <summary>
    /// 
    /// </summary>
    public static partial class UnityUIComponentExtensions {

        /// ----------------------------------------------------------------------------
        #region Text

        // [MEMO] SubscribeWithStateはクロージャが生成されない分，Subscribeより効率がよいらしい．

        /// <summary>
        /// 
        /// </summary>
        public static IDisposable SubscribeToText(this IObservable<string> source, TextMeshProUGUI text) {
            return source.SubscribeWithState(text, (x, t) => t.text = x);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IDisposable SubscribeToText<T>(this IObservable<T> source, TextMeshProUGUI text) {
            return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        public static IDisposable SubscribeToText<T>(this IObservable<T> source, TextMeshProUGUI text, Func<T, string> selector) {
            return source.SubscribeWithState2(text, selector, (x, t, s) => t.text = s(x));
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region InputField

        // [MEMO] OnValuChangedの方がObservable.CreateWithStateを介してるのは，おそらくSubscribe時に初期値を通知するため．

        /// <summary>
        /// Observe onEndEdit(Submit) event.
        /// </summary>
        public static IObservable<string> OnEndEditAsObservable(this TMP_InputField inputField) {
            return inputField.onEndEdit.AsObservable();
        }

        /// <summary>
        /// Observe onValueChanged with current `text` value on subscribe.
        /// </summary>
        public static IObservable<string> OnValueChangedAsObservable(this TMP_InputField inputField) {
            return Observable.CreateWithState<string, TMP_InputField>(inputField, (i, observer) => {
                observer.OnNext(i.text);
                return i.onValueChanged.AsObservable().Subscribe(observer);
            });
        }


        /// <summary>
        /// <see cref="TMP_InputField"/>との双方向バインディング．
        /// </summary>
        public static void BindToInputField(this IReactiveProperty<string> property, TMP_InputField inputField,
            ICollection<IDisposable> disposables) {

            // Model → View
            property.SubscribeWithState(inputField, (x, i) => i.text = x).AddTo(disposables);

            // View → Model
            inputField.OnEndEditAsObservable().SubscribeWithState(property, (x, p) => p.Value = x).AddTo(disposables);
        }

        /// <summary>
        /// 変換処理を指定した<see cref="TMP_InputField"/>との双方向バインディング．
        /// </summary>
        public static void BindToInputField<T>(this IReactiveProperty<T> property, TMP_InputField inputField,
            ICollection<IDisposable> disposables,
            Func<string, T> parseFunc, Func<T, string> formatFunc) {

            // Model → View
            property.SubscribeWithState2(inputField, formatFunc, (x, i, f) => i.text = f(x)).AddTo(disposables);

            // View → Model
            inputField.OnEndEditAsObservable()
                .Subscribe(value => {
                    try {
                        property.Value = parseFunc(value);
                    } catch {
                        // 変換失敗時に入力フィールドをリセット
                        inputField.text = formatFunc(property.Value);
                    }
                })
                .AddTo(disposables);
        }

        public static void BindToInputField(this IReactiveProperty<int> property, TMP_InputField inputField, ICollection<IDisposable> disposables) {

            // int
            property.BindToInputField(inputField, disposables,
                value => int.Parse(value),
                value => value.ToString());
        }

        public static void BindToInputFieldFloat(this IReactiveProperty<float> property, TMP_InputField inputField, ICollection<IDisposable> disposables) {

            // float 
            property.BindToInputField(inputField, disposables,
                value => float.Parse(value),
                value => value.ToString("F2"));
        }
        #endregion

        //public static void Foo() {
        // 
        //    // int バインディング
        //    propertyInt.BindToInputField(
        //        inputField,
        //        disposables,
        //        value => int.Parse(value),
        //        value => value.ToString());
        // 
        //    // float バインディング
        //    propertyFloat.BindToInputField(
        //        inputField,
        //        disposables,
        //        value => float.Parse(value),
        //        value => value.ToString("G"));
        //}



        /// ----------------------------------------------------------------------------
        #region Slider

        /// <summary>
        /// 
        /// </summary>
        public static void BindToSlider(this IReactiveProperty<float> property, Slider slider, ICollection<IDisposable> disposables) {

            // Model → View
            property.SubscribeWithState(slider, (x, s) => s.value = x).AddTo(disposables);

            // View → Model
            slider.OnValueChangedAsObservable()
                .SubscribeWithState(property, (x, p) => p.Value = x).AddTo(disposables);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Toggle

        /// <summary>
        /// 
        /// </summary>
        public static void BindToToggle(this IReactiveProperty<bool> property, Toggle toggle, ICollection<IDisposable> disposables) {

            // Model → View
            property.SubscribeWithState(toggle, (x, t) => t.isOn = x).AddTo(disposables);

            // View → Model
            toggle.OnValueChangedAsObservable()
                .SubscribeWithState(property, (x, p) => p.Value = x).AddTo(disposables);
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region Dropdown

        /// <summary>
        /// Observe onValueChanged with current `value` on subscribe.
        /// </summary>
        public static IObservable<int> OnValueChangedAsObservable(this TMP_Dropdown dropdown) {
            return Observable.CreateWithState<int, TMP_Dropdown>(dropdown, (d, observer) => {
                observer.OnNext(d.value);
                return d.onValueChanged.AsObservable().Subscribe(observer);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public static void BindToDropdown(this IReactiveProperty<int> property, TMP_Dropdown dropdown, ICollection<IDisposable> disposables) {

            // Model → View
            property.SubscribeWithState(dropdown, (x, d) => d.value = x).AddTo(disposables);

            // View → Model
            dropdown.OnValueChangedAsObservable()
                .SubscribeWithState(property, (x, p) => p.Value = x).AddTo(disposables);
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region Image

        /// <summary>
        /// 
        /// </summary>
        public static IDisposable SubscribeToImageFillAmount(this IObservable<float> source, Image image) {
            return source.SubscribeWithState(image, (x, i) => i.fillAmount = x);
        }
        #endregion

    }
}
