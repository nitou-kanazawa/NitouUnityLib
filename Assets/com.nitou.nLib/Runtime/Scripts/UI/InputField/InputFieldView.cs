using System;
using UniRx;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace nitou.UI{

    public abstract class InputFieldView<T> : MonoBehaviour, IDataHolder<T>{

        // [NOTE] 具体的なコンポーネントとRPの紐づけは派生クラス側で行う．


        protected readonly ReactiveProperty<T> _valueRP = new();

        /// <summary>
        /// 値が更新されたときに通知するObservable．
        /// </summary>
        public IObservable<T> OnValueChanged => _valueRP;


        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        protected virtual void OnDestroy() {
            _valueRP.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        public virtual T GetValue() {
            return _valueRP.Value;
        }

        public virtual void SetValue(T newValue) {
            _valueRP.Value = newValue;
        }

        public virtual void ResetValue() {
            _valueRP.Value = default(T);
        }
    }
}
