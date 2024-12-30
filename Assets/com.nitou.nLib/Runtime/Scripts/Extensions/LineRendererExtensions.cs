using System.Collections.Generic;

// [参考]
//  ねこじゃらシティ: Line Rendererで破線を描画する https://nekojara.city/unity-dashed-line

namespace UnityEngine {

    /// <summary>
    /// <see cref="LineRenderer"/>の基本的な拡張メソッド集
    /// </summary>
    public static class LineRendererExtensions {

        /// <summary>
        /// startColor, endColorを一括で設定する拡張メソッド
        /// </summary>
        public static void SetColor(this LineRenderer self, Color color) {
            self.startColor = color;
            self.endColor = color;
        }

        /// <summary>
        /// <see cref="LineRenderer.widthCurve"/>に一定幅を設定する拡張メソッド
        /// </summary>
        public static void SetConstantWidth(this LineRenderer self, float width) {
            self.widthCurve = AnimationCurve.Constant(0, 1, width);
        }


        /// ----------------------------------------------------------------------------
        // 生成

        public static float CalculateLength(this LineRenderer self) {
            if (self == null || self.positionCount < 2) return 0f; 

            var totalLegth = 0f;

            for (var i = 0; i < self.positionCount - 1; i++) {
                totalLegth += Vector3.Distance(
                    self.GetPosition(i),
                    self.GetPosition(i + 1));
            }

            // ※ループする場合は，最初と最後の頂点の距離も加算する
            if (self.loop) {
                totalLegth += Vector3.Distance(
                    self.GetPosition(0),
                    self.GetPosition(self.positionCount - 1));
            }

            return totalLegth;
        }


    }
}
