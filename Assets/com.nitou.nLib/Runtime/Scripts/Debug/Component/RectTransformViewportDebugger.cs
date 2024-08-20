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

            // Min point
            EditorUtil.ScreenGUI.AuxiliaryLine(rect.min, 2f, Colors.Gray);
            var text = GetPositionString(RectTransformExtensions.Corner.Min, mode);
            EditorUtil.ScreenGUI.Label(rect.min, text, fontSize, minAlignment);

            // Max point
            EditorUtil.ScreenGUI.AuxiliaryLine(rect.max, 2f, Colors.Gray);
            text = GetPositionString(RectTransformExtensions.Corner.Max, mode);
            EditorUtil.ScreenGUI.Label(rect.max, text, fontSize, maxAlignment);
        }
#endif


        /// ----------------------------------------------------------------------------
        // Private Method

        public string GetPositionString(RectTransformExtensions.Corner corner, Mode mode) {
            var rect = _rectTrans.GetScreenRect();

            return mode switch {
                Mode.Screen => (corner== RectTransformExtensions.Corner.Min ? rect.min: rect.max).ToString("F1"),
                Mode.Viewport => _rectTrans.GetViewportPos(_canvas, corner).ToString("F2"),
                _ => throw new System.NotImplementedException()
            };
        }
    }
}
