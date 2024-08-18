#if UNITY_EDITOR
using UnityEngine;

namespace nitou.Tools.Shared{
    using nitou.EditorShared;

    internal static class NitouTools{

        internal static readonly PackageFolderInfo pacakageInfo;


        static NitouTools() {
            pacakageInfo = new PackageFolderInfo(
                upmFolderName: "com.nitou.nTools",
                normalFolderName: "com.nitou.nTools");
        }

    }
}
#endif