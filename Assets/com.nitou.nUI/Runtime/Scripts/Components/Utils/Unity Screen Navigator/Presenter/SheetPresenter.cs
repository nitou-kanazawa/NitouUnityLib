﻿using System.Collections;
using System.Threading.Tasks;

namespace UnityScreenNavigator.Runtime.Core.Sheet {

    /// <summary>
    /// Pageプレゼンターの基底クラス
    /// </summary>
    public abstract class SheetPresenter<TSheet> : Presenter<TSheet>, ISheetPresenter 
        where TSheet : Sheet {

        private TSheet View { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected SheetPresenter(TSheet view) : base(view) {
            View = view;
        }


        /// ----------------------------------------------------------------------------
        #region ISheetLifecycleEvent

#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.Initialize() {
            return ViewDidLoad(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.Initialize() {
            return ViewDidLoad(View);
        }
#endif

#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.WillEnter() {
            return ViewWillEnter(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.WillEnter() {
            return ViewWillEnter(View);
        }
#endif

        void ISheetLifecycleEvent.DidEnter() {
            ViewDidEnter(View);
        }

#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.WillExit() {
            return ViewWillExit(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.WillExit() {
            return ViewWillExit(View);
        }
#endif

        void ISheetLifecycleEvent.DidExit() {
            ViewDidExit(View);
        }

#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.Cleanup() {
            return ViewWillDestroy(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.Cleanup() {
            return ViewWillDestroy(View);
        }
#endif
        #endregion


        /// ----------------------------------------------------------------------------
        #region  Inner LifecycleEvent

#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewDidLoad(TSheet view) {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewDidLoad(TSheet view) {
            yield break;
        }
#endif

#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillEnter(TSheet view) {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillEnter(TSheet view) {
            yield break;
        }
#endif

        protected virtual void ViewDidEnter(TSheet view) {
        }

#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillExit(TSheet view) {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillExit(TSheet view) {
            yield break;
        }
#endif

        protected virtual void ViewDidExit(TSheet view) {
        }

#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillDestroy(TSheet view) {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillDestroy(TSheet view) {
            yield break;
        }
#endif
        #endregion


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override void Initialize(TSheet view) {
            // The lifecycle event of the view will be added with priority 0.
            // Presenters should be processed after the view so set the priority to 1.
            view.AddLifecycleEvent(this, 1);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        protected override void Dispose(TSheet view) {
            view.RemoveLifecycleEvent(this);
        }
    }
}
