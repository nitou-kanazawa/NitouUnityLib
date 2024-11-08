using System.Collections.Generic;

// [参考]
//  qiita: パスワードのようなランダムな文字列を生成して返す関数 https://baba-s.hatenablog.com/entry/2015/07/07/000000

namespace nitou {

    public static class StringUtil {

        /// ----------------------------------------------------------------------------
        #region 文字列への変換

        public static string ToFloatText(this float self) {
            return self.ToString("0.00");
        }

        public static string ToFloatText(this float self, int decimalPlaces = 2) {
            // 小数点以下の桁数に基づいてフォーマット
            string format = "0." + new string('0', decimalPlaces);
            return self.ToString(format);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 文字列の生成

        /// <summary>
        /// 簡易的なパスワードを生成する
        /// </summary>
        public static string GeneratePassword(int length) {
            var sb = new System.Text.StringBuilder(length);
            var r = new System.Random();

            for (int i = 0; i < length; i++) {
                int pos = r.Next(PASSWORD_CHARS.Length);
                char c = PASSWORD_CHARS[pos];
                sb.Append(c);
            }

            return sb.ToString();
        }
        private const string PASSWORD_CHARS = "0123456789abcdefghijklmnopqrstuvwxyz";

        #endregion

    }


    public static class ParseUtil {

        /// ----------------------------------------------------------------------------
        #region 文字列の生成

        public static float FloatOrZero(string text) {
            return float.TryParse(text, out var result) ? result : 0f;
        }

        public static float FloatOrDefault(string text, float value) {
            return float.TryParse(text, out var result) ? result : value;
        }
        #endregion

    }

}