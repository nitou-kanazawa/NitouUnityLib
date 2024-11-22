using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Test {

    public class ProcessExecuter {

        private readonly IEnumerable<IProcess> _processes;
        private bool _isPaused;
        private bool _isRunning;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProcessExecuter(IEnumerable<IProcess> processes) {
            _processes = processes ?? throw new ArgumentNullException(nameof(processes));
        }

        /// <summary>
        /// 
        /// </summary>
        public async UniTask Run(CancellationToken token) {

            if (_isRunning) {
                Debug.LogWarning("Execution is already running.");
                return;
            }
            _isRunning = true;

            Debug.Log("Run processes.");

            try {

                foreach (var process in _processes) {
                    process.OnBeforeRun();
                }


                foreach (var process in _processes) {
                    // 非同期に一時停止を待つ処理
                    while (_isPaused) {
                        await UniTask.Yield(PlayerLoopTiming.Update, token);
                    }

                    token.ThrowIfCancellationRequested();  // 外部からのキャンセル確認

                    await process.RunAsync(token);
                }
            } catch (OperationCanceledException) {
                Debug.Log("Execution was cancelled.");
            } finally {
                _isRunning = false;
            }

        }

        public void Pause() {
            _isPaused = true;
            foreach(var process in _processes) { process.Pause(); }
            Debug.Log("Execution paused.");
        }

        public void Resume() {
            _isPaused = false;
            foreach(var process in _processes) { process.Resume(); }
            Debug.Log("Execution resumed.");
        }
    }
}
