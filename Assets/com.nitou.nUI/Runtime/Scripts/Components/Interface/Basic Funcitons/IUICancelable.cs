using System;
using UniRx;

namespace nitou.UI.Components {

    /// <summary>
    /// Interface of the UI that handles the ÅgCancelÅh event.
    /// </summary>
    public interface IUICancelable : IUIComponent {

        /// <summary>
        /// Observable that nortifies when a "Cancel" input is detected.
        /// </summary>
        public IObservable<Unit> OnCanceled { get; }
    }
}
