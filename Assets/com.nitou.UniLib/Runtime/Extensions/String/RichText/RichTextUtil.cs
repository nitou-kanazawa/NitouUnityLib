using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [参考]
//  Document : サポートされるリッチテキストタグ https://docs.unity3d.com/ja/2022.3/Manual/UIE-supported-tags.html
//  _: TextMeshProのリッチテキストタグ一覧 https://madnesslabo.net/utage/?page_id=12903

namespace nitou.RichText {

    /// <summary>
    /// 文字列をリッチテキストへ変換する拡張メソッド集
    /// </summary>
    public static class RichTextUtil {

        /// <summary>
        /// カラータグを挿入する
        /// </summary>
        public static string WithColorTag(this string @this, Color color) {
            return string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
                (byte)(color.r * 255f),
                (byte)(color.g * 255f),
                (byte)(color.b * 255f),
                @this
            );
        }

        /// <summary>
        /// 太字タグを挿入する
        /// </summary>
        public static string WithBoldTag(this string @this) {
            return $"<b>{@this}</b>";
        }

        /// <summary>
        /// 斜体タグを挿入する
        /// </summary>
        public static string WithItalicTag(this string @this) {
            return $"<i>{@this}</i>";
        }

        /// <summary>
        /// サイズタグを挿入する
        /// </summary>
        public static string WithSizeTag(this string @this, int size) {
            return $"<size={size}>{@this}</size>";
        }

        /// <summary>
        /// インデントタグを挿入する
        /// </summary>
        public static string WithIndentTag(this string @this, int charNum = 1) {
            return $"<indent={Mathf.Clamp(charNum, 1, 20)}em>{@this}</indent>";
        }
    }
}