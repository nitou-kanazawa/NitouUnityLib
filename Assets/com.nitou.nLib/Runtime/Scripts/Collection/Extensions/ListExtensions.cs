using System;
using System.Collections.Generic;

namespace nitou {

    /// <summary>
    /// <see cref="List{T}"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class ListExtensions {

        /// ----------------------------------------------------------------------------
        #region 要素の削除

        /// <summary>
        /// 指定したインデックスがリスト範囲内か確認する
        /// </summary>
        public static bool IsInRange<T>(this int index, IReadOnlyList<T> list) {
            return 0 <= index && index < list.Count;
        }

        /// <summary>
        /// 指定したインデックスがリスト範囲外か確認する
        /// </summary>
        public static bool IsOutOfRange<T>(this int index, IReadOnlyList<T> list) {
            return !index.IsInRange(list);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の削除

        /// <summary>
        /// 該当する要素を全て削除する拡張メソッド．
        /// </summary>
        public static void RemoveAll<T>(this IList<T> self, Func<T, bool> predicate) {
            for (int i = self.Count - 1; i >= 0; i--) {
                if (predicate(self[i])) {
                    self.RemoveAt(i);
                }
            }
        }
        #endregion

    }
}
