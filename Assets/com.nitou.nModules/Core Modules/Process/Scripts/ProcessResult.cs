
namespace nitou.GameSystem {

    /// <summary>
    /// 結果データの基底クラス
    /// </summary>
    public abstract class ProcessResult {
        public bool IsSuccess() => this is CompleteResult;
        public bool IsCanceled() => this is CancelResult;
    }

    /// <summary>
    /// 成功時の結果データ
    /// </summary>
    public class CompleteResult : ProcessResult { }

    /// <summary>
    /// キャンセル時の結果データ
    /// </summary>
    public class CancelResult : ProcessResult {

        public readonly string message;

        public CancelResult(string message) {
            this.message = message;
        }

        public CancelResult() : this(string.Empty) { }
    }

}
