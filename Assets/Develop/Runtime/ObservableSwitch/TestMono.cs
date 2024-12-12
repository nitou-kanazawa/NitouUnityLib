using UnityEngine;
using UniRx;

namespace Project {
}

public class Actor {
    // 攻撃力
    public ReactiveProperty<int> AttackRP { get; } = new(0);
}

public class ActorSelectionController {
    // 選択中のアクター
    public ReactiveProperty<Actor> CurrentActorRP { get; } = new(null);
}


public class TestMono : MonoBehaviour {

    private ActorSelectionController _actorSelectionController = new();

    private void Start() {
        _actorSelectionController.CurrentActorRP
            .Where(actor => actor != null)
            
            // 変更箇所
            .SelectMany(actor => actor.AttackRP)
            //.Select(actor => actor.AttackRP)
            //.Switch()

            .Subscribe(attack => Debug.Log($"Actor's Attack : {attack}"))
            .AddTo(this);

        // サンプルシナリオ
        SimulateActorSwitching();
    }

    private void SimulateActorSwitching() {
        // Actorの生成
        var actor1 = new Actor();
        var actor2 = new Actor();

        // Actor1を選択
        _actorSelectionController.CurrentActorRP.Value = actor1;

        // Actor1のAttackRPを変更
        actor1.AttackRP.Value = 1;
        actor1.AttackRP.Value = 2;
        actor1.AttackRP.Value = 3;

        // Actor2を選択
        _actorSelectionController.CurrentActorRP.Value = actor2;

        // Actor2のAttackRPを変更
        actor2.AttackRP.Value = 10;
        actor2.AttackRP.Value = 20;
        actor2.AttackRP.Value = 30;

        // 再びActor1を選択
        _actorSelectionController.CurrentActorRP.Value = actor1;

        // Actor1のAttackRPを変更
        actor1.AttackRP.Value = 5;
        actor1.AttackRP.Value = 6;
        actor1.AttackRP.Value = 7;
    }
}
