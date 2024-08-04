#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

// [参考]
//  Docswell: AssetPostprocessor完全に理解した https://www.docswell.com/s/henjiganai/5714J5-AssetPostprocessor#p8
//  qiita: エディター拡張で、読み込むアセットのパスをハードコードしないために https://qiita.com/tsukimi_neko/items/3d57e3808acb88e11c39
//  　→ （※AssetPostprocessorはUnity.Object？を親に持たないため，シリアライズ対象外みたい）

namespace nitou.Tools.ProjectWindow {

    /// <summary>
    /// フォルダアイコン画像を管理するDictionayを生成する
    /// </summary>
    internal class IconDictionaryCreator : AssetPostprocessor {

        // リソース情報
        private const string AssetsPath = "com.nitou.nTools/Project Folder/Project Folder Icon/Icons";
        private const string PackagePath = "Packages/" + AssetsPath;
        internal static Dictionary<string, Texture> IconDictionary;


        /// ----------------------------------------------------------------------------
        // Internal Method


        /// <summary>
        /// Dictionaryの生成
        /// </summary>
        internal static void BuildDictionary() {

            // [NOTE]
            // ※開発時はAssets直下，配布後はPackages直下に存在するパッケージが存在する
            //string iconFolderPath = DetermineIconPath();
            //if (string.IsNullOrEmpty(iconFolderPath)) {
            //    Debug.LogError("Icon folder not found.");
            //    return;
            //}


            var dictionary = new Dictionary<string, Texture>();

            //FileInfo[] info = new DirectoryInfo(iconFolderPath).GetFiles("*.png");
            //foreach (FileInfo f in info) {
            //    var texture = (Texture)AssetDatabase.LoadAssetAtPath(GetRelativePath(iconFolderPath, f.FullName), typeof(Texture2D));
            //    dictionary.Add(Path.GetFileNameWithoutExtension(f.Name), texture);
            //}
            //IconDictionary = dictionary;


            var dir = new DirectoryInfo(Application.dataPath + "/" + AssetsPath);
            FileInfo[] info = dir.GetFiles("*.png");
            foreach (FileInfo f in info) {
                var texture = (Texture)AssetDatabase.LoadAssetAtPath($"Assets/{AssetsPath}/{f.Name}", typeof(Texture2D));
                dictionary.Add(Path.GetFileNameWithoutExtension(f.Name), texture);
            }

            IconDictionary = dictionary;
        }

        /*

        /// <summary>
        /// フルパスから相対パスを取得する
        /// </summary>
        private static string GetRelativePath(string rootPath, string fullPath) {
            if (fullPath.StartsWith(rootPath)) {
                return "Assets" + fullPath.Substring(Application.dataPath.Length);
            }
            return fullPath;
        }

        /// <summary>
        /// アイコンのパスを決定する
        /// </summary>
        private static string DetermineIconPath() {
            // Assetsフォルダ内のパスを確認
            string assetsFullPath = Path.Combine(Application.dataPath, AssetsPath);
            if (Directory.Exists(assetsFullPath)) {
                return assetsFullPath;
            }

            // Packagesフォルダ内のパスを確認
            string packageFullPath = Path.Combine(Application.dataPath.Replace("Assets", ""), PackagePath);
            if (Directory.Exists(packageFullPath)) {
                return packageFullPath;
            }

            return string.Empty;
        }

        */



        /// <summary>
        /// 指定したキーに対応するアイコン画像を取得する
        /// </summary>
        public static (bool isExist, Texture texture) GetIconTexture(string fileNameKey) {

            // ファイル名が完全一致の場合
            if (IconDictionary.ContainsKey(fileNameKey)) {
                return (true, IconDictionary[fileNameKey]);
            }

            // 正規表現対応 (※とりあえず決め打ちの実装)
            if (fileNameKey[0] == '_' && fileNameKey.Length > 1) {
                fileNameKey = fileNameKey.Substring(1);
                if (IconDictionary.ContainsKey(fileNameKey))
                    return (true, IconDictionary[fileNameKey]);
            }

            return (false, null);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// アセットをインポートした後の処理
        /// </summary>
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
            if (!ContainsIconAsset(importedAssets) &&
                !ContainsIconAsset(deletedAssets) &&
                !ContainsIconAsset(movedAssets) &&
                !ContainsIconAsset(movedFromAssetPaths)) {
                return;
            }

            BuildDictionary();
        }

        /// <summary>
        /// ファイル名の検証
        /// </summary>
        private static bool ContainsIconAsset(string[] assets) {
            foreach (string str in assets) {
                if (ReplaceSeparatorChar(Path.GetDirectoryName(str)) == "Assets/" + AssetsPath) {
                    return true;
                }
            }
            return false;
        }

        private static string ReplaceSeparatorChar(string path) {
            return path.Replace("\\", "/");
        }
    }
}
#endif