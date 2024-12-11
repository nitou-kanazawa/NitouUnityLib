using System;
using UniRx;

namespace Project
{
    public class Actor : IDisposable {
        
        private readonly ReactiveProperty<int> _attackRP = new(0);
        public IReadOnlyReactiveProperty<int> AttackRP => _attackRP;

        public void Dispose() {
            _attackRP.Dispose();
        }

        public void SetAttack(int value) {
            _attackRP.Value = value;
        }
    }


    public class ActorSelectionController {

        private readonly ReactiveProperty<Actor> _currentActorRP = new(null);
        public IReadOnlyReactiveProperty<Actor> CurrentActorRP => _currentActorRP;


        public void Select(Actor actor) {
            _currentActorRP.Value = actor;
        }

    }
}
