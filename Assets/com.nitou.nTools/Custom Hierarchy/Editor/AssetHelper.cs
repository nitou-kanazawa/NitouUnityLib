#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace nitou.EditorShared {

    public struct PackageFolderInfo {
        public readonly string upmFolderName;
        public readonly string normalFolderName;
        
        public PackageFolderInfo(string upmFolderName = "com.nitou.nLib", string normalFolderName = "nLib") {
            this.upmFolderName = upmFolderName;
            this.normalFolderName = normalFolderName;
        }
    }

    public static class CustomPackageUtil {


        /// <summary>
        /// 
        /// </summary>
        public static T FindAssetWithPath<T>(string assetName, string relativePath, PackageFolderInfo packageInfo)
            where T : Object {

            string path = AssetInPackagePath(relativePath, assetName, packageInfo);
            var t = AssetDatabase.LoadAssetAtPath(path, typeof(T));
            if (t == null) Debug.LogError($"Couldn't load the {nameof(T)} at path :{path}");
            return t as T;
        }


        /// ----------------------------------------------------------------------------
        // Private Method
        
        /// <summary>
        /// 
        /// </summary>
        private static string AssetInPackagePath(string relativePath, string assetName, PackageFolderInfo packageInfo) {
            return GetPathInCurrentEnvironent($"{relativePath}/{assetName}", packageInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetPathInCurrentEnvironent(string fullRelativePath, PackageFolderInfo packageInfo) {
            // 配布先でのパス
            var upmPath = $"Packages/{packageInfo.upmFolderName}/{fullRelativePath}";
            // 開発プロジェクト内でのパス
            var normalPath = $"Assets/{packageInfo.normalFolderName}/{fullRelativePath}";

            return !File.Exists(Path.GetFullPath(upmPath)) ? normalPath : upmPath;

            //if (File.Exists(Path.GetFullPath(upmPath))) {
            //    return upmPath;
            //} else if (File.Exists(Path.GetFullPath(normalPath))) {
            //    return normalPath;
            //} else {
            //    Debug.LogError($"File not found in both UPM and normal paths: {upmPath} and {normalPath}");
            //    return null;
            //}
        }
    }
}
#endif