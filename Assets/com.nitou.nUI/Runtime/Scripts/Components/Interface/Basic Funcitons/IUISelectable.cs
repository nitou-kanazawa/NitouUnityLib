using System;
using UniRx;

namespace nitou.UI.Components {

    /// <summary>
    /// Interface of the UI that handles the ÅgSelectÅh event.
    /// </summary>
    public interface IUISelectable : IUIComponent {

        /// <summary>
        /// Observable that nortifies when object is selected.
        /// </summary>
        public IObservable<Unit> OnSelected { get; }

        /// <summary>
        /// Observable that nortifies when object is deselected.
        /// </summary>
        public IObservable<Unit> OnDeselected { get; }
    }

}