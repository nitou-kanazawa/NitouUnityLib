using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Test {
    
    public class ImageFillProcess : IProcess {
        
        private readonly Image _image;
        private readonly float _duration;
        private bool _isPaused;

        public ImageFillProcess(Image image, float duration) {
            _image = image;
            _duration = duration;
        }

        public async UniTask RunAsync(CancellationToken token) {
            float initialFill = _image.fillAmount;
            float elapsedTime = 0f;

            while (elapsedTime < _duration) {
                token.ThrowIfCancellationRequested();

                if (_isPaused) {
                    await UniTask.WaitUntil(() => !_isPaused, cancellationToken: token);
                }

                elapsedTime += Time.deltaTime;
                _image.fillAmount = Mathf.Lerp(initialFill, 1f, elapsedTime / _duration);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
            _image.fillAmount = 1f; // Š®—¹Žž‚ÍŠmŽÀ‚É1‚É‚·‚é
        }

        public void Pause() => _isPaused = true;
        public void Resume() => _isPaused = false;
    }
}
