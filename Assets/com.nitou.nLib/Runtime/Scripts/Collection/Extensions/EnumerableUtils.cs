using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// [REF]
//  kanのメモ帳: Enumerable.Rangeを使って簡単かつスマートに連番のListを作る https://kan-kikuchi.hatenablog.com/entry/EnumerableRange
//  MathWorks: linspace https://jp.mathworks.com/help/matlab/ref/linspace.html

namespace nitou {

    /// <summary>
    /// 汎用的なシーケンスデータを生成するユーティリティクラス
    /// </summary>
    public static partial class EnumerableUtils {

        /// ----------------------------------------------------------------------------
        #region 補間 (要素数指定)

        /// <summary>
        /// 線形に配置されたデータを作成する．
        /// </summary>
        public static IEnumerable<float> Linspace(float start, float end, int num = 10) {
            if (num <= 0) throw new InvalidOperationException("Number of points must be positive and non-zero.");
            if (num == 1) return new List<float> { start };

            float step = (end - start) / (num - 1);
            return Enumerable
                .Range(0, num)
                .Select(i => start + i * step);
        }

        /// <summary>
        /// 線形に配置されたデータを作成する．
        /// </summary>
        public static IEnumerable<Vector2> Linspace(Vector2 start, Vector2 end, int num = 10) {
            if (num <= 0) throw new InvalidOperationException("Number of points must be positive and non-zero.");
            if (num == 1) return new List<Vector2> { start };

            Vector2 step = (end - start) / (num - 1);
            return Enumerable
                .Range(0, num)
                .Select(i => start + i * step);
        }

        /// <summary>
        /// 線形に配置されたデータを作成する．
        /// </summary>
        public static IEnumerable<Vector3> Linspace(Vector3 start, Vector3 end, int num = 10) {
            if (num <= 0) throw new InvalidOperationException("Number of points must be positive and non-zero.");
            if (num == 1) return new List<Vector3> { start };

            Vector3 step = (end - start) / (num - 1);
            return Enumerable
                .Range(0, num)
                .Select(i => start + i * step);
        }

        /// <summary>
        /// 対数的に配置されたデータを作成する．
        /// </summary>
        public static IEnumerable<double> Logspace(double startExp, double endExp, int num) {
            if (num <= 0) throw new InvalidOperationException("Number of points must be positive and non-zero.");
            if (num == 1) return new List<double> { startExp };

            return Enumerable
                .Range(0, num)
                .Select(i => Math.Pow(10, startExp + i * (endExp - startExp) / (num - 1)));
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 補間 (ステップ指定)

        /// <summary>
        /// ステップサイズで線形に配置されたデータを作成する．
        /// </summary>
        public static IEnumerable<float> LinspaceWithStep(float start, float end, float step) {
            if (step <= 0) throw new InvalidOperationException("Step must be positive and non-zero.");

            int count = (int)((end - start) / step) + 1;
            return Enumerable
                .Range(0, count)
                .Select(i => start + i * step)
                .TakeWhile(value => value <= end);
        }
        #endregion


        /// ----------------------------------------------------------------------------

        /// <summary>
        /// start〜end(含む)の連番を順に含んだListを作成し取得する．
        /// </summary>
        public static List<int> RangeNumbers(int start, int end) {
            if (start == end) {
                return new List<int>() { start };
            }
            if (start > end) {
                return Enumerable.Range(end, start - end + 1).Reverse().ToList();
            }
            return Enumerable.Range(start, end - start + 1).ToList();
        }
    }
}
