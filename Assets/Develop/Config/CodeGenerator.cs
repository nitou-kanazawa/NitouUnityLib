#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;

namespace nitou.Tools.CodeGeneration{

    /// <summary>
    /// コード生成関連のメソッドを提供するクラス
    /// </summary>
    public static class CodeGenerator{

        public struct ClassInfo {
            public string className;
            public string namespaceName;
        }


        /// <summary>
        /// コードを生成し、指定されたディレクトリにファイルとして保存します。
        /// </summary>
        /// <param name="templatePath">テンプレートファイルのパス</param>
        /// <param name="outputDirectory">生成されたファイルの保存先ディレクトリ</param>
        /// <param name="className">生成されるクラスの名前</param>
        /// <param name="filePath">ファイル名のベース（拡張子なし）</param>
        /// <param name="namespaceName">生成されるクラスの名前空間</param>
        public static void GenerateClass(string templatePath, string outputDirectory,string filePath, ClassInfo classInfo) {
            // テンプレートファイルの読み込み
            if (!File.Exists(templatePath)) {
                Debug.LogError($"テンプレートファイルが見つかりません: {templatePath}");
                return;
            }

            string templateContent = File.ReadAllText(templatePath);

            // テンプレート内の置換
            templateContent = templateContent.Replace("#CLASSNAME#", classInfo.className);
            templateContent = templateContent.Replace("#FILEPATH#", filePath);
            templateContent = templateContent.Replace("#NAMESPACE#", classInfo.namespaceName);

            // 出力ディレクトリが存在しない場合は作成
            if (!Directory.Exists(outputDirectory)) {
                Directory.CreateDirectory(outputDirectory);
            }

            // ファイルのパスを決定して書き込み
            string fileName = Path.Combine(outputDirectory, $"{classInfo.className}.cs");
            File.WriteAllText(fileName, templateContent);

            // AssetDatabaseをリフレッシュしてUnityにファイルを認識させる
            AssetDatabase.Refresh();

            Debug.Log($"クラス生成完了: {fileName}");
        }

    }
}
#endif