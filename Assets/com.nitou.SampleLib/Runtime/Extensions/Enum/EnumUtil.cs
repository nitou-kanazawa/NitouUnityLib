using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// [参考]
//  kanのメモ帳: enumの汎用的な便利メソッドをまとめた便利クラス https://kan-kikuchi.hatenablog.com/entry/EnumUtility

namespace nitou {

    /// <summary>
    /// <see cref="Enum"/>型に対する汎用メソッド集
    /// </summary>
    public static class EnumUtil {

        /// --------------------------------------------------------------------
        #region 列挙型要素の取得

        /// <summary>
        /// 項目数を取得
        /// </summary>
        public static int Count<T>() where T : struct {
            return Enum.GetValues(typeof(T)).Length;
        }

        /// <summary>
        /// 最初の要素を取得する
        /// </summary>
        public static T GetFirst<T>() where T : struct {
            return (T)Enum.GetValues(typeof(T)).GetValue(0);
        }

        /// <summary>
        /// 最後の要素を取得する
        /// </summary>
        public static T GetLast<T>() where T : struct {
            var array = Enum.GetValues(typeof(T));
            return (T)array.GetValue(array.Length - 1);
        }

        /// <summary>
        /// 次の要素を取得する (※動作確認が必要！)
        /// </summary>
        public static bool TryGetNext<T>(T target, out T next) where T : struct {

            var allInList = GetAllInList<T>();
            var index = allInList.FindIndex(x => x.ToString() == target.ToString());

            // 最後の要素の場合
            if (index + 1 == allInList.Count) {
                next = NoToType<T>(0);
                return false;
            }

            next = allInList[index + 1];
            return true;
        }

        /// <summary>
        /// 項目をランダムに一つ取得
        /// </summary>
        public static T GetRandom<T>() where T : struct {
            int no = UnityEngine.Random.Range(0, Count<T>());
            return NoToType<T>(no);
        }

        /// <summary>
        /// 全ての項目が入ったListを取得
        /// </summary>
        public static List<T> GetAllInList<T>() where T : struct {
            var list = new List<T>();
            foreach (T t in Enum.GetValues(typeof(T))) {
                list.Add(t);
            }
            return list;
        }
        #endregion


        /// --------------------------------------------------------------------
        #region 列挙型要素の変換

        /// <summary>
        /// 入力された文字列と同じ項目を取得
        /// </summary>
        public static T KeyToType<T>(string targetKey) where T : struct {
            return (T)Enum.Parse(typeof(T), targetKey);
        }

        /// <summary>
        /// 入力された番号の項目を取得
        /// </summary>
        public static T NoToType<T>(int targetNo) where T : struct {
            return (T)Enum.ToObject(typeof(T), targetNo);
        }
        #endregion


        /// --------------------------------------------------------------------
        #region 列挙型要素の判定

        /// <summary>
        /// 入力された文字列の項目が含まれているか
        /// </summary>
        public static bool ContainsKey<T>(string tagetKey) where T : struct {
            foreach (T t in Enum.GetValues(typeof(T))) {
                if (t.ToString() == tagetKey) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 最初の要素かどうか
        /// </summary>
        public static bool IsFirst<T>(T target) where T : struct {
            return target.ToString() == (GetFirst<T>().ToString());
        }

        /// <summary>
        /// 最後の要素かどうか
        /// </summary>
        public static bool IsLast<T>(T target) where T : struct {
            return target.ToString() == (GetLast<T>().ToString());
        }
        #endregion


        /// --------------------------------------------------------------------
        #region その他

        /// <summary>
        /// 全ての項目に対してデリゲートを実行
        /// </summary>
        public static void ForEach<T>(Action<T> action) where T : struct {
            foreach (T t in Enum.GetValues(typeof(T))) {
                action(t);
            }
        }
        #endregion
    }




}


