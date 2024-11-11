using UnityEngine;
using Cysharp.Threading.Tasks;

namespace RPG{

    public class BattlerAction {

        public async UniTask Execute() {
            Debug.Log("Execute action");
            await UniTask.Delay(500);
        }

    }
}
