using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Project {
    public class RepetTest : MonoBehaviour {

        void Start() {

            // Zキーの入力が変化したら通知するObservable
            var zKeyOnChanged = this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Z))
                .DistinctUntilChanged()
                .Skip(1);

            // Zキーが1秒以上押されたら通知するObservable
            var zKeyLongPressStart = zKeyOnChanged
                .Throttle(TimeSpan.FromSeconds(1))
                .Where(x => x);

            // Zキーが離されたら通知するObservable
            var zKeyRelease = zKeyOnChanged.Where(x => !x);

            // 1秒以上Zキーが長押しされている間、メソッドを実行する
            this.UpdateAsObservable()
                .SkipUntil(zKeyLongPressStart)  // 長押しされるまで待機
                .TakeUntil(zKeyRelease)     // キーが離されたら終了
                .RepeatUntilDestroy(this)
                .Subscribe(_ => {
                    Debug.Log("Long press state!");
                }).AddTo(this);

            var streem = new Subject<Unit>();

            // 1
            streem.ThrottleFirst(TimeSpan.FromSeconds(1f))
                .Subscribe();

            // 2



        }

    }
}
