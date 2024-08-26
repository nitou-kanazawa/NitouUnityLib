#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace nitou.EditorShared{

    /// <summary>
    /// <see cref="Resources"/>ライクに非Resourcesフォルダのアセットを読み込むためのクラス（※AssetDatabaseのラッパー）
    /// </summary>
    public class AssetsLoader{

        /// ----------------------------------------------------------------------------
        #region Public Method (単体ロード)

        /// <summary>
        /// ファイルのアセットパス(拡張子も含める)と型を設定し、Objectを読み込む．
        /// </summary>
        public static T Load<T>(AssetPath assetPath) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(assetPath.ToString());
        }

        /// <summary>
        /// ファイルのアセットパス(拡張子も含める)と型を設定し、Objectを読み込む．
        /// </summary>
        public static Object Load(AssetPath assetPath){
            return Load<Object>(assetPath);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Public Method (複数ロード)

        /// <summary>
        /// ディレクトリのパス(Assetsから)と型を設定し、Objectを読み込む．存在しない場合は空のListを返す．
        /// </summary>
        public static List<T> LoadAll<T>(AssetPath directoryPath) where T : Object {
            var assetList = new List<T>();

            if (!directoryPath.IsDirectory()) {
                Debug_.LogWarning($"The specified directory ({directoryPath}) does not exist.");
                return assetList;
            }

            // 指定したディレクトリに入っている全ファイルを取得(子ディレクトリも含む)
            // ※Directory.GetFilesはアセットパスで指定可能
            var filePaths = Directory.EnumerateFiles(directoryPath.ToAssetDatabasePath(), "*", SearchOption.AllDirectories);
            Debug_.ListLog(filePaths.ToList());

            // 取得したファイルの中からアセットだけリストに追加する
            foreach (string filePath in filePaths) {
                T asset = Load<T>(AssetPath.FromAssetPath(filePath));
                assetList.AddIfNotNull(asset);
            }
            Debug_.ListLog(assetList.Select(s => s.name).ToList());

            return assetList;
        }

        /// <summary>
        /// ディレクトリのパス(Assetsから)と型を設定し、Objectを読み込む．存在しない場合は空のListを返す．
        /// </summary>
        public static List<Object> LoadAll(AssetPath directoryPath) {
            return LoadAll<Object>(directoryPath);
        }
        #endregion
    }
}
#endif