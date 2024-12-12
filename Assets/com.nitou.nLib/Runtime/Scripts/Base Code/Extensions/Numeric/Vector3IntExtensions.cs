using System;
using UnityEngine;

// [参考]
//  コガネブログ:　Vector3Intの代入を簡略化するDeconstruction　https://baba-s.hatenablog.com/entry/2019/09/03/230600

namespace nitou {

    public static partial class Vector3IntExtensions     {

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct(this Vector3Int self, out int x, out int y, out int z) {
            x = self.x;
            y = self.y;
            z = self.z;
        }
    }
}
