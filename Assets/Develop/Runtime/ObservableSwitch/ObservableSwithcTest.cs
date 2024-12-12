using UnityEngine;
using UniRx;

namespace Project
{
    public class ObservableSwithcTest : MonoBehaviour {

        private ActorSelectionController _selectionController;

        void Start()
        {
            _selectionController = new ActorSelectionController();

            var actor1 = new Actor ();
            var actor2 = new Actor ();
            var actor3 = new Actor ();

            _selectionController.CurrentActorRP
                .Where(actor => actor is not null)
                .Select(actor => actor.AttackRP)
                .Switch()
                .Subscribe(attack => Debug.Log($"[Switch] Attack : {attack} "))
                .AddTo(this);

        }

        private void TestObservableBehavior() {

        }
    }




}
