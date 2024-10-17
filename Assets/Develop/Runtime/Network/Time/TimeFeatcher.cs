using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using nitou.Networking;

namespace Project{

    /// <summary>
    /// 
    /// </summary>
    public class TimeRequest : HttpRequest {
        public override string Path => "http://worldtimeapi.org/api/timezone/Etc/UTC";
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class TimeResponse : HttpResponse {
        public string datetime; // JSONの "datetime" フィールドをマッピング
    }


    public class TimeFeatcher : MonoBehaviour {

        private HttpClient httpClient;

        async void Start() {
            httpClient = new HttpClient();

            Debug.Log("リターンを押してください．");
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            Debug.Log("通信を開始します．");
            FetchTimeAsync().Forget(); // 非同期処理の開始
        }

        // 非同期でモックのAPIから時刻を取得
        async UniTaskVoid FetchTimeAsync() {
            var request = new TimeRequest();
            var token = new CancellationToken(); // 必要に応じてキャンセル処理が可能

            // モックのAPI通信を行う
            (HttpRequest.Result result, TimeResponse response) = await httpClient.SendAsync<TimeResponse>(request, token);

            if (result.IsSuccess()) {
                Debug.Log("Current UTC Time (Mock): " + response.datetime);
            } else {
                Debug.LogError("Failed to fetch time (Mock).");
            }
        }
    }
}
