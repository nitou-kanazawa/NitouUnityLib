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
