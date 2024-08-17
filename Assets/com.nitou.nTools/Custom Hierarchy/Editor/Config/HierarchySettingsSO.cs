#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace nitou.Tools.Hierarchy{

    /// <summary>
    /// Editorで参照するプロジェクト固有の設定データ
    /// </summary>
    [FilePath(
        "ProjectSettings/HierarchySettingsSO.asset",
        FilePathAttribute.Location.ProjectFolder
    )]
    public class HierarchySettingsSO : ScriptableSingleton<HierarchySettingsSO> {

        public int test;

        public void Save() => Save(true);
    }
}
#endif