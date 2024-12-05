using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Demo.Sequencer {

    public class OpeningSequence : ISequence {

        private readonly string _message;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public OpeningSequence(string message) {
            _message = message;
        }

        /// <summary>
        /// シーケンス実行．
        /// </summary>
        public async UniTask Run(CancellationToken token = default) {

            ShowMessage();

            // スキップボタンが押されるか、一定時間経過で終了
            var skipTask = WaitForSkipRequest(token);
            var timeoutTask = UniTask.Delay(TimeSpan.FromSeconds(10), cancellationToken: token);

            await UniTask.WhenAny(skipTask, timeoutTask);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void ShowMessage() {
            Debug.Log($"Opening: {_message}");
        }


        private async UniTask WaitForSkipRequest(CancellationToken token) {
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken: token);
            Debug.Log("skip");
        }

    }
}
