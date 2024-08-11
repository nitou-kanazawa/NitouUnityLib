using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// [参考]
// ねこじゃらシティ: RectTransformのサイズをスクリプトから変更する https://nekojara.city/unity-rect-transform-size
// _: nGUIなどのRectTransformのwidthやheightなどの値を変更する方法 https://nekosuko.jp/1792/
// Unity Forums: Best algorithm to clamp a UI window within the canvas? https://forum.unity.com/threads/best-algorithm-to-clamp-a-ui-window-within-the-canvas.314034/
//  Hatena: RectTransformのスクリーン座標のRectを取得する https://hacchi-man.hatenablog.com/entry/2020/12/11/220000

namespace nitou {

        // [NOTE]
        //  ユースケースをメモしておく
        //  ・
        //  ・
        //  ・
        //  ・
        //  ・

    /// <summary>
    /// <see cref="RectTransform"/>の基本的な拡張メソッド集
    /// </summary>
    public static partial class RectTransformExtensions {

        // [NOTE]
        //  RectTransform.GetCornersは0:左下、1左上、2:右上、3:右下の順で点が格納される

        // 計算用
        private static readonly Vector3[] _corners = new Vector3[4];
        private static readonly Vector3[] _corners2 = new Vector3[4];

        // 定数
        private const int CORNER_COUNT = 4;
        private const int Min = 0;          // Left bottom  ※左下から時計回りに格納される
        private const int MinX_MaxY = 1;    // Left top
        private const int Max = 2;          // Right top
        private const int MaxX_MinY = 3;    // Right bottom

        public enum Corner {
            Min = 0,
            MinX_MaxY = 1,
            Max = 2,
            MaxX_MinY = 3
        }


        // ----------------------------------------------------------------------------
        #region WORLD座標

        /// <summary>
        /// ワールド座標での位置を取得する
        /// </summary>
        public static Vector2 GetWorldPosition(this RectTransform self, Corner corner = Corner.Min) {
            self.GetWorldCorners(_corners);
            return _corners[Min];  // ※Zは無視
        }

        /// <summary>
        /// ワールド座標でのサイズを取得する
        /// </summary>
        public static Vector2 GetWorldSize(this RectTransform self) {
            self.GetWorldCorners(_corners);
            float width = Vector3.Distance(_corners[Min], _corners[MaxX_MinY]);
            float height = Vector3.Distance(_corners[Min], _corners[MinX_MaxY]);
            return new Vector2(width, height);
        }

        /// <summary>
        /// ワールド座標での位置とサイズを取得する
        /// </summary>
        public static Rect GetWorldRect(this RectTransform self) {
            self.GetWorldCorners(_corners);
            Vector2 pos = _corners[0];  // ※Zは無視
            float width = Vector3.Distance(_corners[0], _corners[3]);
            float height = Vector3.Distance(_corners[0], _corners[1]);
            return new Rect(pos, new Vector2(width, height));
        }
        #endregion

        /// <summary>
        /// キャンバスに対する相対位置(0~1)を取得する
        /// </summary>
        public static Rect GetRelativeRect(this RectTransform self, RectTransform other) {
            if (self == null) throw new System.ArgumentNullException(nameof(self), "self recttransform cannot be null.");
            if (other == null) throw new System.ArgumentNullException(nameof(other), "other recttransform cannot be null.");

            // ワールド座標での位置・サイズ (※pixel座標ではない)
            var canvasRect = other.GetWorldRect();
            var selfRect = self.GetWorldRect();

            // 相対位置
            var relativePos = new Vector2(
                (selfRect.position.x - canvasRect.position.x) / canvasRect.size.x,
                (selfRect.position.y - canvasRect.position.y) / canvasRect.size.y);
            // 相対サイズ
            var relativeSize = new Vector2(
                selfRect.size.x / canvasRect.size.x,
                selfRect.size.y / canvasRect.size.y);

            return new Rect(relativePos, relativeSize);
        }


        // ----------------------------------------------------------------------------
        #region VIEW PORT座標

