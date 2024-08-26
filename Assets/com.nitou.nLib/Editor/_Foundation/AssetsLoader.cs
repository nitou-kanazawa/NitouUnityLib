#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace nitou.EditorShared{

    public class AssetsLoader{

        /// ----------------------------------------------------------------------------
        // Method (単体ロード)

        /// <summary>
        /// ファイルのアセットパス(拡張子も含める)と型を設定し、Objectを読み込む．
        /// </summary>
        public static T Load<T>(AssetPath assetPath) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(assetPath.ToString());
        }


        /// ----------------------------------------------------------------------------
        // Method (複数ロード)

        /// <summary>
        /// ディレクトリのパス(Assetsから)と型を設定し、Objectを読み込む。存在しない場合は空のListを返す
        /// </summary>
        public static List<T> LoadAll<T>(AssetPath directoryPath) where T : Object {
            var assetList = new List<T>();

            if (!directoryPath.IsDirectory()) {
                Debug_.LogWarning("指定したディレクトリは存在しません．");
                return assetList;
            }

            // 指定したディレクトリに入っている全ファイルを取得(子ディレクトリも含む)
            //string[] filePathArray = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            //// 取得したファイルの中からアセットだけリストに追加する
            //foreach (string filePath in filePathArray) {
            //    T asset = Load<T>(filePath);
            //    if (asset != null) {
            //        assetList.Add(asset);
            //    }
            //}

            return assetList;
        }
    }
}
#endif