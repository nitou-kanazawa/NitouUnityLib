using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;



public class RetryOnErrorExample : MonoBehaviour {
    void Start() {
        Observable.Timer(TimeSpan.FromSeconds(3))  // 3秒後に開始
            .SelectMany(_ => FetchAsync().ToObservable())  // 非同期のデータ取得メソッド
            .Retry(3)  // 最大3回リトライ
            .Catch<string, Exception>((ex) => {  // すべて失敗した場合の処理
                Debug.Log("3回失敗しました: " + ex.Message);
                return Observable.Empty<string>();  // エラー時に空のObservableを返す
            })
            .Subscribe(
                result => Debug.Log("成功: " + result),  // 成功した場合の処理
                error => Debug.Log("エラー発生: " + error.Message)  // エラー発生時の処理
            );
    }

    // 非同期データ取得メソッド（例）
    private async UniTask<string> FetchAsync() {
        // ここではエラーを発生させてみます（本来はデータ取得処理を実装）
        bool isSuccess = UnityEngine.Random.Range(0, 2) == 0;  // ランダムに成功/失敗を決める
        Debug.Log("try fetch");
        await UniTask.WaitForSeconds(1);  // 擬似的な非同期処理
        if (isSuccess) {
            return "データ取得成功";
        } else {
            throw new Exception("データ取得失敗");
        }
    }
}





// 5
public class DataBindingExample : MonoBehaviour {
    private ReactiveProperty<string> inputText = new ReactiveProperty<string>("");

    void Start() {
        // ここにinputTextの変更が1秒ごとにログに出力されるように設定
    }

    public void OnInputChanged(string text) {
        inputText.Value = text;
    }
}


// 6
public class TimedFeedbackExample : MonoBehaviour {
    public Button targetButton;

    void Start() {
        // ここに、ボタンを押してから5秒以内に2回目の押下が行われるかチェックするロジックを記述
    }
}




// 7
public class AsyncOperationExample : MonoBehaviour {
    public Button fetchDataButton;

    void Start() {
        // ボタンをクリックで非同期操作を開始、再度クリックでキャンセルするロジックを記述
    }

    private async UniTask FetchDataAsync(CancellationToken token) {
        // 非同期でデータを取得する例として3秒待機
        await UniTask.Delay(3000, cancellationToken: token);
        Debug.Log("Data fetched");
    }
}




// 8 


// 9 
