#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;

namespace nitou.Tools.Hierarchy.EditorSctipts {

    internal static class HierarchyDrawerInitializer {

        [InitializeOnLoadMethod]
        private static void Init() {
            
            var drawers = TypeCache.GetTypesDerivedFrom<HierarchyDrawer>()
                .Where(x => !x.IsAbstract)
                .Select(x => (HierarchyDrawer)Activator.CreateInstance(x));

            foreach (var drawer in drawers) {
                EditorApplication.hierarchyWindowItemOnGUI += drawer.OnGUI;
            }
        }
    }
}
#endif