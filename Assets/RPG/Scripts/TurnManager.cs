using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RPG {
    public class TurnManager {

        private List<Battler> _battlers;
        private Battler _currentBattler;

        private Queue<BattlerAction> _actionQueue = new();


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public TurnManager(List<Battler> battlers) {
            _battlers = battlers;
        }


        public async UniTask StartBattle() {
            while (!IsBattleOver()) {
                // プレイヤー入力フェーズ
                await HandlePlayerInput();

                // ターン開始時処理
                BeginTurn();

                // バトラー行動フェーズ
                await ExecuteActions();

                // ターン終了時処理
                EndTurn();
            }

            Debug.Log("Battle Ended");
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private async UniTask HandlePlayerInput() {
            // プレイヤーに入力を促し、行動をキューに登録
            // ここでは例として固定の行動を入れる
            _actionQueue.Enqueue(new BattlerAction { /* 行動内容 */ });
            await UniTask.Delay(1000); // プレイヤーの選択待機シミュレーション
        }

        private void BeginTurn() {
            Debug.Log("Turn Start");
            // 状態異常のターン開始時効果適用など
        }

        private async UniTask ExecuteActions() {
            while (_actionQueue.Count > 0) {
                BattlerAction action = _actionQueue.Dequeue();
                await action.Execute();
            }
        }

        private void EndTurn() {
            Debug.Log("Turn End");
            // 状態異常の経過ターン更新や、効果終了判定など
        }

        private bool IsBattleOver() {
            // 全員のHPが0ならバトル終了
            return false; // 仮の実装
        }

    }
}
