using UnityEngine;

namespace nitou.SceneSystem{

    /// <summary>
    /// Interface to perform processing in EnterPlayMode.
    /// </summary>
    internal interface IInitializeOnEnterPlayMode{

        void OnEnterPlayMode();
    }
}
