using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou{

    /// <summary>
    /// タイマーの基本操作を定義したインターフェース
    /// </summary>
    public interface ITimer {
        public void Start();
        public void Stop();
        public void Reset();
    }
}
