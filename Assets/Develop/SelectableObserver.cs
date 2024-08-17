using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

public class SelectableObserver : MonoBehaviour {

    private Subject<Selectable> _onSelectableSelected = new ();
    private GameObject _lastSelected = null;

    void Start() {

        // EventSystemのデフォルトモジュールを取得
        var eventSystem = EventSystem.current;
        if (eventSystem != null) {
            Observable.EveryUpdate()
                .Select(_ => eventSystem.currentSelectedGameObject)
                .Where(current => current != null && current != _lastSelected)  // 前フレームと異なる時だけ
                .Select(current => current.GetComponent<Selectable>())
                .Subscribe(selectable => {
                    _lastSelected = eventSystem.currentSelectedGameObject;
                    _onSelectableSelected.OnNext(selectable);
                });
        }
        
        // Selectableが選択された時の処理
        _onSelectableSelected.Subscribe(selectable => {
            Debug.Log("Selected Selectable: " + selectable.name);
            // ここに処理を追加する
        });
    }

    private void OnDestroy() {
        _onSelectableSelected.OnCompleted();
        _onSelectableSelected.Dispose();
    }
}
