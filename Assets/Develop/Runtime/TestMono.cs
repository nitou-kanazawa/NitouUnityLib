using UnityEngine;
using UnityEngine.UI;
using nitou; // RectTransformExtensions のある名前空間

namespace Project {
    public class TestMono : MonoBehaviour {

        public RectTransformExtensions.Corner corner;

        [SerializeField] private RectTransform _worldCanvasRect;  // 操作対象の RectTransform
        [SerializeField] private Button[] _buttons;               // ボタン配列（複数のボタン）
        private int _currentButtonIndex = 0;                      // 現在のボタン参照

        private void Start() {
            // 各ボタンに異なる座標への移動イベントを追加
            for (int i = 0; i < _buttons.Length; i++) {
                int index = i;  // クロージャ対策
                _buttons[i].onClick.AddListener(() => OnButtonClicked(index));
            }
        }

        /// <summary>
        /// ボタンがクリックされたときの処理
        /// </summary>
        /// <param name="buttonIndex">押されたボタンのインデックス</param>
        private void OnButtonClicked(int buttonIndex) {
            // ボタンごとに異なるターゲット位置を設定
            Vector2[] targetPositions = new Vector2[]
            {
                new Vector2(1, 1),  // ボタン1を押したときのターゲット位置
                new Vector2(2, 2),  // ボタン2を押したときのターゲット位置
                new Vector2(-2, 1),  // ボタン3を押したときのターゲット位置
                new Vector2(0       , 0),  // ボタン3を押したときのターゲット位置
            };

            // RectTransform の位置を SetWorldPosition で移動
            if (buttonIndex < targetPositions.Length) {
                //_worldCanvasRect.SetWorldPosition(targetPositions[buttonIndex], corner);
                _worldCanvasRect.SetWorldCenterPosition(targetPositions[buttonIndex]);
                Debug.Log($"Button {buttonIndex + 1} clicked: New Position = {targetPositions[buttonIndex]}");
            } else {
                Debug.LogWarning("Invalid button index or target position");
            }

            // 現在の座標を更新する
            UpdateButtonText(_buttons[buttonIndex], targetPositions[buttonIndex]);
        }

        /// <summary>
        /// ボタンのテキストに現在の位置を表示
        /// </summary>
        /// <param name="button">対象のボタン</param>
        /// <param name="position">表示する座標</param>
        private void UpdateButtonText(Button button, Vector2 position) {
            var buttonText = button.GetComponentInChildren<Text>();
            if (buttonText != null) {
                buttonText.text = $"Move to ({position.x}, {position.y})";
            }
        }
    }
}
