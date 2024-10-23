using System.Collections.Generic;
using UniRx;
using UnityEngine;
using nitou;

namespace Project{

    public class UniRxTester : MonoBehaviour{


        private void Start() {
            // ReactiveArray の初期化 (長さ5)
            ReactiveArray<float> reactiveArray = new ReactiveArray<float>(new List<float> { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f });

            // Replace イベントの購読
            reactiveArray.ObserveReplace().Subscribe(replaceEvent => {
                Debug.Log($"Item replaced at index {replaceEvent.Index}: {replaceEvent.OldValue} -> {replaceEvent.NewValue}");
            }).AddTo(this); // MonoBehaviourにライフタイムをバインド

            // Move イベントの購読
            reactiveArray.ObserveMove().Subscribe(moveEvent => {
                Debug.Log($"Item moved from index {moveEvent.OldIndex} to {moveEvent.NewIndex}: {moveEvent.Value}");
            }).AddTo(this);

            // いくつかのテスト操作を実行
            Debug.Log("Initial array:");
            PrintArray(reactiveArray);

            // インデックス2の要素を0.35に変更
            reactiveArray.SetItem(2, 0.35f);

            reactiveArray[3] = 10f;
            reactiveArray.SetItem(3, 99);

            // インデックス1の要素をインデックス4に移動
            reactiveArray.Move(1, 4);

            // 結果の配列を表示
            Debug.Log("Updated array:");
            PrintArray(reactiveArray);
        }

        // 配列の内容を出力するヘルパーメソッド
        private void PrintArray(ReactiveArray<float> array) {
            for (int i = 0; i < array.Count; i++) {
                Debug.Log($"Index {i}: {array[i]}");
            }
        }





    }
}
