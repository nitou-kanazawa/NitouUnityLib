using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Demo.Sequencer {

    public class SequenceManager  {

        private readonly List<ISequence> _sequences = new ();

        /// <summary>
        /// シーケンスを設定します。
        /// </summary>
        public void SetSequences(List<ISequence> sequences) {
            _sequences.Clear();
            _sequences.AddRange(sequences);
        }

        /// <summary>
        /// ステージを実行します。
        /// </summary>
        public async UniTask RunStage(CancellationToken token) {
            foreach (var sequence in _sequences) {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException();

                await sequence.Run(token);
            }
        }
    }
}
