using UnityEngine;

namespace nitou.UI.Component {

    /// <summary>
    /// スコア表示を担うUIであることを示すインターフェース
    /// </summary>
    public interface IScoreTextView{

        /// <summary>
        /// スコアを設定する
        /// </summary>
        public void SetScore(int value);

        /// <summary>
        /// デフォルト値を設定する
        /// </summary>
        public void SetDefaule();
    }

}