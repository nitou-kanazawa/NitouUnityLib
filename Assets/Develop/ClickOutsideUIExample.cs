using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

public class ClickOutsideUIExample : MonoBehaviour {
    public GameObject[] uiElements; // クリック検出対象のUI要素

    void Start() {
        // EventSystemから全てのクリックイベントを監視
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0)) // 左クリックを検出
            .Select(_ => EventSystem.current.IsPointerOverGameObject()) // UI上にカーソルがあるかチェック
            .Where(isOverUI => !isOverUI) // UI上にない場合のみ
            .Subscribe(_ => Debug.Log("Clicked outside of specified UI elements"))
            .AddTo(this);
    }
}
