using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using nitou.EditorShared;
#endif

namespace nitou.DebugInternal {
    using nitou.Inspector;

    /// <summary>
    /// <see cref="RectTransform"/>のRect範囲をViewport情報として可視化するデバッグ用コンポーネント
    /// </summary>
    [RequireComponent(typeof(RequireComponent))]
    internal class RectTransformViewportDebugger : DebugComponent<RectTransform> {

        /// <summary>
        /// 描画モード
        /// </summary>
        public enum Mode {
            Screen,
            Viewport,
        }

        private RectTransform _rectTrans;
        private Canvas _canvas;

        public TextAnchor minAlignment = TextAnchor.UpperRight;
        public TextAnchor maxAlignment = TextAnchor.LowerLeft;

        [Min(1)] public int fontSize = 20;

        [EnumToggle]
        public Mode mode;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void OnValidate() {
            _rectTrans = gameObject.GetComponent<RectTransform>();
        }

#if UNITY_EDITOR
        private void OnGUI() {
            if (_rectTrans == null) return;

            if (_canvas == null) {
                _canvas = _rectTrans.GetComponentInParent<Canvas>();
            }

            var rect = _rectTrans.GetScreenRect();

            EditorUtil.ScreenGUI.Box(rect);

            (var minText, var maxText) = GetPositionString(mode);

            // Min point
            EditorUtil.ScreenGUI.AuxiliaryLine(rect.min, 2f, Colors.Gray);
            EditorUtil.ScreenGUI.Label(rect.min, minText, fontSize, minAlignment);

            // Max point
            EditorUtil.ScreenGUI.AuxiliaryLine(rect.max, 2f, Colors.Gray);
            EditorUtil.ScreenGUI.Label(rect.max, maxText, fontSize, maxAlignment);
        }
#endif


        /// ----------------------------------------------------------------------------
        // Private Method

        public (string min, string max) GetPositionString(Mode mode) {
            var rect = mode switch {
                Mode.Screen => _rectTrans.GetScreenRect(_canvas),
                Mode.Viewport => _rectTrans.GetViewportRect(_canvas),
                _ => throw new System.NotImplementedException()
            };

            return (rect.min.ToString("F2"), rect.max.ToString("F2"));
        }
    }
}
