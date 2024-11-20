using UnityEngine;

namespace nitou.EventChannel {
    using nitou.EventChannel.Shared;

    /// <summary>
    /// Event channel for type of <see cref="int"/>.
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_Int",
        menuName = AssetMenu.Prefix.EventChannel + "Int Event"
    )]
    public class IntEventChannel : EventChannel<int> { }

}