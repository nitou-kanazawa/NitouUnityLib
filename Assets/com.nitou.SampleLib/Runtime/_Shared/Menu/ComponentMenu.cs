using UnityEngine;

namespace nitou {

    /// <summary>
    /// "Component Menu"のキーワード定義 (※AddComponentMenu属性の呼び出し時に使用する)
    /// </summary>
    public static partial class ComponentMenu {

        /// ----------------------------------------------------------------------------
        // 接頭辞
        public static partial class Prefix {

            // Default Category
            public const string Audio = "Audio/";
            public const string Phisics = "Phisics/";


            public const string EventChannel = "Event Channel/";

        }

        
        /// ----------------------------------------------------------------------------
        // 接尾辞
        public static class Suffix {

        }


        /// ----------------------------------------------------------------------------
        // 表示順
        public static class Order {

            public const int EventChannel = 0;

        }
    }

}