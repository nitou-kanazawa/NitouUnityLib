using System;
using UniRx;

namespace nitou.UI.Component{

    /// <summary>
    /// Interface of the UI that handles the ÅgPointerÅh event.
    /// </summary>
    public interface IUIPointerable{

        /// <summary>
        /// Observable that nortifies when the pointe has enter.
        /// </summary>
        public IObservable<Unit> OnPointerEnter { get; }

        /// <summary>
        /// Observable that nortifies when the pointe has exit.
        /// </summary>
        public IObservable<Unit> OnPointerExit { get; }
    }
}
