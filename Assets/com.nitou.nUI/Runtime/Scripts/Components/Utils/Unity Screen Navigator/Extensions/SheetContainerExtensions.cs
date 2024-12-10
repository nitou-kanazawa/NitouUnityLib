using UnityEngine;

namespace UnityScreenNavigator.Runtime.Core.Sheet {

    /// <summary>
    /// <see cref="SheetContainer"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class SheetContainerExtensions {

        /// <summary>
        /// コンテナが空かどうか．
        /// </summary>
        public static bool IsEmpty(this SheetContainer container) {
            return container.Sheets.Count == 0;
        }

        /// <summary>
        /// アクティブなシートが存在するかどうか．
        /// </summary>
        public static bool HasActiveSheet(this SheetContainer container) {
            return container.ActiveSheet != null;
        }

    }
}
