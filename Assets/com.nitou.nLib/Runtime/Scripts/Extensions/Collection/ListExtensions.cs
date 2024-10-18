using System.Collections.Generic;

namespace nitou {

    /// <summary>
    /// <see cref="List{T}"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class ListExtensions {

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
    }
}
