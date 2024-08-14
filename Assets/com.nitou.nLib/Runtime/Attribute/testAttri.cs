using UnityEngine;

namespace nitou {

    using nitou.Inspector;

    public class testAttri : MonoBehaviour {


        public int a;
        [ReadOnly] public int b;
        [Indent] public int e;
        [Title("Title")]
        [Indent]public int f;
    }
}
