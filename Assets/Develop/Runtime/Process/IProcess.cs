using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Test{

    public interface IProcess{

        void OnBeforeRun();
        void OnComplete();

        UniTask RunAsync(CancellationToken token);
        void Pause();
        void Resume();
    }
}
