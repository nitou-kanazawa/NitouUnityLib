using System.Collections.Generic;
using UnityEngine;

namespace nitou.Tools.CodeGeneration{

    public sealed class CodeInfo{
        public string className;
        public string namespaceName;


        public CodeInfo(string className, string namespaceName) {
            this.className = className;
            this.namespaceName = namespaceName;
        }
    }

}
