using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Demo.Sequencer {

    public class SceneEntryPoint : MonoBehaviour {


        private SequenceManager _inGameManager;

        private  async void Start() {


            try {
                //await SomProcessAsync();
                SomProcessAsync().Forget();

            } catch (Exception e) {

                Debug.Log($"Catch error : {e}");
            }


            //_inGameManager = new SequenceManager();

            //var sequences = new List<ISequence>{
            //    new OpeningSequence("Welcome to the stage!"),
            //    new MainSequence(),
            //    new ResultSequence()
            //};

            //_inGameManager.SetSequences(sequences);
            //RunGame().Forget();
        }

        private async UniTask SomProcessAsync() {
            await UniTask.RunOnThreadPool(() => throw new Exception("ExampleException"));
        }


        private async UniTaskVoid RunGame() {

            var cts = new CancellationTokenSource();
            try {
                await _inGameManager.RunStage(cts.Token);
            } catch (OperationCanceledException) {
                Debug.Log("Game canceled.");
            }

        }

    }
}
