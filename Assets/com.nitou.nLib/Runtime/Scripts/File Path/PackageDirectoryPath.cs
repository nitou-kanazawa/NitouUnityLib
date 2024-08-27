using System.IO;
using UnityEngine;

namespace nitou {

    [System.Serializable]
    public sealed class PackageDirectoryPath : IUnityProjectPath{

        public enum Mode {
            Upm,
            Normal,
            NotExist,
        }

        // Package配布後の相対パス ("Packages/...")
        private readonly string _upmRelativePath;

        // 開発プロジェクトでの相対パス ("Assets/...")
        private readonly string _normalRelativePath;

        private Mode _mode;


        public string UpmPath => $"Packages/{_upmRelativePath}";
        public string NormalPath => $"Assets/{_normalRelativePath}";


        /// ----------------------------------------------------------------------------
        // Pubic Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PackageDirectoryPath(string upmRelativePath = "com.nitou.nLib", string normalRelativePath = "nLib") {
            _upmRelativePath = upmRelativePath;
            _normalRelativePath = normalRelativePath;

            // 判定する
            _mode = CheckDirectoryLocation();
        }
            
        public string ToProjectPath() {
            throw new System.NotImplementedException();
        }

        public string ToAbsolutePath() {
            throw new System.NotImplementedException();
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// ディレクトリの位置を判定する
        /// </summary>
        private Mode CheckDirectoryLocation() {

            if (Directory.Exists(UpmPath)) return Mode.Upm;
            if (Directory.Exists(NormalPath)) return Mode.Normal;

            Debug.LogError($"Directory not found in both UPM and normal paths: \n" +
                    $"  [{UpmPath}] and \n" +
                    $"  [{NormalPath}]");
            return Mode.NotExist;
        }
    }
}
