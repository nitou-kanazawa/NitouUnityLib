using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;


namespace Demo.Sequencer {

    public class SceneEntryPoint : MonoBehaviour {

        [SerializeField] PlayableDirector _director;


        private  async void Start() {

        }


        public async UniTask PlayTimelineAsync() {

            if(_director == null){
                Debug.LogError("PlayableDirectorが設定されていません。");
                return;
            }

            // 
            _director.Play();

            await UniTask.WaitUntil(() => _director.state != PlayState.Playing);

            Debug.Log("タイムラインが終了しました．");
        }


    }
}
