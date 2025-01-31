﻿using System;
using UnityEngine;

namespace nitou.TweenPlayables {

    [Serializable]
    public class TweenCameraBehaviour : TweenAnimationBehaviour<Camera> {
        
        public FloatTweenParameter orthographicSize;
        public FloatTweenParameter fieldOfView;
        public ColorTweenParameter backgroundColor;

        public override void OnTweenInitialize(Camera playerData) {
            orthographicSize.SetInitialValue(playerData, playerData.orthographicSize);
            fieldOfView.SetInitialValue(playerData, playerData.fieldOfView);
            backgroundColor.SetInitialValue(playerData, playerData.backgroundColor);
        }
    }

}