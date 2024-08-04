#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Alchemy.Inspector;

// [参考]
//  Zenn: Vector3やEnumをSceneView上で編集できるようにする https://zenn.dev/kd_gamegikenblg/articles/30b2b1139b213c
//  qiita: Unity Editorの拡張 Vector3の座標をシーンから変更できるようにする https://qiita.com/RYA234/items/13d98a49e291ee2028d7

namespace nitou.LevelObjects.EditorScripts{

    [CustomEditor(typeof(WayPoints))]
    public class WayPointsEditor : Editor{

        // 操作対象
        private static WayPoints _instance = null;


        /// ----------------------------------------------------------------------------
        // Editor Method

        private void OnEnable() {
            _instance = target as WayPoints;
        }

        private void OnSceneGUI() {

            // Handle
            foreach(var wayPoint in _instance.Points) {
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(wayPoint.position, Quaternion.identity);

                // 変更の反映
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(_instance, "Move WayPoint");
                    wayPoint.position = newPosition;
                }
            }

            // Others
            for(int i=0; i< _instance.Points.Count; i++) {
                var wayPoint = _instance.Points[i];

                // Lavel
                var style = new GUIStyle();
                style.normal.textColor = wayPoint.GetColor();
                style.fontSize = 14;
                Handles.Label(wayPoint.position, $"\n\n{i}: {wayPoint.tag}", style);

                // 
                Handles.BeginGUI();

                //var screenPos = HandleUtility.WorldToGUIPointWithDepth(wayPoint.position);
                var screenPos = new Vector2();
                EditorGUI.BeginChangeCheck();
                var rect = new Rect(screenPos.x, screenPos.y + 10, 100, 20);
                var editedTag = (WayPoint.TagTypes)EditorGUI.EnumPopup(rect, wayPoint.tag);
                
                // 変更されたら反映する
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(_instance, "Edit Destination");
                    wayPoint.tag = editedTag;
                    //EditorUtility.SetDirty(_instance);
                }

                Handles.EndGUI();
            }

        }


        /// ----------------------------------------------------------------------------
        // Private Method

    }
}
#endif