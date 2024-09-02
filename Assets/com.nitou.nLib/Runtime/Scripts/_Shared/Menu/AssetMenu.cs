
namespace nitou {

    /// <summary>
    /// "Create Asset Menu"用の各種定義
    /// </summary>
    public static class AssetMenu {

        /// ----------------------------------------------------------------------------
        // 接頭辞
        public static class Prefix {

            /// <summary>
            /// スクリプタブルオブジェクト
            /// </summary>
            public const string ScriptableObject = "Scriptable Objects/";

            /// <summary>
            /// キャラクターモデル
            /// </summary>
            public const string ActorModelInfo = ScriptableObject + "Actor Model/";

            /// <summary>
            /// クレジット情報
            /// </summary>
            public const string CreditInfo = ScriptableObject + "Credit Info/";

            /// <summary>
            /// シーン情報
            /// </summary>
            public const string SceneInfo = ScriptableObject + "Scene Info/";

            // ----- 

            /// <summary>
            /// イベントチャンネル
            /// </summary>
            public const string EventChannel = "Event Channel/";
            
            /// <summary>
            /// アニメーションデータ
            /// </summary>
            public const string AnimationData = "Animation Data/";

            /// <summary>
            /// キャラクター操作関連
            /// </summary>
            public const string CharacterControl = "CharacterControl/";
        }


        /// ----------------------------------------------------------------------------
        // 接尾辞
        public static class Suffix {

        }


        /// ----------------------------------------------------------------------------
        // 表示順
        public static class Order {
            public static readonly int VeryEarly = 100;
            public static readonly int Early = 50;
            public static readonly int Normal = 0;
            public static readonly int Late = -50;
            public static readonly int VeryLate = -100;
        }
    }

}