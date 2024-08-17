#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace nitou.Tools.Hierarchy {

    internal static class HierarchyObjectCreationMenu {

        [MenuItem(
            GameObjectMenu.Prefix.DammyObject + "Header", 
            priority = GameObjectMenu.Order.DammyObject
        )]
        static void CreateHeader(MenuCommand menuCommand) {

            var obj = new GameObject("Header");
            obj.AddComponent<HierarchyHeader>();
            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
            Selection.activeObject = obj;
        }

        [MenuItem(
            GameObjectMenu.Prefix.DammyObject + "Separator",
            priority = GameObjectMenu.Order.DammyObject
        )]
        static void CreateSeparator(MenuCommand menuCommand) {

            var obj = new GameObject("Separator");
            obj.AddComponent<HierarchySeparator>();
            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
            Selection.activeObject = obj;
        }
    }
}
#endif