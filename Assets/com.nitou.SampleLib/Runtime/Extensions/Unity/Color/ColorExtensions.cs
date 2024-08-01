using UnityEngine;
using UnityEngine.UI;

// [参考]
//  ゲームUIネット : DOTweenで作成したモーション17個を含むプロジェクトを公開 https://game-ui.net/?p=975
//  _: Imageの色それぞれ変更する拡張 https://hi-network.sakura.ne.jp/wp/2021/01/26/post-3660/

namespace nitou {

    /// <summary>
    /// <see cref="Color"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class ColorExtensions {

        /// ----------------------------------------------------------------------------
        #region SpriteRenderer

        /// <summary>
        /// α値を設定する拡張メソッド
        /// </summary>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
        #endregion
               


        /// ----------------------------------------------------------------------------
        #region Misc

        /// <summary>
        /// α値を設定する拡張メソッド
        /// </summary>
        public static void SetAlphasInChildren(this GameObject obj, float alpha) {
            var spriteRenderers = obj.GetComponentsInChildren<SpriteRenderer>();
            var graphics = obj.GetComponentsInChildren<Graphic>();

            if (spriteRenderers != null) {
                foreach (var spriteRenderer in spriteRenderers) {
                    spriteRenderer.SetAlpha(alpha);
                }
            }

            if (graphics != null) {
                foreach (var graphic in graphics) {
                    graphic.SetAlpha(alpha);
                }
            }
        }
        #endregion
    }
}