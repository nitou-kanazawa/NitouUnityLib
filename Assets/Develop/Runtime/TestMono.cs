using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using nitou;

namespace Project{

    public class TestMono : MonoBehaviour{

        [SerializeField] private List<GraphicRaycaster> raycasters; // チェックするGraphicRaycasterのリスト

        private void Update() {
            if (Input.GetMouseButtonDown(0)) // マウス左クリックのチェック
            {
                CheckOverlap();
            }
        }

        private void CheckOverlap() {
            // PointerEventDataを初期化
            var pointerEventData = new PointerEventData(EventSystem.current) {
                position = Input.mousePosition // クリックしたスクリーン座標を設定
            };

            // OverlapUIメソッドを呼び出して、UIが重なっているかをチェック
            bool isOverlap = raycasters.OverlapUI(pointerEventData);

            // 結果をログ出力
            if (isOverlap) {
                Debug_.Log("UIが重なっています。", Colors.Red);
            } else {
                Debug_.Log("UIが重なっていません。", Colors.Green);
            }
        }
    }
}
