using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Demo.Sequencer
{
    public class ResultSequence : ISequence {

        /// <summary>
        /// シーケンス実行．
        /// </summary>
        public async UniTask Run(CancellationToken token) {
            DisplayResult();
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return), cancellationToken: token);
        }

        private void DisplayResult() {
            Debug.Log("Displaying results.");
        }

    }
}
