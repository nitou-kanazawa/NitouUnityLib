using UnityEngine;

namespace nitou.UI.Component {

    public interface IIconView {

        /// <summary>
        /// スプライトを設定する
        /// </summary>
        public void SetSprite(Sprite sprite);

        /// <summary>
        /// スプライトのネイティブサイズを適用する
        /// </summary>
        public void SetNativeSize(float scale);
    }
}
