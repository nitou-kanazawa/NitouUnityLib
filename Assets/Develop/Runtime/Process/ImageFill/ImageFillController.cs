using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Test {

    public class ImageFillController : MonoBehaviour {
        
        [SerializeField] private List<Image> images;
        [SerializeField] private Button startButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button cancelButton;

        private ProcessExecuter _processExecuter;
        private CancellationTokenSource _cancellationTokenSource;

        private void Start() {
            startButton.onClick.AddListener(StartProcesses);
            pauseButton.onClick.AddListener(PauseProcesses);
            resumeButton.onClick.AddListener(ResumeProcesses);
            cancelButton.onClick.AddListener(CancelProcesses);
        }

        private void StartProcesses() {
            List<IProcess> processes = new List<IProcess>();
            foreach (var image in images) {
                processes.Add(new ImageFillProcess(image, 5f)); // 5ïbÇ©ÇØÇƒfillÇëùÇ‚Ç∑
            }

            _processExecuter = new ProcessExecuter(processes);
            _cancellationTokenSource = new CancellationTokenSource();
            _processExecuter.Run(_cancellationTokenSource.Token).Forget();
        }

        private void PauseProcesses() {
            _processExecuter?.Pause();
        }

        private void ResumeProcesses() {
            _processExecuter?.Resume();
        }

        private void CancelProcesses() {
            _cancellationTokenSource?.Cancel();
        }
    }
}
