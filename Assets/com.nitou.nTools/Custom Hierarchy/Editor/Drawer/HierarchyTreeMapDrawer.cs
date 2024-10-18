#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace nitou.Tools.Hierarchy.EditorSctipts {
    using nitou.Tools.Shared;
    using nitou.EditorShared;

    public class HierarchyTreeMapDrawer : HierarchyDrawer {

        private static readonly Dictionary<string, Texture2D> TextureCached = new();

        // íËêî
        private readonly string CURRENT_TEXTURE = "tree_map_current.png";
        private readonly string LAST_TEXTURE = "tree_map_last.png";
        private readonly string LEVEL_TEXTURE = "tree_map_level.png";
        private readonly string LINE_TEXTURE = "tree_map_line.png";


        public static Texture2D TreeMapCurrent {
            get {
                TextureCached.TryGetValue(nameof(TreeMapCurrent), out var tex);
                if (tex != null) return tex;

                // Textureì«Ç›çûÇ›
                var relativePath = "Custom Hierarchy/Editor/Textures";
                var assetName = "tree_map_current.png";
                var fullPath = PathUtil.Combine(PackageInfo.packagePath.ToAbsolutePath(),relativePath, assetName);
                Debug_.Log(fullPath, Colors.Orange);
                Debug_.Log(AssetPath.FromAbsolutePath(fullPath), Colors.Red);
                tex = AssetsLoader.Load<Texture2D>(AssetPath.FromAbsolutePath(fullPath));

                //tex = NonResources.Load<Texture2D>("tree_map_current.png", "Custom Hierarchy/Editor/Textures", NitouTools.pacakageInfo);
                TextureCached[nameof(TreeMapCurrent)] = tex;
                return tex;
            }
        }

        public static Texture2D TreeMapLast {
            get {
                TextureCached.TryGetValue(nameof(TreeMapLast), out var tex);
                if (tex != null) return tex;

                // Textureì«Ç›çûÇ›
                var relativePath = "Custom Hierarchy/Editor/Textures";
                var assetName = "tree_map_last.png";
                var fullPath = PathUtil.Combine(PackageInfo.packagePath.ToAbsolutePath(), relativePath, assetName);
                Debug_.Log(fullPath, Colors.Orange);
                Debug_.Log(AssetPath.FromAbsolutePath(fullPath), Colors.Red);
                tex = AssetsLoader.Load<Texture2D>(AssetPath.FromAbsolutePath(fullPath));

                //tex = NonResources.Load<Texture2D>("tree_map_last.png", "Custom Hierarchy/Editor/Textures", NitouTools.pacakageInfo);
                TextureCached[nameof(TreeMapLast)] = tex;
                return tex;
            }
        }

        public static Texture2D TreeMapLevel {
            get {
                TextureCached.TryGetValue(nameof(TreeMapLevel), out var tex);
                if (tex != null) return tex;

                // Textureì«Ç›çûÇ›
                var relativePath = "Custom Hierarchy/Editor/Textures";
                var assetName = "tree_map_level.png";
                var fullPath = PathUtil.Combine(PackageInfo.packagePath.ToAbsolutePath(), relativePath, assetName);
                Debug_.Log(fullPath, Colors.Orange);
                Debug_.Log(AssetPath.FromAbsolutePath(fullPath), Colors.Red);
                tex = AssetsLoader.Load<Texture2D>(AssetPath.FromAbsolutePath(fullPath));

                //tex = NonResources.Load<Texture2D>("tree_map_level.png", "Custom Hierarchy/Editor/Textures", NitouTools.pacakageInfo);
                TextureCached[nameof(TreeMapLevel)] = tex;
                return tex;
            }
        }

        public static Texture2D TreeMapLine {
            get {
                TextureCached.TryGetValue(nameof(TreeMapLine), out var tex);
                if (tex != null) return tex;

                // Textureì«Ç›çûÇ›
                var relativePath = "Custom Hierarchy/Editor/Textures";
                var assetName = "tree_map_line.png";
                var fullPath = PathUtil.Combine(PackageInfo.packagePath.ToAbsolutePath(), relativePath, assetName);
                Debug_.Log(fullPath, Colors.Orange);
                Debug_.Log(AssetPath.FromAbsolutePath(fullPath), Colors.Red);
                tex = AssetsLoader.Load<Texture2D>(AssetPath.FromAbsolutePath(fullPath));

                //tex = NonResources.Load<Texture2D>("tree_map_line.png", "Custom Hierarchy/Editor/Textures", NitouTools.pacakageInfo);
                TextureCached[nameof(TreeMapLine)] = tex;
                return tex;
            }
        }


        public override void OnGUI(int instanceID, Rect selectionRect) {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) return;

            var settings = nToolsSettings.GetOrCreateSettings();

            var tempColor = GUI.color;

            if (settings.ShowTreeMap) {
                selectionRect.width = 14;
                selectionRect.height = 16;

                int childCount = gameObject.transform.childCount;
                int level = Mathf.RoundToInt(selectionRect.x / 14f);
                var t = gameObject.transform;
                Transform parent = null;

                for (int i = 0, j = level - 1; j >= 0; i++, j--) {
                    selectionRect.x = 14 * j;
                    if (i == 0) {
                        if (childCount == 0) {
                            GUI.color = settings.TreeMapColor;
                            GUI.DrawTexture(selectionRect, TreeMapLine);
                        }

                        t = gameObject.transform;
                    } else if (i == 1) {
                        GUI.color = settings.TreeMapColor;
                        if (parent == null) {
                            if (t.GetSiblingIndex() == gameObject.scene.rootCount - 1) {
                                GUI.DrawTexture(selectionRect, TreeMapLast);
                            } else {
                                GUI.DrawTexture(selectionRect, TreeMapCurrent);
                            }
                        } else if (t.GetSiblingIndex() == parent.childCount - 1) {
                            GUI.DrawTexture(selectionRect, TreeMapLast);
                        } else {
                            GUI.DrawTexture(selectionRect, TreeMapCurrent);
                        }

                        t = parent;
                    } else {
                        if (parent == null) {
                            if (t.GetSiblingIndex() != gameObject.scene.rootCount - 1) GUI.DrawTexture(selectionRect, TreeMapLevel);
                        } else if (t.GetSiblingIndex() != parent.childCount - 1) GUI.DrawTexture(selectionRect, TreeMapLevel);

                        t = parent;
                    }

                    if (t != null) parent = t.parent;
                    else break;
                }

                GUI.color = tempColor;
            }
        }
    }
}
#endif