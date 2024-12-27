#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

// [REF]
//  はなちる: Player SettingsのScriptingDefineSymbolsをスクリプトから取得・設定する方法 https://www.hanachiru-blog.com/entry/2024/06/03/120000

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

            var buildTarget = NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            // パッケージがインストールされたときに実行される処理
            AddDefineSymbolsIfNeeded();

            AddDefineSymbolsIfNeeded(buildTarget, "USN_USE_ASYNC_METHODS");
            //AddDefineSymbolsIfNeeded(buildTarget, "UNITASK_DOTWEEN_SUPPORT");
        }


        /// ----------------------------------------------------------------------------

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

        /// <summary>
        /// シンボルを定義する
        /// </summary>
        private static void AddDefineSymbolsIfNeeded(NamedBuildTarget buildTarget, string symbol) {

            // ビルドターゲットの定義済みのシンボルを取得
            string defines = PlayerSettings.GetScriptingDefineSymbols(buildTarget);

            // シンボルがすでに定義されていないかチェック
            if (!defines.Contains(symbol)) {
                // シンボルが未定義なら追加
                defines += ";" + symbol;

                // シンボルを設定
                PlayerSettings.SetScriptingDefineSymbols(buildTarget, symbol);

                // デバッグ用ログ
                Debug.Log($"Added define symbol: {SYMBOL_TO_DEFINE}");
            } else {
                //Debug.Log($"{SYMBOL_TO_DEFINE} is already defined.");
            }
        }
    }
}
#endif
