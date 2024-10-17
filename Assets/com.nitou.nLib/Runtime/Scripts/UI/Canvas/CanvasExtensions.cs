using UnityEngine;

namespace nitou{

    /// <summary>
    /// <see cref="Canvas"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class CanvasExtensions{

        /// <summary>
        /// スクリーン座標取得の拡張メソッド
        /// </summary>
        public static Vector2 GetScreenPosition(this Canvas canvas, Vector2 worldPos, Camera camera = null) {
            // デフォルトカメラの設定
            camera ??= Camera.main;

            // スクリーン座標
            return canvas.renderMode switch {
                RenderMode.ScreenSpaceOverlay => worldPos,
                RenderMode.ScreenSpaceCamera => RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldPos),
                RenderMode.WorldSpace => RectTransformUtility.WorldToScreenPoint(camera, worldPos),
                _ => throw new System.NotImplementedException()
            };
        }

        /// <summary>
        /// スクリーンRect取得の拡張メソッド
        /// </summary>
        public static Rect GetScreenRect(this Canvas canvas, Vector2 worldMin, Vector2 worldMax, Camera camera = null) {
            Vector2 screenMin, screenMax;
            camera ??= Camera.main;

            // スクリーン座標
            switch (canvas.renderMode) {
                case RenderMode.ScreenSpaceOverlay:
                    screenMin = worldMin;
                    screenMax = worldMax;
                    break;
                case RenderMode.ScreenSpaceCamera:
                    screenMin = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldMin);
                    screenMax = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldMax);
                    break;
                case RenderMode.WorldSpace:
                    screenMin = RectTransformUtility.WorldToScreenPoint(camera, worldMin);
                    screenMax = RectTransformUtility.WorldToScreenPoint(camera, worldMax);
                    break;
                default:
                    throw new System.NotImplementedException();
            }

            return RectUtil.MinMaxRect(screenMin, screenMax);
        }
    }
}
