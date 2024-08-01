using UnityEngine;

namespace nitou {

    /// <summary>
    /// <see cref="Rect"/>の基本的な拡張メソッド集
    /// </summary>
    public static class RectExtensions {

        /// ----------------------------------------------------------------------------
        #region Set Position

        public static Rect SetPosition(this Rect rect, Vector2 position) {
            rect.position = position;
            return rect;
        }

        public static Rect SetX(this Rect rect, float x) {
            rect.x = x;
            return rect;
        }

        public static Rect SetXMax(this Rect rect, float xMax) {
            rect.xMax = xMax;
            return rect;
        }

        public static Rect SetXMin(this Rect rect, float xMin) {
            rect.xMin = xMin;
            return rect;
        }

        public static Rect SetCenter(this Rect rect, Vector2 center) {
            rect.center = center;
            return rect;
        }

        public static Rect SetCenter(this Rect rect, float x, float y) {
            rect.center = new Vector2(x, y);
            return rect;
        }

        public static Rect SetCenterX(this Rect rect, float x) {
            rect.center = new Vector2(x, rect.center.y);
            return rect;
        }

        public static Rect SetCenterY(this Rect rect, float y) {
            rect.center = new Vector2(rect.center.x, y);
            return rect;
        }

        public static Rect SetMax(this Rect rect, Vector2 max) {
            rect.max = max;
            return rect;
        }

        public static Rect SetMin(this Rect rect, Vector2 min) {
            rect.min = min;
            return rect;
        }

