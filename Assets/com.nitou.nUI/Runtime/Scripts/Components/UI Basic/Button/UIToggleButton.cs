using System;
using UnityEngine;
using UniRx;

namespace nitou.UI.Components{

    [AddComponentMenu(ComponentMenu.Prefix.UIComponents + "UI Toggle")]
    public class UIToggleButton : UIButton {

        // [FIXME]


        protected Subject<int> _onValueChangeSubject = new();

        /// <summary>
        /// ’l‚ª•Ï‰»‚µ‚½Žž‚É
        /// </summary>
        public IObservable<int> OnValueChanged => _onValueChangeSubject;


        /// ----------------------------------------------------------------------------
        // Public MonoBehaviour

        protected override void OnDestroy() {
            base.OnDestroy();

            _onValueChangeSubject.Dispose();
        }
    }
}
