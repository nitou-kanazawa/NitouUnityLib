using UniRx;
using UnityEngine;



namespace Project
{
    using nitou;

    public class TestPresenter : MonoBehaviour
    {
        [SerializeField] EventSystemObserver _observer;
        [SerializeField] ScrollViewHighlighter _highlighter;


        private void Start() {
            _observer.OnSelected
                .Subscribe(s => _highlighter.SetTarget(s.GetComponent<RectTransform>()))
                .AddTo(this);
        }
    }
}
