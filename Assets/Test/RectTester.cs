using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test {

    public class RectTester : MonoBehaviour {

        [SerializeField] RectTransform _canvas;
        [SerializeField] RectTransform _target;


        public Vector2 viewport;
        public float canvasLength;
        public float rectLength;


        private void Update() {
            if (_canvas == null || _target == null) {
                viewport = Vector2.zero;
                return;
            }

            // サイズ
            canvasLength = _canvas.GetGrobalWidth();
            rectLength = _target.GetGrobalWidth();

            // ビューポート座標
            viewport = _target.GetViewportPosition(_canvas);

        }


    }

    /// <summary>
    /// <see cref="RectTransform"/>の基本的な拡張メソッド集
    /// </summary>
    public static partial class RectTransformExtensions {

        // 計算用
        private static readonly Vector3[] _corners = new Vector3[4];

        #region Get Position/Size
        
        // [NOTE]
        //  RectTransform.GetCornersは0:左下、1左上、2:右上、3:右下の順で点が格納される

        /// <summary>
        /// ワールド座標での幅を取得する
        /// </summary>
        public static float GetGrobalWidth(this RectTransform self) {
            self.GetWorldCorners(_corners);
            float width = Vector3.Distance(_corners[0], _corners[3]);
            return width;
        }

        /// <summary>
        /// ワールド座標での高さを取得する
        /// </summary>
        public static float GetWorldHeight(this RectTransform self) {
            self.GetWorldCorners(_corners);
            float height = Vector3.Distance(_corners[0], _corners[1]);
            return height;
        }

        /// <summary>
        /// ワールド座標での位置とサイズを取得する
        /// </summary>
        public static (Vector2 pos, Vector2 size) GetWorldPositionAndSize(this RectTransform self) {
            self.GetWorldCorners(_corners);
            Vector2 pos = _corners[0];  // ※Zは無視
            float width = Vector3.Distance(_corners[0], _corners[3]);  
            float height = Vector3.Distance(_corners[0], _corners[1]); 
            return (pos, new Vector2(width, height));
        }

        /// <summary>
        /// キャンバスに対する相対位置(0~1)を取得する
        /// </summary>
        public static Vector2 GetViewportPosition(this RectTransform self) {
            // [NOTE]
            // Game画面での絶対座標を取得するのが意外と困難だったため、
            // Canvasから相対位置を取得する方針で実装.

            // 直近のCanvasを取得
            var canvas = self.GetParentCanvas();
            if (canvas == null) {
                Debug.LogWarning("RectTransform is not a child of a Canvas. Returning Vector2.zero.");
                return Vector2.zero;
            }
            var canvasRect = canvas.GetComponent<RectTransform>();

            // ワールド座標での位置・サイズ (※pixel座標ではない)
            var (canvasPos, canvasSize) = canvasRect.GetWorldPositionAndSize();
            var (selfPos, _) = self.GetWorldPositionAndSize();

            // キャンバスの位置・サイズで正規化した座標（※canva値域 0 ~ 1）
            return new Vector2(
                (selfPos.x - canvasPos.x) / canvasSize.x,
                (selfPos.y - canvasPos.y) / canvasSize.y);
        }
        #endregion


        #region Misc

        /// <summary>
        /// 親階層をたどって所属する<see cref="Canvas"/>を取得する拡張メソッド
        /// </summary>
        public static Canvas GetParentCanvas(this RectTransform self) {
            var currentTrans = self.transform;
            while (currentTrans != null) {
                if (currentTrans.TryGetComponent<Canvas>(out var canvas)) {
                    return canvas;
                }
                currentTrans = currentTrans.parent;
            }
            return null;
        }
        #endregion
    }

}