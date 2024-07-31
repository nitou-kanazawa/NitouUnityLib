using System.Collections.Generic;
using UnityEngine;

namespace nitou{

    public interface ISmoothDampValue<TValue>
        where TValue : struct{

        /// <summary>
        /// •½ŠŠ‰»‚³‚ê‚½’l‚ðŽæ“¾‚·‚é
        /// </summary>
        public TValue GetNext(TValue target, float smoothTime);

        /// <summary>
        /// •½ŠŠ‰»‚³‚ê‚½’l‚ðŽæ“¾‚·‚é
        /// </summary>
        public TValue GetNext(TValue target, float smoothTime, float maxSpeed);

        /// <summary>
        /// •½ŠŠ‰»‚³‚ê‚½’l‚ðŽæ“¾‚·‚é
        /// </summary>
        public TValue GetNext(TValue target, float smoothTime, float maxSpeed, float deltaTime);
    }
}
