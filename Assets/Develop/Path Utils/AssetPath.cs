using System;
using System.IO;
using UnityEngine;

// [参考]
//  _:絶対パスと Assets パスの変換メソッド https://www.create-forever.games/unity-absolute-path-assets-path/#google_vignette

namespace nitou {

    // [基本機能]
    // アセットパスの保持: アセットパスを表す文字列を保持する。
    // パスの変換: AssetDatabase用のアセットパスと、System.IO用のファイルシステムパスとの相互変換が簡単に行える。
    // パス操作: パスの結合、相対パスと絶対パスの変換、拡張子の変更や削除など、基本的なパス操作が可能。
    // パスの検証: アセットパスが正しいフォーマットであるか、存在するかを検証できる。

    public sealed class AssetPath {

        // "Assets/以下の相対パス"
        private readonly string _relativePath;


        // 非公開のコンストラクタ
        private AssetPath(string relativePath) {
            _relativePath = relativePath.Replace("\\", "/");
        }

        /// --------------------------------------------------------------------
        #region Factory Methods

        public static AssetPath Empty() {
            return new AssetPath("");
        }

        /// <summary> 
        /// "Assets/"以下の相対パスを指定して生成する
        /// </summary>
        public static AssetPath FromRelativePath(string relativePath) {
            if (relativePath.StartsWith("Assets/")) {
                relativePath = relativePath.Substring("Assets/".Length);
            }
            return new AssetPath(relativePath);
        }

        /// <summary>
        /// 任意のアセットから生成する
        /// </summary>
        public static AssetPath FromAsset(UnityEngine.Object asset) {
            if (asset == null)
                throw new ArgumentNullException(nameof(asset));

            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrEmpty(assetPath))
                throw new ArgumentException("The provided asset is not a valid asset in the Assets folder.");

            return FromRelativePath(assetPath);
        }

        /// <summary>
        /// 絶対パスから生成する
        /// </summary>
        public static AssetPath FromAbsolutePath(string absolutePath) {
            string relativePath = absolutePath.Replace(Application.dataPath, "").TrimStart('\\', '/');
            return new AssetPath(relativePath);
        }
        #endregion


        /// --------------------------------------------------------------------
        #region Convert to string

        /// <summary>
        /// 相対パス
        /// </summary>
        public string ToRelativePath() => _relativePath;

        /// <summary>
        /// アセットパス
        /// </summary>
        public string ToAssetDatabasePath() => "Assets/" + _relativePath;

        /// <summary>
        /// 絶対パス
        /// </summary>
        public string ToAbsolutePath() => Path.GetFullPath(Path.Combine(Application.dataPath, _relativePath));
        
        public override string ToString() => this.ToAssetDatabasePath();
        #endregion



        public AssetPath ChangeExtension(string newExtension) {
            return new AssetPath(Path.ChangeExtension(_relativePath, newExtension));
        }

        public AssetPath RemoveExtension() {
            return ChangeExtension(null);
        }

        public AssetPath Combine(string additionalPath) {
            return new AssetPath(Path.Combine(_relativePath, additionalPath).Replace("\\", "/"));
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValidAssetPath() {
            return !string.IsNullOrEmpty(_relativePath);
        }

        /// <summary>
        /// ファイルが存在するか確認する
        /// </summary>
        public bool Exists() {
            return File.Exists(this.ToAbsolutePath());
        }

        public override bool Equals(object obj) {
            if (obj is AssetPath other) {
                return string.Equals(_relativePath, other._relativePath, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode() => _relativePath.GetHashCode(StringComparison.OrdinalIgnoreCase);

        public static bool operator ==(AssetPath left, AssetPath right) => Equals(left, right);
        public static bool operator !=(AssetPath left, AssetPath right) => !Equals(left, right);
    }
}
