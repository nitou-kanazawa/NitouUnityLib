using System;
using System.Collections.Generic;
using UniRx;

namespace nitou {

    /// <summary>
    /// <see cref="T"/>型のデータを保持できるオブジェクト．
    /// </summary>
    public interface IDataHolder<T> {

        /// <summary>
        /// 値が変化したときに通知するObservable．
        /// </summary>
        public IObservable<T> OnValueChanged { get; }

        /// <summary>
        /// 値を取得する．
        /// </summary>
        public T GetValue();

        /// <summary>
        /// 値を設定する．
        /// </summary>
        public void SetValue(T value);
    }


    public static class DataHolderExtensions {

        /// <summary>
        /// 双方向バインディング．
        /// </summary>
        public static void BindTo<T>(this IReactiveProperty<T> property, IDataHolder<T> target, ICollection<IDisposable> disposables) {

            // Property → Target
            property.SubscribeWithState(target, (x, t) => t.SetValue(x)).AddTo(disposables);

            // Targer → Property
            target.OnValueChanged.SubscribeWithState(property, (x, p) => p.Value = x).AddTo(disposables);
        }
    }
}
