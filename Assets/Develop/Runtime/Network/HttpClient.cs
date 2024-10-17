using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

// [参考]
//  qiita:  UniTask勉強会～実装例を見ながら～

namespace nitou.Networking {

    /// <summary>
    /// 通信処理を行うクライアント
    /// </summary>
    public interface IHttpClient {

        /// <summary>
        /// リクエストを送信する
        /// </summary>
        UniTask<(HttpRequest.Result result, T response)> SendAsync<T>(HttpRequest request, CancellationToken token)
            where T : HttpResponse, new();
    }


    public class HttpClient: IHttpClient{

        /// <summary>
        /// リクエストから通信を行い、レスポンスを取得する．
        /// 成功/失敗とレスポンスを返す．
        /// </summary>
         public async UniTask<(HttpRequest.Result result, T response)> SendAsync<T>(HttpRequest request, CancellationToken token) 
            where T : HttpResponse, new() {

            // Web Request
            var webRequest = UnityWebRequest.Get(request.Path);
            await webRequest.SendWebRequest().ToUniTask(cancellationToken:token);

            // 簡易版のため、通信処理を行わずに成功とデフォルトのオブジェクトを返す
            await UniTask.Yield();
            var mockResponse = new T();

            if (webRequest.isHttpError) {

                
            }

            return (new HttpRequest.Success(), mockResponse);
        }
    }
}
