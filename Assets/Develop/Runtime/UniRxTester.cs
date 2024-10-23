using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using nitou;

namespace Project{

    public class UniRxTester : MonoBehaviour{

        private readonly ReactiveArray<Image> _inventory = new(10);

        private void Start() {

            Debug_.ListLog(_inventory.ToList());

        }







    }
}
