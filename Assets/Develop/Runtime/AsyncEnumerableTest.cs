using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project {
    public class AsyncEnumerableTest : MonoBehaviour {

        public Button _button;

        private CancellationTokenSource cts = new();

        void Start() {


            _button.OnClickAsAsyncEnumerable(cts.Token)
                .ForEachAwaitAsync(async _ => {
                    Debug.Log("<color=red>実行開始！</color>");

                    // 待機                    
                    await UniTask.WaitForSeconds(3, cancellationToken: cts.Token);
                    Debug.Log("<color=green>実行終了！</color>");

                }, cts.Token);

        }

        private void OnDestroy() {
            cts.Cancel();
            cts.Dispose();

        }

    }
}
