using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace nitou.GameSystem {

    /// <summary>
    /// 
    /// </summary>
    public abstract class SimpleProcessBase : IProcess {

        // State
        private UniTaskCompletionSource<ProcessResult> _completionSource = new();
        private readonly ReactiveProperty<ProcessState> _state = new(ProcessState.NotStarted);

        /// <summary>
        /// 
        /// </summary>
        public UniTask<ProcessResult> ProcessFinished => _completionSource.Task;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyReactiveProperty<ProcessState> State => _state;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 
        /// </summary>
        void IProcess.Run() {
            if (_state.Value != ProcessState.NotStarted)
                throw new InvalidOperationException("Process can only be started once.");

            OnStart(); // ライフサイクルイベント
            _state.Value = ProcessState.Running;
        }

        /// <summary>
        /// 
        /// </summary>
        void IProcess.Pause() {
            if (_state.Value != ProcessState.Running)
                throw new InvalidOperationException("Process can only be paused when running.");

            OnPause(); // ライフサイクルイベント
            _state.Value = ProcessState.Paused;
        }

        /// <summary>
        /// 
        /// </summary>
        void IProcess.UnPause() {
            if (_state.Value != ProcessState.Paused)
                throw new InvalidOperationException("Process can only be unpaused when paused.");

            OnUnPause(); // ライフサイクルイベント            
            _state.Value = ProcessState.Running;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancelResult"></param>
        void IProcess.Cancel(CancelResult cancelResult) {
            if (_state.Value != ProcessState.Running && 
                _state.Value != ProcessState.Paused)
                throw new InvalidOperationException("Process can only be cancelled when running or paused.");

            OnCancel(cancelResult); // ライフサイクルイベント
            _state.Value = ProcessState.Cancelled;

            _completionSource.TrySetResult(cancelResult ?? new CancelResult("Default Reason"));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            if (_state.Value == ProcessState.Running || _state.Value == ProcessState.Paused) {
                ((IProcess)this).Cancel(new CancelResult("Disposed before completion."));
            }

            _state.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        protected virtual void OnStart() { }
        protected virtual void OnPause() { }
        protected virtual void OnUnPause() { }
        protected virtual void OnCancel(CancelResult cancelResult) { }
        protected virtual void OnDispose() { }

        /// <summary>
        /// プロセス完了イベントの発火（※派生クラス用）
        /// </summary>
        protected void TriggerComplete(CompleteResult result) {
            if (_state.Value != ProcessState.Running)
                throw new InvalidOperationException("Process can only be completed when running.");

            _state.Value = ProcessState.Completed;

            _completionSource.TrySetResult(result);
        }
    }
}
