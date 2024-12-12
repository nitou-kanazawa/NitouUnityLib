using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Project {
}

public class ObservableCollider : MonoBehaviour {

    private void Start() {
        var targetObservable =
            // IObservable<Collider>型
            this.OnCollisionEnterAsObservable()
            // IObservable<IObservable<Vector3>>型
            // （※ColliderからIObservable<Vector3>を作っている）
            .Select(collider => CreatePositionObservable(collider.gameObject));

        targetObservable
            .Switch()
            .Subscribe(targetPos => {
                    // 対象を追跡
                    var newPosition = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
                transform.position = newPosition;
            })
            .AddTo(this);
    }

    // 
    private IObservable<Vector3> CreatePositionObservable(GameObject target) {
        return target.UpdateAsObservable()  // 毎フレーム監視
            .Select(_ => target.transform.position);
    }
}
