#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace nitou.Tools.CodeGeneration{

    [CustomEditor(typeof(CodeTemplateAsset))]
    public class CodeTemplateImporterEditor : Editor{


        public override void OnInspectorGUI() {
            // インポートされたアセットのインスペクタを表示
            var asset = (CodeTemplateAsset)target;

            EditorGUILayout.LabelField("File content:");
            EditorGUILayout.TextArea(asset.data);


            // Apply changes
            //ApplyRevertGUI();
        }
    }
}

#endif