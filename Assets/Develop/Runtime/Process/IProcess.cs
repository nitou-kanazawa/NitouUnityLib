using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Test{

    public interface IProcess{
        UniTask RunAsync(CancellationToken token);
        void Pause();
        void Resume();
    }
}
