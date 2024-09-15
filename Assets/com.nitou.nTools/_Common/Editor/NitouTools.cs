#if UNITY_EDITOR
using UnityEngine;

namespace nitou.Tools.Shared{
    using nitou.EditorShared;

    internal static class NitouTools{

        // old
        internal static readonly PackageFolderInfo pacakageInfo;

        // new
        internal static readonly PackageDirectoryPath pacakagePath = null;


        static NitouTools() {
            pacakageInfo = new PackageFolderInfo(
                upmFolderName: "com.nitou.nTools",
                normalFolderName: "com.nitou.nTools");


            pacakagePath = new PackageDirectoryPath("com.nitou.nTools");
        }

    }
}
#endif