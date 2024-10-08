using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [参考]
//  qiita: Cinemachineを使わないお手軽なカメラワーク設計 https://qiita.com/flankids/items/0a4f70c9bfb6d20f20eb

namespace nitou.CameraModule {

    /// <summary>
    /// <see cref="RectTransform">UI要素</see>に基づいてカメラのViewport設定を制御するコンポーネント
    /// </summary>
    [RequireComponent(typeof(Camera))]
    [DisallowMultipleComponent]
    public class CameraViewportController : MonoBehaviour , ICameraController{
        
        private Camera _camera;

        // 参照するUI要素
        public RectTransform TargetRect { get; private set; }
        private RectTransform _canvasRect = null;

        // パラメータ設定
        public bool apply = true;
        public float smoothTime = 0.05f;

        // 内部処理用
        private Rect _targetViewport;           // ※この値にカメラのViewport値を追従させる 
        private SmoothDamper _followingService; // 目標値に滑らかに追従させるためのインスタンス
        private static readonly Rect defaultViewport = new(Vector2.zero, Vector2.one);

        /// <summary>
        /// 制御対象
        /// </summary>
        public Camera Camera => _camera;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void OnValidate() {
            if (_camera == null) {
                _camera = gameObject.GetComponent<Camera>();
            }
            if (TargetRect != null) {
                _followingService.Reset(_camera.rect);
            }
        }

        private void OnEnable() {
            if (TargetRect != null && apply) {
                // 指定RectTransform
                var viewportRect = TargetRect.GetViewportRect();
                _camera.rect = viewportRect;
            } else {
                // 全画面
                _camera.rect = defaultViewport;
            }
            _followingService.Reset(_camera.rect);
        }

        private void OnDisable() {
            _camera.rect = defaultViewport;
            _followingService.Reset(_camera.rect);
        }

        private void Update() {
            if (_camera == null)
                return;

            // Viewportの目標値を設定
            _targetViewport = (TargetRect != null && apply)
                ? TargetRect.GetRelativeRect(_canvasRect)
                : defaultViewport;

            // 目標値へ追従
            _camera.rect = _followingService.GetNext(_targetViewport, smoothTime);
        }


        public void SetTarget(RectTransform target) {
            TargetRect = target;
            _canvasRect = target != null ? target.GetBelongedCanvas().GetComponent<RectTransform>() : null;
        }


        /// ----------------------------------------------------------------------------
        #region Util

        /// <summary>
        /// Vector2.SmoothDamp()のラッパー
        /// </summary>
        private struct SmoothDamper {
            // 位置情報
            private Vector2 _currentPos;
            private Vector2 _currentVelocity;

            // サイズ情報
            private Vector2 _currentSize;
            private Vector2 _currentSizeVelocity;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public SmoothDamper(Rect initialValue) {
                // 初期値を設定
                _currentPos = initialValue.position;
                _currentSize = initialValue.size;

                // 速度
                _currentVelocity = Vector2.zero;
                _currentSizeVelocity = Vector2.zero;
            }

            /// <summary>
            /// 平滑化された値を取得する
            /// </summary>
            public Rect GetNext(Rect target, float smoothTime) {
                // 位置
                _currentPos = Vector2.SmoothDamp(
                    _currentPos,
                    target.position,
                    ref _currentVelocity,
                    smoothTime
                    );

                // サイズ
                _currentSize = Vector2.SmoothDamp(
                    _currentSize,
                    target.size,
                    ref _currentSizeVelocity,
                    smoothTime
                    );

                return new Rect(_currentPos, _currentSize);
            }

            public void Reset(Rect current) {
                // 現在の値と同期
                _currentPos = current.position;
                _currentSize = current.size;

                // 速度をリセット
                _currentVelocity = Vector2.zero;
                _currentSizeVelocity = Vector2.zero;
            }
        }
        #endregion
    }
}

