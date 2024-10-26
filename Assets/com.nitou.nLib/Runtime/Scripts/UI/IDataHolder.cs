using System;

namespace nitou{

    public interface IDataHolder<T>{

        /// <summary>
        /// 値の変化を通知するストリーム．
        /// </summary>
        IObservable<T> OnValueChanged { get; }

        /// <summary>
        /// 値を取得する
        /// </summary>
        T GetValue();

        /// <summary>
        /// 値を設定する．
        /// </summary>
        void SetValue(T value);
    }
}
