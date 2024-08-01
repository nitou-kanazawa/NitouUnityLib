using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace nitou {

    /// <summary>
    /// <see cref="IEnumerable"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class EnumerableExtensions {

        /// ----------------------------------------------------------------------------
        #region 要素の判定

        // [参考]
        // _: IEnumerable.IsNullOrEmpty https://csharpvbcomparer.blogspot.com/2014/04/tips-ienumerable-isnullorempty.html

        /// <summary>
        /// Null,または空かどうか調べる拡張メソッド
        /// </summary>
        public static bool IsNullOrEmptyEnumerable<T>(this IEnumerable<T> source) {
            if (source == null) return true;
            using (var e = source.GetEnumerator()) return !e.MoveNext();
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の変換

        /// <summary>
        /// nullを除いたシーケンスに変換する拡張メソッド
        /// </summary>
        public static IEnumerable<T> WithoutNull<T>(this IEnumerable<T> source) where T : class {
            return source.Where(item => item != null);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の走査

        /// <summary>
        /// IEnumerableの各要素に対して、指定された処理を実行する拡張メソッド
        /// </summary>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action) {
            foreach (var n in source.Select((val, index) => new { val, index })) {
                action(n.val, n.index);
            }
            return source;
        }

        /// <summary>
        /// IEnumerableの各要素に対して、指定された処理を実行する拡張メソッド
        /// </summary>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (var n in source) {
                action(n);
            }
            return source;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の取得

        /// <summary>
        /// 最大値と最小値を取得する拡張メソッド
        /// </summary>
        public static (T min, T max) MinMax<T>(this IEnumerable<T> source)
            where T : IComparable<T> {
            if (source is null) throw new ArgumentNullException(nameof(source));

            using (var enumerator = source.GetEnumerator()) {
                if (!enumerator.MoveNext()) throw new InvalidOperationException("Sequence contains no elements");

                // 初期値
                T min = enumerator.Current;
                T max = enumerator.Current;

                while (enumerator.MoveNext()) {
                    if (enumerator.Current.CompareTo(max) > 0) {
                        max = enumerator.Current;
                    }
                    if (enumerator.Current.CompareTo(min) < 0) {
                        min = enumerator.Current;
                    }
                }

                return (min, max);
            }
        }

        /// <summary>
        /// 最大値と最小値を取得する拡張メソッド
        /// </summary>
        public static (TResult min, TResult max) MinMax<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
            where TResult : IComparable<TResult> {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            using (var enumerator = source.GetEnumerator()) {
                if (!enumerator.MoveNext()) throw new InvalidOperationException("Sequence contains no elements");

                // 初期値
                TResult minValue = selector(enumerator.Current);
                TResult maxValue = selector(enumerator.Current);

                while (enumerator.MoveNext()) {
                    TResult currentValue = selector(enumerator.Current);

                    if (currentValue.CompareTo(maxValue) > 0) {
                        maxValue = currentValue;
                    }
                    if (currentValue.CompareTo(minValue) < 0) {
                        minValue = currentValue;
                    }
                }

                return (minValue, maxValue);
            }
        }

        /// <summary>
        /// 最大値と最小値を取得する拡張メソッド
        /// </summary>
        public static (T min, T max) MinMax<T>(this IEnumerable<T> source, Func<T, T, bool> isGreaterThan) {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            if (isGreaterThan is null) throw new System.ArgumentNullException(nameof(isGreaterThan));

            using (var enumerator = source.GetEnumerator()) {
                if (!enumerator.MoveNext()) throw new InvalidOperationException("Sequence contains no elements");

                // 初期値
                T min = enumerator.Current;
                T max = enumerator.Current;

                while (enumerator.MoveNext()) {
                    var current = enumerator.Current;

                    if (isGreaterThan(current, max)) {
                        max = current;
                    }
                    if (!isGreaterThan(current, min)) {
                        min = current;
                    }
                }

                return (min, max);
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region  その他

        

        /// <summary>
        /// Csv 形式の文字列に変換します。
        /// </summary>
        public static string ToCsv(this IEnumerable<string> enumerable, char separator = ',') {
            if (enumerable == null) return null;

            var csv = new StringBuilder();
            enumerable.ForEach(v => {
                string val = v;
                if (v.Contains("\"") || v.Contains("\n")) {
                    if (v.Contains("\"")) {
                        // ダブルクォートがある場合はダブルクォートを２つに重ねる。(" => "")
                        val = val.Replace("\"", "\"\"");
                    }
                    // ダブルクォートまたは改行がある場合はダブルクォートで囲む。
                    val = $"\"{val}\"";
                }
                csv.AppendFormat("{0}{1}", val, separator);
            });
            return csv.ToString(0, csv.Length - 1);
        }
        #endregion
    }
}