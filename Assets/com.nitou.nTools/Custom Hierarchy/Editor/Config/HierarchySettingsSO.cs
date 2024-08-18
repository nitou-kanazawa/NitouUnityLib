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

        [SerializeField] HierarchyObjectMode hierarchyObjectMode = HierarchyObjectMode.RemoveInBuild;
        [SerializeField] bool showHierarchyToggles;
        [SerializeField] bool showComponentIcons;
        [SerializeField] bool showTreeMap;
        [SerializeField] Color treeMapColor = new(0.53f, 0.53f, 0.53f, 0.45f);
        [SerializeField] bool showSeparator;
        [SerializeField] bool showRowShading;
        [SerializeField] Color separatorColor = new(0.19f, 0.19f, 0.19f, 0f);
        [SerializeField] Color evenRowColor = new(0f, 0f, 0f, 0.07f);
        [SerializeField] Color oddRowColor = Color.clear;




        public void Save() => Save(true);
    }
}
#endif