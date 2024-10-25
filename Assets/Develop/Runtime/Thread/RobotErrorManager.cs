using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using nitou;

namespace Project {

    public class RobotErrorManager : MonoBehaviour {

        private List<string> errorList = new ();
        private bool isCommunicating = true;
        
        private const float CommunicationInterval = 2f; // 通信周期（秒）


        private void Start() {
            // エラー取得処理を開始
            StartReceivingErrors().Forget();
        }


        private async UniTaskVoid StartReceivingErrors() {
            // Timerを使用して一定間隔で処理を行う
            await foreach (var _ in UniTaskAsyncEnumerable.Timer(System.TimeSpan.FromSeconds(CommunicationInterval), System.TimeSpan.FromSeconds(CommunicationInterval))
                .WithCancellation(this.GetCancellationTokenOnDestroy())) {
                if (!isCommunicating) {
                    Debug_.Log("Communication stopped.", Colors.Orange);
                    break;
                }

                // ロボットからエラー情報を取得
                Debug.Log("---------------------------------------.");
                Debug.Log("Attempting to get errors from the robot.");
                var newErrors = await GetErrorsFromRobotAsync();

                // 取得したエラー情報をメインリストに追加
                lock (errorList) {
                    errorList.AddRange(newErrors);
                    Debug.Log($"Added {newErrors.Count} errors. Total errors: {errorList.Count}");
                }
            }
        }

        /// <summary>
        /// 具体的な通信処理
        /// </summary>
        private async UniTask<List<string>> GetErrorsFromRobotAsync() {
            // ロボットと通信してエラー情報を取得する処理を実装
            await UniTask.Delay(100); // 通信時間のシミュレーション
            var simulatedErrors = new List<string> { "Error1", "Error2" }; // 仮のエラー情報
            Debug.Log($"Received {simulatedErrors.Count} errors from the robot.");
            return simulatedErrors;
        }

        private void OnDestroy() {
            // 通信を停止
            isCommunicating = false;
            Debug.Log("RobotErrorManager is being destroyed.");
        }
    }
}
