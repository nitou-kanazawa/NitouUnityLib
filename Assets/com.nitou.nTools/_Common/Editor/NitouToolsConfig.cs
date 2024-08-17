#if UNITY_EDITOR
using UnityEngine;

namespace nitou.Tools.Shared{
    using nitou.EditorShared;

    internal static class NitouToolsConfig{

        internal static readonly PackageFolderInfo pacakageInfo;


        static NitouToolsConfig() {
            pacakageInfo = new PackageFolderInfo(
                upmFolderName: "com.nitou.nTools",
                normalFolderName: "com.nitou.nTools");
        }

    }
}
#endif