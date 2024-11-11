using UnityEngine;
using System;
using UniRx;

namespace RPG
{
    public class BattleSceneManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // 1 •ª‚©‚ç‚È‚¢
            IObservable<int> source1 = new Subject<int>();
            source1
                .Where(x => x >= 10)
                // ”ñ“¯Šúˆ—‚ð‹²‚Þ
                .SelectMany(x => ProcessValueAsync(x))
                .Subscribe();


            // 2 
            IObservable<int> ageStreem = new Subject<int>();
            IObservable<string> nameStreem = new Subject<string>();

            Observable
                .Zip(ageStreem.Select(x => x.ToString()), nameStreem, (age, name) => $"age: {age}, name: {name}")
                .Subscribe(data => Debug.Log(data));

            // 3
            IObservable<int> intStreem1 = new Subject<int>();
            IObservable<int> intStreem2 = new Subject<int>();

            Observable.Merge(intStreem1, intStreem2)
                .DistinctUntilChanged()
                .Subscribe();

            // 4 •ª‚©‚ç‚È‚¢
            IObservable<bool> isUserLoggedInStream = new Subject<bool>();
            IObservable<string> messageStream = new Subject<string>();

        }

        private IObservable<string> ProcessValueAsync(int value) {
            return Observable.Return($"Processed {value}");
        }


    }
}
