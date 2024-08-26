using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// [参考]
//  コガネブログ: 区切り文字にスラッシュを使用して指定したディレクトリ内のファイル名を返す関数 https://baba-s.hatenablog.com/entry/2015/07/29/100000

namespace nitou {

    /// <summary>
    /// ディレクトリ操作に関する汎用メソッド集
    /// </summary>
    public static class DirectoryUtil{

        /// <summary>
        /// <para>指定したディレクトリ内のファイルの名前 (パスを含む) を返します</para>
        /// <para>パスの区切り文字は「\\」ではなく「/」です</para>
        /// </summary>
        public static string[] GetFiles(string path) {
            return Directory
                .GetFiles(path)
                .Select(c => c.Replace("\\", "/"))
                .ToArray();
        }

        /// <summary>
        /// <para>指定したディレクトリ内の指定した検索パターンに一致するファイル名 (パスを含む) を返します</para>
        /// <para>パスの区切り文字は「\\」ではなく「/」です</para>
        /// </summary>
        public static string[] GetFiles(string path, string searchPattern) {
            return Directory
                .GetFiles(path, searchPattern)
                .Select(c => c.Replace("\\", "/"))
                .ToArray();
        }

        /// <summary>
        /// <para>指定したディレクトリの中から、指定した検索パターンに一致し、</para>
        /// <para>サブディレクトリを検索するかどうかを決定する値を持つファイル名 (パスを含む) を返します</para>
        /// <para>パスの区切り文字は「\\」ではなく「/」です</para>
        /// </summary>
        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption) {
            return Directory
                .GetFiles(path, searchPattern, searchOption)
                .Select(c => c.Replace("\\", "/"))
                .ToArray();
        }

    }
}
