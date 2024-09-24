using UnityEngine;

namespace nitou.Shared{

    /// <summary>
    /// パッケージの各種設定を管理する静的クラス
    /// </summary>
    internal static class NitouLib {

        /// <summary>
        /// パッケージのディレクトリパス
        /// </summary>
        internal static readonly PackageDirectoryPath packagePath = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static NitouLib() {
            packagePath = new PackageDirectoryPath("com.nitou.nLib");

        }
    }
}
