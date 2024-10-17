using System;

namespace nitou{

    /// <summary>
    /// 識別可能なオブジェクト
    /// </summary>
    public interface IIdentifiable{
        Guid guid { get; }
    }
}
