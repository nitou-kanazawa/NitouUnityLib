using System.Threading;
using Cysharp.Threading.Tasks;

namespace Demo.Sequencer {

    /// <summary>
    /// 
    /// </summary>
    public interface ISequence{

        /// <summary>
        /// シーケンス実行．
        /// </summary>
        UniTask Run(CancellationToken token = default);
    }
}
