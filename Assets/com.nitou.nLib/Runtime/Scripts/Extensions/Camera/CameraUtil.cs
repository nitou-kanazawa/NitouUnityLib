using System.Collections.Generic;
using UnityEngine;

namespace nitou {

    public static class CameraUtil {

        /// <summary>
        /// 登録されている全ての<see cref="Camera">カメラ</see>を非アクティブ状態に設定する
        /// </summary>
        public static void DeactivateAllCamera() {
            Camera.allCameras.ForEach(c => c.gameObject.SetActive(false));
        }

        /// <summary>
        /// 登録されている全ての<see cref="AudioListener">オーディオリスナー</see>を非アクティブ状態に設定する
        /// </summary>
        public static void DeactivateAllAudioListeners() {
            Camera.allCameras.ForEach(x => x.GetOrAddAudioListener().enabled = false);
        }
    }
}
