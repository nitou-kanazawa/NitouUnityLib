using UnityEngine;

namespace nitou {

    using nitou.Inspector;

    public class testAttri : MonoBehaviour {

        [Title("aaaa")]

        [SerializeField,Indent] int a;
        
        [HideInPlayMode]
        [SerializeField,Indent] int b;

        [DisableInPlayMode]
        [SerializeField, Indent] int f;
    }
}
