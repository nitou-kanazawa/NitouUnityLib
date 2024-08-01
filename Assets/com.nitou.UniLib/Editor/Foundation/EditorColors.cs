#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

// Rererence: https://www.foundations.unity.com/fundamentals/color-palette

namespace nitou.EditorShared {

    // [TODO] いい感じの２キーDictionaryを整備したい (2024.08.01)

    /// <summary>
    /// Unityエディタで使用されているカラー集
    /// </summary>
    public static class EditorColors {

        /// ----------------------------------------------------------------------------
        // Window Colors

        /// <summary>
        /// 背景色
        /// </summary>
        public static Color DefaultBackground =>
            GetColor(EditorGUIUtility.isProSkin ? "#282828" : "#A5A5A5");

        /// <summary>
        /// 背景色（非アクティブハイライト）
        /// </summary>
        public static Color HighlightBackgroundInactive =>
            GetColor(EditorGUIUtility.isProSkin ? "#4D4D4D" : "#AEAEAE");

        /// <summary>
        /// 背景色（ハイライト）
        /// </summary>
        public static Color HighlightBackground =>
            GetColor(EditorGUIUtility.isProSkin ? "#2C5D87" : "#3A72B0");

        /// <summary>
        /// 
        /// </summary>
        public static Color WindowBackground =>
            GetColor(EditorGUIUtility.isProSkin ? "#383838" : "#C8C8C8");

        /// <summary>
        /// 
        /// </summary>
        public static Color InspectorTitlebarBorder =>
            GetColor(EditorGUIUtility.isProSkin ? "#1A1A1A" : "#7F7F7F");


        /// ----------------------------------------------------------------------------
        // Button Colors

        /// <summary>
        /// ボタン背景色
        /// </summary>
        public static Color ButtonBackground =>
            GetColor(EditorGUIUtility.isProSkin ? "#585858" : "#E4E4E4");

        /// <summary>
        /// ボタン背景色（ホバー）
        /// </summary>
        public static Color ButtonBackgroundHover =>
            GetColor(EditorGUIUtility.isProSkin ? "#676767" : "#ECECEC");

        /// <summary>
        /// ボタン背景色（クリック）
        /// </summary>
        public static Color ButtonBackgroundHoverPressedr =>
            GetColor(EditorGUIUtility.isProSkin ? "#4F657F" : "#B0D2FC");


        /// ----------------------------------------------------------------------------
        // Text Colors

        /// <summary>
        /// テキスト色
        /// </summary>
        public static Color DefaultText =>
            GetColor(EditorGUIUtility.isProSkin ? "#D2D2D2" : "#090909");

        /// <summary>
        /// テキスト色（エラー）
        /// </summary>
        public static Color ErrorText =>
            GetColor(EditorGUIUtility.isProSkin ? "#D32222" : "#5A0000");

        /// <summary>
        /// テキスト色（警告）
        /// </summary>
        public static Color WarningText =>
            GetColor(EditorGUIUtility.isProSkin ? "#F4BC02" : "#333308");

        /// <summary>
        /// テキスト色（リンク）
        /// </summary>
        public static Color LinkText =>
            GetColor(EditorGUIUtility.isProSkin ? "#4C7EFF" : "#4C7EFF");





        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// HTMLからRGBカラーへの変換
        /// </summary>
        private static Color GetColor(string htmlColor) {
            if (!ColorUtility.TryParseHtmlString(htmlColor, out var color))
                throw new ArgumentException();

            return color;
        }
    }
}
#endif