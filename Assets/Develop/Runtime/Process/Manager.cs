using UnityEngine;
using UniRx;

namespace Project.Test{

    public enum EngineState {
        Teaching,
        Playing,
    }


    /// <summary>
    /// 教示モードと再生モードをもつエディタマネージャークラス．
    /// </summary>
    public class Manager : MonoBehaviour{

        private readonly ReactiveProperty<EngineState> _currentStateRP = new(EngineState.Teaching);

        public IReadOnlyReactiveProperty<EngineState> CurrentStateRP => _currentStateRP;

    }
}
