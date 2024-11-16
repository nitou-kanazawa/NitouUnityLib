using UnityEngine;
using DG.Tweening;

namespace nitou.UI.Component{

    /// <summary>
    /// 表示・非表示の切り替えが可能な基本UI
    /// </summary>
    public interface IHideableView {

        public Tweener DOShow(float duration);

        public Tweener DOHide(float duration);
    }
}
