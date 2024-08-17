using UnityEngine;

namespace nitou.DebugInternal{

    [DisallowMultipleComponent]
    public abstract class DebugComponent: MonoBehaviour{
    }


    [DisallowMultipleComponent]
    public abstract class DebugComponent<TComponent> : MonoBehaviour 
        where TComponent : Component{
    }
}
