using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Project{

    public class UniRxTester : MonoBehaviour{

        private readonly Subject<Collider> _onOverlapSubject = new ();



        private void Start() {
            _onOverlapSubject
                .Where(c => c.GetComponent<AudioSource>() != null)
                .SelectMany(c => c.GetComponents<AudioSource>())
                .Subscribe(a => Debug.Log(a));
        }


        private void OnTriggerEnter(Collider other) {
            _onOverlapSubject.OnNext(other);
        }


    }
}