        public static Rect ResetPosition(this Rect rect) {
            rect.position = Vector2.zero;
            return rect;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Set Size

        public static Rect SetSize(this Rect rect, float width, float height) {
            rect.size = new Vector2(width, height);
            return rect;
        }

        public static Rect SetSize(this Rect rect, float widthAndHeight) {
            rect.size = new Vector2(widthAndHeight, widthAndHeight);
            return rect;
        }

        public static Rect SetSize(this Rect rect, Vector2 size) {
            rect.size = size;
            return rect;
        }

        public static Rect SetWidth(this Rect rect, float width) {
            rect.width = width;
            return rect;
        }

        public static Rect SetHeight(this Rect rect, float height) {
            rect.height = height;
            return rect;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Add

        public static Rect AddPosition(this Rect rect, Vector2 move) {
            rect.position += move;
            return rect;
        }

        public static Rect AddPosition(this Rect rect, float x, float y) {
            rect.position += new Vector2(x, y);
            return rect;
        }

        public static Rect AddX(this Rect rect, float x) {
            rect.position += new Vector2(x, 0);
            return rect;
        }

        public static Rect AddY(this Rect rect, float y) {
            rect.position += new Vector2(0, y);
            return rect;
        }

        public static Rect AddMax(this Rect rect, Vector2 value) {
            rect.max += value;
            return rect;
        }

        public static Rect AddMin(this Rect rect, Vector2 value) {
            rect.min += value;
            return rect;
        }

        public static Rect AddXMax(this Rect rect, float value) {
            rect.xMax += value;
            return rect;
        }

        public static Rect AddXMin(this Rect rect, float value) {
            rect.xMin += value;
            return rect;
        }

        public static Rect AddYMax(this Rect rect, float value) {
            rect.yMax += value;
            return rect;
        }

        public static Rect AddYMin(this Rect rect, float value) {
            rect.yMin += value;
            return rect;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Align

        public static Rect AlignBottom(this Rect rect, float height) {
            return new Rect(rect.x, rect.yMax - height, rect.width, height);
        }

        public static Rect AlignCenter(this Rect rect, float width) {
            return new Rect(rect.x + (rect.width - width) / 2, rect.y, width, rect.height);
        }

        public static Rect AlignCenter(this Rect rect, float width, float height) {
            return new Rect(rect.x + (rect.width - width) / 2, rect.y + (rect.height - height) / 2, width, height);
        }

        public static Rect AlignCenterX(this Rect rect, float width) {
            return new Rect(rect.x + (rect.width - width) / 2, rect.y, width, rect.height);
        }

        public static Rect AlignCenterXY(this Rect rect, float width, float height) {
            return new Rect(rect.x + (rect.width - width) / 2, rect.y + (rect.height - height) / 2, width, height);
        }

        public static Rect AlignCenterXY(this Rect rect, float size) {
            return new Rect(rect.x + (rect.width - size) / 2, rect.y + (rect.height - size) / 2, size, size);
        }

        public static Rect AlignCenterY(this Rect rect, float height) {
            return new Rect(rect.x, rect.y + (rect.height - height) / 2, rect.width, height);
        }

        public static Rect AlignLeft(this Rect rect, float width) {
            return new Rect(rect.x, rect.y, width, rect.height);
        }

        public static Rect AlignMiddle(this Rect rect, float height) {
            return new Rect(rect.x, rect.y + (rect.height - height) / 2, rect.width, height);
        }

        public static Rect AlignRight(this Rect rect, float width) {
            return new Rect(rect.xMax - width, rect.y, width, rect.height);
        }

        public static Rect AlignRight(this Rect rect, float width, bool clamp) {
            return clamp ? new Rect(Mathf.Max(rect.x, rect.xMax - width), rect.y, width, rect.height) : new Rect(rect.xMax - width, rect.y, width, rect.height);
        }

        public static Rect AlignTop(this Rect rect, float height) {
            return new Rect(rect.x, rect.y, rect.width, height);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Expand

        public static Rect Expand(this Rect rect, float left, float right, float top, float bottom) {
            return new Rect(rect.x - left, rect.y - top, rect.width + left + right, rect.height + top + bottom);
        }

        public static Rect Expand(this Rect rect, float horizontal, float vertical) {
            return new Rect(rect.x - horizontal / 2, rect.y - vertical / 2, rect.width + horizontal, rect.height + vertical);
        }

        public static Rect Expand(this Rect rect, float expand) {
            return new Rect(rect.x - expand, rect.y - expand, rect.width + 2 * expand, rect.height + 2 * expand);
        }

        public static Rect ExpandTo(this Rect rect, Vector2 pos) {
            if (!rect.Contains(pos)) {
                rect.xMin = Mathf.Min(rect.xMin, pos.x);
                rect.xMax = Mathf.Max(rect.xMax, pos.x);
                rect.yMin = Mathf.Min(rect.yMin, pos.y);
                rect.yMax = Mathf.Max(rect.yMax, pos.y);
            }
            return rect;
        }
        #endregion


        /// ----------------------------------------------------------------------------

        public static Rect HorizontalPadding(this Rect rect, float padding) {
            return new Rect(rect.x + padding, rect.y, rect.width - 2 * padding, rect.height);
        }

        public static Rect HorizontalPadding(this Rect rect, float left, float right) {
            return new Rect(rect.x + left, rect.y, rect.width - left - right, rect.height);
        }

        public static bool IsPlaceholder(this Rect rect) {
            return rect == new Rect(0, 0, 0, 0) || rect == new Rect(0, 0, 1, 1);
        }

        public static Rect MaxHeight(this Rect rect, float maxHeight) {
            if (rect.height > maxHeight) {
                rect.height = maxHeight;
            }
            return rect;
        }

        public static Rect MaxWidth(this Rect rect, float maxWidth) {
            if (rect.width > maxWidth) {
                rect.width = maxWidth;
            }
            return rect;
        }

        public static Rect MinHeight(this Rect rect, float minHeight) {
            if (rect.height < minHeight) {
                rect.height = minHeight;
            }
            return rect;
        }

        public static Rect MinWidth(this Rect rect, float minWidth) {
            if (rect.width < minWidth) {
                rect.width = minWidth;
            }
            return rect;
        }

        public static Rect Padding(this Rect rect, float left, float right, float top, float bottom) {
            return new Rect(rect.x + left, rect.y + top, rect.width - left - right, rect.height - top - bottom);
        }

        public static Rect Padding(this Rect rect, float horizontal, float vertical) {
            return new Rect(rect.x + horizontal, rect.y + vertical, rect.width - 2 * horizontal, rect.height - 2 * vertical);
        }

        public static Rect Padding(this Rect rect, float padding) {
            return new Rect(rect.x + padding, rect.y + padding, rect.width - 2 * padding, rect.height - 2 * padding);
        }


        /// ----------------------------------------------------------------------------
        #region Split

        /// <summary>
        /// 水平方向に指定数で分割する拡張メソッド
        /// </summary>
        public static Rect[] HorizontalSplit(this Rect rect, int count, float padding = 2f) {
            if (count < 1) throw new System.InvalidOperationException("");

            var rects = new Rect[count];

            float totalPadding = padding * (count - 1);   // ※要素間の隙間
            float width = (rect.width - totalPadding) / count;
            for (int i = 0; i < count; i++) {
                rects[i] = new Rect(
                    rect.x + (width + padding) * i,
                    rect.y,
                    width,
                    rect.height);
            }
            return rects;
        }

        /// <summary>
        /// 垂直方向に指定数で分割する拡張メソッド
        /// </summary>
        public static Rect[] VerticalSplit(this Rect rect, int count, float padding = 2f) {
            if (count < 1) throw new System.InvalidOperationException("");

            var rects = new Rect[count];

            var totalPadding = padding * (count - 1);
            var height = (rect.height - totalPadding) / count;
            for (int i = 0; i < count; i++) {
                rects[i] = new Rect(
                    rect.x,
                    rect.y + (height + padding) * i,
                    rect.width,
                    height);
            }
            return rects;
        }
        #endregion
    }


    public static class RectUtil {

        public static Rect Create(Vector2 center, Vector2 size) {
            Vector2 position = center - size / 2;
            return new Rect(position, size);
        }
    }

}