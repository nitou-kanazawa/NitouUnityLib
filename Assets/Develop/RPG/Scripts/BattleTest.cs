using UnityEngine;
using Cysharp.Threading.Tasks;

namespace RPG
{
    public class BattleTest : MonoBehaviour
    {
        private TurnManager turnManager;

        private async void Start() {

            // キャラクターの初期化
            var playerCharacter = new Character("Hero", 100, 30, 20, 10, 5);
            var enemyCharacter = new Character("Slime", 80, 20, 15, 5, 3);

            // バトラーの初期化
            var player = new Battler(playerCharacter);
            var enemy = new Battler(enemyCharacter);

            // プレイヤーが敵を攻撃
            player.AttackTarget(enemy);

            // 遅延を入れて連続での攻撃を確認
            await UniTask.Delay(1000);

            // 敵がプレイヤーを攻撃
            enemy.AttackTarget(player);

            /*

            // TurnManagerの初期化
            turnManager = new TurnManager();

            // バトルの開始（テスト用）
            await turnManager.StartBattle();
            */

        }
    }
}
