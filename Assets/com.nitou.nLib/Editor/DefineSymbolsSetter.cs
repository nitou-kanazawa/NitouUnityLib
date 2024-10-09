#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace nitou.EditorScripts {

    /// <summary>
    /// 
    /// </summary>
    [InitializeOnLoad]
    internal static class DefineSymbolsSetter{

        // 定義したいシンボル
        private const string SYMBOL_TO_DEFINE = "NLIB_PACKAGE_SYMBOL";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        static DefineSymbolsSetter() {
            // パッケージがインストールされたときに実行される処理
            AddDefineSymbolsIfNeeded();
        }

        /// <summary>
        /// シンボルを定義する
        /// </summary>
        private static void AddDefineSymbolsIfNeeded() {

            // 現在のビルドターゲットごとの定義済みのシンボルを取得
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            // シンボルがすでに定義されていないかチェック
            if (!defines.Contains(SYMBOL_TO_DEFINE)) {
                // シンボルが未定義なら追加
                defines += ";" + SYMBOL_TO_DEFINE;

                // シンボルを設定
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, defines);

                // デバッグ用ログ
                Debug.Log($"Added define symbol: {SYMBOL_TO_DEFINE}");
            } else {
                //Debug.Log($"{SYMBOL_TO_DEFINE} is already defined.");
            }
        }
    }
}
#endif
