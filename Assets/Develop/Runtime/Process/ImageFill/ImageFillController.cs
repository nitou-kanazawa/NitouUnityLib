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
        private CancellationTokenSource _cts;

        private void Start() {
            startButton.onClick.AddListener(StartProcesses);
            pauseButton.onClick.AddListener(PauseProcesses);
            resumeButton.onClick.AddListener(ResumeProcesses);
            cancelButton.onClick.AddListener(CancelProcesses);
        }

        private void StartProcesses() {

            _cts?.Cancel();

            List<IProcess> processes = new List<IProcess>();
            foreach (var image in images) {
                processes.Add(new ImageFillProcess(image, 3f)); // 5ïbÇ©ÇØÇƒfillÇëùÇ‚Ç∑
            }

            _processExecuter = new ProcessExecuter(processes);
            _cts = new CancellationTokenSource();
            _processExecuter.Run(_cts.Token).Forget();
        }

        private void PauseProcesses() {
            _processExecuter?.Pause();
        }

        private void ResumeProcesses() {
            _processExecuter?.Resume();
        }

        private void CancelProcesses() {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}
