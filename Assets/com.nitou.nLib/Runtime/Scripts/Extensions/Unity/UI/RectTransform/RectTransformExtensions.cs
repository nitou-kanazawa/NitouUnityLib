using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// [参考]
//  ねこじゃらシティ: RectTransformのサイズをスクリプトから変更する https://nekojara.city/unity-rect-transform-size
//  _: nGUIなどのRectTransformのwidthやheightなどの値を変更する方法 https://nekosuko.jp/1792/
//  Unity Forums: Best algorithm to clamp a UI window within the canvas? https://forum.unity.com/threads/best-algorithm-to-clamp-a-ui-window-within-the-canvas.314034/
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

        // [NOTE] RectTransform.GetCornersは0:左下、1左上、2:右上、3:右下の順で点が格納される

        // 計算用
        private static readonly Vector3[] _corners = new Vector3[4];
        private static readonly Vector3[] _corners2 = new Vector3[4];

        // 定数
        private const int CORNER_COUNT = 4;

        /// <summary>
        /// Rectの各コーナー
        /// </summary>
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

            return _corners[(int)corner];  // ※Zは無視
        }

        /// <summary>
        /// ワールド座標での中心位置を取得する
        /// </summary>
        public static Vector2 GetWorldCenterPosition(this RectTransform self) {
            self.GetWorldCorners(_corners);

            return _corners.GetCenter();
        }

        /// <summary>
        /// ワールド座標でのサイズを取得する
        /// </summary>
        public static Vector2 GetWorldSize(this RectTransform self) {
            self.GetWorldCorners(_corners);

            var min = _corners[(int)Corner.Min];
            var max = _corners[(int)Corner.Max];
            return max - min;
        }

        /// <summary>
        /// ワールド座標での位置とサイズを取得する
        /// </summary>
        public static Rect GetWorldRect(this RectTransform self) {
            self.GetWorldCorners(_corners);

            var min = _corners[(int)Corner.Min];
            var max = _corners[(int)Corner.Max];
            return new Rect(min, max -min);
        }
        #endregion


        // ----------------------------------------------------------------------------
        #region SCREEN座標

        // [参考]
        //  テラシュール: Screenの座標とWorld（3D）座標の変換について https://tsubakit1.hateblo.jp/entry/2016/03/01/020510
        //  LIGHT11: uGUIにアルファ付きのマスクを掛ける https://light11.hatenadiary.com/entry/2019/04/24/232041

        /// <summary>
        /// スクリーン座標での位置を取得する
        /// </summary>
        public static Vector2 GetScreenPosition(this RectTransform self, Canvas canvas = null, Corner corner = Corner.Min) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));
            if (canvas == null) {
                canvas = self.GetBelongedCanvas();
            }

            // ワールド座標
            var worldPos = self.GetWorldPosition(corner);

            // スクリーン座標
            return canvas.renderMode switch {
                RenderMode.ScreenSpaceOverlay => worldPos,
                RenderMode.ScreenSpaceCamera => RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldPos),
                RenderMode.WorldSpace => RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos),
                _ => throw new System.NotImplementedException()
            };
        }

        /// <summary>
        /// スクリーン座標での位置を取得する
        /// </summary>
        public static Rect GetScreenRect(this RectTransform self, Canvas canvas = null) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));
            if (canvas == null) {
                canvas = self.GetBelongedCanvas();
            }

            // ワールド座標
            var worldMin = self.GetWorldPosition(Corner.Min);
            var worldMax = self.GetWorldPosition(Corner.Max);

            // スクリーン座標
            Vector2 screenMin, screenMax;
            switch (canvas.renderMode) {
                case RenderMode.ScreenSpaceOverlay: {
                        screenMin = worldMin;
                        screenMax = worldMax;
                        return RectUtil.MinMaxRect(screenMin, screenMax);
                    }
                case RenderMode.ScreenSpaceCamera: {
                        screenMin = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldMin);
                        screenMax = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldMax);
                        return RectUtil.MinMaxRect(screenMin, screenMax);
                    }
                case RenderMode.WorldSpace: {   // ※WorldCanvas基準のRectが必要なら相対座標を使用する
                        screenMin = RectTransformUtility.WorldToScreenPoint(Camera.main, worldMin);
                        screenMax = RectTransformUtility.WorldToScreenPoint(Camera.main, worldMax);
                        return RectUtil.MinMaxRect(screenMin, screenMax);
                    }
                default:
                    throw new System.NotImplementedException();
            }
        }
        #endregion



        // ----------------------------------------------------------------------------
        #region VIEW PORT座標

        /// <summary>
        /// ビューポート座標系での位置を取得する
        /// </summary>
        public static Vector2 GetViewportPos(this RectTransform self, Canvas canvas = null, Corner corner = Corner.Min) {

            // スクリーン座標
            var screenPos = self.GetScreenPosition(canvas, corner);
            
            // ビューポート座標
            return new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
        }

        /// <summary>
        /// ビューポート座標系での位置とサイズを取得する
        /// </summary>
        public static Rect GetViewportRect(this RectTransform self, Canvas canvas = null) {
            
            // スクリーン座標
            var screenRect = self.GetScreenRect(canvas);

            // ビューポート座標
            var viewportMin = new Vector2(screenRect.min.x / Screen.width, screenRect.min.y / Screen.height);
            var viewportSize = new Vector2(screenRect.size.x / Screen.width, screenRect.size.y / Screen.height);
            return new Rect(viewportMin, viewportSize);
        }

        #endregion


        // ----------------------------------------------------------------------------
        #region RELATIVE座標

        // [FIXME] どちらに対する相対位置、サイズなのか曖昧なのを統一する

        /// <summary>
        /// 相対的な位置を取得する
        /// </summary>
        public static Vector2 GetRelativePosition(this RectTransform self, Vector2 position) {
            if (self == null) throw new System.ArgumentNullException(nameof(self), "self recttransform cannot be null.");

            // ワールド座標での位置 (※pixel座標ではない)
            var selfRect = self.GetWorldRect();

            // 相対位置
            return RectUtil.GetRelativePosition(position, selfRect);
        }

        /// <summary>
        /// 相対的な位置とサイズを取得する
        /// </summary>
        public static Rect GetRelativeRect(this RectTransform self, RectTransform other) {
            if (self == null) throw new System.ArgumentNullException(nameof(self), "self recttransform cannot be null.");
            if (other == null) throw new System.ArgumentNullException(nameof(other), "other recttransform cannot be null.");

            // ワールド座標での位置・サイズ (※pixel座標ではない)
            var selfRect = self.GetWorldRect();
            var otherRect = other.GetWorldRect();

            // 相対位置
            var relativePos = new Vector2(
                (selfRect.position.x - otherRect.position.x) / otherRect.size.x,
                (selfRect.position.y - otherRect.position.y) / otherRect.size.y);
            // 相対サイズ
            var relativeSize = new Vector2(
                selfRect.size.x / otherRect.size.x,
                selfRect.size.y / otherRect.size.y);

            return new Rect(relativePos, relativeSize);
        }

        #endregion




        private static Vector3 GetCenter(this Vector3[] corners) {
            return (corners[(int)Corner.Min] + corners[(int)Corner.Max]) / 2f;
        }

        private static Vector2 GetCenter(Vector2 min, Vector2 max) {
            return (min + max) / 2f;
        }








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
        #region その他

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


}
