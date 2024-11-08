using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;



public class RetryOnErrorExample : MonoBehaviour {

    private Subject<Unit> stopTimerSubject = new Subject<Unit>();

    private void Start() {

        var inputObservable = this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .FirstOrDefault();


        // タイマー処理を記述する
        Observable.Interval(TimeSpan.FromSeconds(3))
            .TakeUntil(inputObservable)
            .Subscribe(t => Debug.Log($"time: {t}"))
            .AddTo(this);


    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // タイマー停止の処理を記述する
        }
    }

}


