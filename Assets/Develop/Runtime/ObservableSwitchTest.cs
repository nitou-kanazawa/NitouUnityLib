using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using nitou;

// [REF]
//  Hatena: オペレータのSwitchについて https://shitakami.hateblo.jp/entry/2021/08/22/204549
//  qiita: UniTaskをCancellationTokenを指定しながらToObservableするメモ https://qiita.com/toRisouP/items/8ec18d73d9e8c5169587

namespace Project {

    public class ObservableSwitchTest : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _logText = null;
        [SerializeField] private TMP_InputField _inputField = null;
        [SerializeField] private Image _image = null;

        private void Start() {
            _inputField.OnValueChangedAsObservable()
                // 非同期読み込み処理を実行 (※CancellationDisposableを返す)
                .Select(path => ObservableConverter.FromUniTask(ct => LoadSpriteAsync(path, ct)))
                // 最新のIObservableに切り替える
                .Switch()
                .Subscribe(sprite => _image.sprite = sprite)
                .AddTo(this);
        }

        // スプライト読み込み
        private async UniTask<Sprite> LoadSpriteAsync(string resourcePath, CancellationToken token = default) {
            var sprite = await Resources.LoadAsync<Sprite>(resourcePath) as Sprite;
            
            // 仮に3秒程かかるとする
            await UniTask.WaitForSeconds(2f, cancellationToken: token);
            return sprite;
        }
    }
}
