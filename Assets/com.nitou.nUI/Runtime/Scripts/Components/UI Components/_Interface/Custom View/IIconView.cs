using UnityEngine;

namespace nitou.UI.Component {

    public interface IIconView {

        /// <summary>
        /// Set sprite.
        /// </summary>
        public void SetSprite(Sprite sprite);

        /// <summary>
        /// Set size of sprite.
        /// </summary>
        public void SetNativeSize(float scale);
    }
}
