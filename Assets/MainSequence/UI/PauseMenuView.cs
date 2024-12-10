using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour {
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    public void Initialize(System.Action onResume, System.Action onQuit) {
        // ボタンイベント設定
        resumeButton.onClick.AddListener(() => {
            pauseMenuPanel.SetActive(false);
            onResume?.Invoke();
        });

        quitButton.onClick.AddListener(() => {
            onQuit?.Invoke();
        });
    }

    public void ShowPauseMenu() {
        pauseMenuPanel.SetActive(true);
    }

    public void HidePauseMenu() {
        pauseMenuPanel.SetActive(false);
    }
}
