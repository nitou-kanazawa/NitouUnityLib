
namespace nitou.Tools.Shared{

    /// <summary>
    /// パッケージの各種設定を管理する静的クラス
    /// </summary>
    internal static class PackageInfo{

        /// <summary>
        /// パッケージのディレクトリパス
        /// </summary>
        internal static readonly PackageDirectoryPath packagePath = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static PackageInfo() {
            //pacakageInfo = new nitou.EditorShared.PackageFolderInfo(
            //    upmFolderName: "com.nitou.nTools",
            //    normalFolderName: "com.nitou.nTools");


            packagePath = new PackageDirectoryPath("com.nitou.nTools");
        }
    }
}
