using System;

namespace nitou.Tools.Assets {

    /// <summary>
    /// Assembly Definition Reference（.asmref）のJSONを表すクラス
    /// </summary>
    [Serializable]
    public sealed class JsonAssemblyDefinitionReference {
        public string reference = string.Empty;
    }
}