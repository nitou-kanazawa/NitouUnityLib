using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Rpg {

    public class TurnBasedBattleSystem : MonoBehaviour {
        
        public Character player;
        public Character enemy;
        private bool isBattleOver = false;

        private async void Start() {
            await StartBattle();
        }

        private async UniTask StartBattle() {
            Debug.Log("バトル開始！");
            while (!isBattleOver) {
                await PlayerTurn();
                if (isBattleOver) break;

                await EnemyTurn();
                if (isBattleOver) break;
            }
        }

        private async UniTask PlayerTurn() {
            Debug.Log("プレイヤーのターン");
            // 入力待機（例: 攻撃選択を待つ）
            var action = await WaitForPlayerAction();

            // 入力に応じて行動処理
            switch (action) {
                case PlayerAction.Attack:
                    await PerformAttack(player, enemy);
                    break;
                case PlayerAction.Defend:
                    await PerformDefense(player);
                    break;
            }

            // 勝敗判定
            if (enemy.Health <= 0) {
                Debug.Log("プレイヤーの勝利！");
                isBattleOver = true;
            }
        }

        private async UniTask EnemyTurn() {
            Debug.Log("敵のターン");

            // 敵の行動決定 (例: ランダムで攻撃)
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            await PerformAttack(enemy, player);

            // 勝敗判定
            if (player.Health <= 0) {
                Debug.Log("プレイヤーの敗北…");
                isBattleOver = true;
            }
        }

        private async UniTask<PlayerAction> WaitForPlayerAction() {
            // ボタン入力の待機などの処理
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.A)); // 攻撃
            return PlayerAction.Attack;
        }

        private async UniTask PerformAttack(Character attacker, Character defender) {
            Debug.Log($"{attacker.Name} が {defender.Name} に攻撃！");
            await UniTask.Delay(TimeSpan.FromSeconds(1)); // アニメーション再生
            defender.TakeDamage(attacker.AttackPower);
            Debug.Log($"{defender.Name} のHP: {defender.Health}");
        }

        private async UniTask PerformDefense(Character character) {
            Debug.Log($"{character.Name} は防御の態勢を取った！");
            await UniTask.Delay(TimeSpan.FromSeconds(1)); // アニメーション再生
            character.Defend();
        }
    }

    public enum PlayerAction {
        Attack,
        Defend
    }

    public class Character {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }

        public void TakeDamage(int damage) {
            Health -= damage;
        }

        public void Defend() {
            // 防御時の処理
        }
    }

}