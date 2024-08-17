
namespace nitou{

    /// <summary>
    /// 範囲を表すインターフェース
    /// </summary>
    public interface IRangeValue<TValue> 
        where TValue : struct{

        TValue Min { get; set; }
        TValue Max { get; set; }

        /// <summary>
        /// 中央値
        /// </summary>
        TValue Mid { get; }
        
        /// <summary>
        /// 範囲の長さ
        /// </summary>
        TValue Length { get; }
        
        /// <summary>
        /// 範囲内のランダムな値
        /// </summary>
        TValue Random { get; }

        /// <summary>
        /// 値が範囲内か調べる
        /// </summary>
        bool Contains(TValue value);
    }
}