        public static Vector2 GetViewportPos(this RectTransform self, Canvas canvas = null) {
            if (canvas == null) {
                canvas = self.GetBelongedCanvas();
            }
            Vector2 bottomLeftViewportPos = Vector2.zero;

            // Rect左下のワールド座標
            self.GetWorldCorners(_corners);
            Vector3 bottomLeftWorld = _corners[0];

            // Canvasのモード毎の処理
            switch (canvas.renderMode) {
                case RenderMode.ScreenSpaceOverlay: {
                        // スクリーン空間のオーバーレイモード
                        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, bottomLeftWorld);
                        bottomLeftViewportPos = new Vector3(screenPos.x / Screen.width, screenPos.y / Screen.height, 0);
                        break;
                    }
                case RenderMode.ScreenSpaceCamera:
                case RenderMode.WorldSpace: {
                        // ワールド座標をビューポート座標に変換
                        bottomLeftViewportPos = Camera.main.WorldToViewportPoint(bottomLeftWorld);
                        break;
                    }
            }
            return bottomLeftViewportPos;
        }

        
        #endregion


        // ----------------------------------------------------------------------------
        #region SCREEN座標

        public static Vector2 GetScreenPos(this RectTransform self, Canvas canvas = null) {
            if (canvas == null) {
                canvas = self.GetBelongedCanvas();
            }

            // Rect左下のワールド座標
            self.GetWorldCorners(_corners);
            Vector3 bottomLeftWorld = _corners[0];

            // Canvasのモード毎の処理
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, bottomLeftWorld);
            return screenPos;
        }

        public static Rect GetScreenRect(this RectTransform self, Canvas canvas = null) {
            var position = self.GetScreenPos(canvas);
            return new Rect(position, self.rect.size);
        }


        public static Rect GetScreenRect(this RectTransform self, Camera camera) {
            self.GetWorldCorners(_corners);
            if (camera != null) {
                _corners[Min] = RectTransformUtility.WorldToScreenPoint(camera, _corners[Min]);
                _corners[Max] = RectTransformUtility.WorldToScreenPoint(camera, _corners[Max]);
            }

            var rect = new Rect {
                x = _corners[Min].x,
                y = _corners[Min].y,
                width = _corners[Max].x - _corners[Min].x,
                height = _corners[Max].y - _corners[Min].y
            };
            return rect;
        }

        public static Rect GetScreenRect(this RectTransform self, PointerEventData data) {
            return self.GetScreenRect(data.pressEventCamera);
        }


        //public static Rect GetScreenRect(this RectTransform self) {
        //    var canvas = self.GetComponentInParent<Canvas>();
        //    return self.GetScreenRect(canvas.worldCamera);
        //}


        #endregion


        // ----------------------------------------------------------------------------
        #region コンポーネント取得

        /// <summary>
        /// 親階層をたどって所属する<see cref="Canvas"/>を取得する拡張メソッド
        /// </summary>
        public static Canvas GetBelongedCanvas(this RectTransform self) {
            var currentTrans = self.transform;
            while (currentTrans != null) {
                if (currentTrans.TryGetComponent<Canvas>(out var canvas)) {
                    return canvas;
                }
                currentTrans = currentTrans.parent;
            }
            return null;
        }

        /// <summary>
        /// 親階層をたどって所属する<see cref="CanvasScaler"/>を取得する拡張メソッド
        /// </summary>
        public static CanvasScaler GetBelongedCanvasScaler(this RectTransform self) {
            var canvas = self.GetBelongedCanvas();
            if (canvas is null) return null;

            return canvas.GetComponent<CanvasScaler>();
        }

        #endregion








        /// ----------------------------------------------------------------------------
        #region 変換

        // [参考]
        //  テラシュール: Screenの座標とWorld（3D）座標の変換について https://tsubakit1.hateblo.jp/entry/2016/03/01/020510


        //public static Vector2 foo(Vector3 worldPoint) {

        //    return Vector2.zero;
        //}


        #endregion


        // ----------------------------------------------------------------------------
        #region 重なり判定

        // [参考]
        //  kanのメモ帳: RectTransformが重なっているか(衝突しているか)をコライダーを使わないで判定する拡張メソッド https://kan-kikuchi.hatenablog.com/entry/RectTransform_IsOverlapping
        //  Hatena: 特定のRectTransformの範囲内かどうか判定する https://hacchi-man.hatenablog.com/entry/2020/05/09/220000

        /// <summary>
        /// 他の<see cref="RectTransform"/>と重なっているか判定する拡張メソッド
        /// </summary>
        public static bool Overlaps(this RectTransform self, RectTransform othrer) {

            // コーナー座標を取得
            self.GetWorldCorners(_corners);
            othrer.GetWorldCorners(_corners2);

            // 各コーナーをチェック
            for (var i = 0; i < CORNER_COUNT; i++) {

                //rect1の角がrect2の内部にあるか
                if (IsPointInsideRect(_corners[i], _corners2)) {
                    return true;
                }
                //rect2の角がrect1の内部にあるか
                if (IsPointInsideRect(_corners2[i], _corners)) {
                    return true;
                }
            }

            return false;
        }

        // [参考]
        //  _: 点の多角形に対する内外判定 https://www.nttpc.co.jp/technology/number_algorithm.html
        private static bool IsPointInsideRect(Vector2 point, Vector3[] rectCorners) {
            var inside = false;

            //rectCornersの各頂点に対して、pointがrect内にあるかを確認
            for (int i = 0, j = 3; i < CORNER_COUNT; j = i++) {
                // ※交差回数で内外判定
                if (((rectCorners[i].y > point.y) != (rectCorners[j].y > point.y)) &&
                    (point.x < (rectCorners[j].x - rectCorners[i].x) * (point.y - rectCorners[i].y) / (rectCorners[j].y - rectCorners[i].y) + rectCorners[i].x)) {
                    // 交差する度に切り替え
                    inside = !inside;
                }
            }

            return inside;
        }


        // 


        public static bool Contains(this RectTransform self, RectTransform other) {
            var selfBounds = GetBounds(self);
            var otherBounds = GetBounds(other);

            return selfBounds.Contains(new Vector3(otherBounds.min.x, otherBounds.min.y, 0f)) &&
                   selfBounds.Contains(new Vector3(otherBounds.max.x, otherBounds.max.y, 0f)) &&
                   selfBounds.Contains(new Vector3(otherBounds.min.x, otherBounds.max.y, 0f)) &&
                   selfBounds.Contains(new Vector3(otherBounds.max.x, otherBounds.min.y, 0f));
        }


        /// <summary>
        /// 計算用の<see cref="Bounds"/>を取得する
        /// </summary>
        private static Bounds GetBounds(this RectTransform self) {
            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // 最小、最大の取得
            self.GetWorldCorners(_corners);
            for (var i = 0; i < CORNER_COUNT; i++) {
                min = Vector3.Min(_corners[i], min);
                max = Vector3.Max(_corners[i], max);
            }

            max.z = 0f;
            min.z = 0f;

            // AABBを定義
            var bounds = new Bounds(min, Vector3.zero);
            bounds.Encapsulate(max);
            return bounds;
        }

        #endregion




        /// ----------------------------------------------------------------------------

        



        #region 重なり判定


        /// ----------------------------------------------------------------------------
        // サイズ設定

        /// <summary>
        /// Widthを設定する拡張メソッド
        /// </summary>
        public static void SetWidth(this RectTransform self, float width) {
            self.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        /// <summary>
        /// Heightを設定する拡張メソッド
        /// </summary>
        public static void SetHeight(this RectTransform self, float height) {
            self.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        /// <summary>
        /// サイズを指定する拡張メソッド
        /// </summary>
        public static void SetSize(this RectTransform self, Vector2 size) {

            // 親要素のサイズ取得
            var parent = self.parent as RectTransform;
            var parentSize = parent != null ? parent.rect.size : Vector2.zero;

            // 自身のアンカーサイズを計算
            var anchorSize = parentSize * (self.anchorMax - self.anchorMin);

            // 入力サイズからアンカーサイズを引いた結果が
            // sizeDeltaに指定すべき値
            self.sizeDelta = size - anchorSize;
        }


        /// ----------------------------------------------------------------------------
        // ピボット設定

        /// <summary>
        /// ピボットXを設定する拡張メソッド
        /// </summary>
        public static void SetPivotX(this RectTransform self, float x) {
            self.pivot = new Vector2(Mathf.Clamp01(x), self.pivot.y);
        }

        /// <summary>
        /// ピボットYを設定する拡張メソッド
        /// </summary>
        public static void SetPivotY(this RectTransform self, float y) {
            self.pivot = new Vector2(self.pivot.x, Mathf.Clamp01(y));
        }

        #endregion
    }


    public static class RectTransformUtil {



    }

}
