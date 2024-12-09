using System;
using System.Collections.Generic;

namespace nitou {

    /// <summary>
    /// <see cref="List{T}"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class ListExtensions {

        /// <summary>
        /// 最後の要素を取り出す拡張メソッド．要素が0なら<see cref="InvalidOperationException">例外</see>を投げる．
        /// </summary>
        public static T PopLast<T>(this IList<T> list) {
            if (list.Count == 0) {
                throw new InvalidOperationException();
            }

            var t = list[list.Count - 1];

            list.RemoveAt(list.Count - 1);

            return t;
        }


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
        
        
        /// ----------------------------------------------------------------------------
        #region 要素の削除

        #endregion

    }
}
