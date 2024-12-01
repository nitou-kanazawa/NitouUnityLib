using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Unity.SceneManagement.Internal {

    /// <summary>
    /// A class that manages scene loading.
    /// </summary>
    internal class SceneLoadManager : IInitializeOnEnterPlayMode {
        
        private readonly List<GameObject> _rootObjects = new();

        /// <summary>
        /// List of currently loaded scenes.
        /// </summary>
        private readonly List<AssetReferenceScene> _loadedHandles = new();


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public SceneLoadManager() {
            SceneInitialization.Register(this);
        }

        /// <summary>
        /// 初期化処理（エディタ）．
        /// </summary>
        void IInitializeOnEnterPlayMode.OnEnterPlayMode() {
            _loadedHandles.Clear();
        }

        /// <summary>
        /// Load a scene.
        /// </summary>
        /// <param name="scene">The scene to load.</param>
        /// <param name="priority">The priority of the scene.</param>
        /// <param name="isActive">Set the scene as active when loaded.</param>
        /// <param name="opHandle">The load handle.</param>
        /// <returns>True if the load operation was executed.</returns>
        public AsyncOperationHandle<SceneInstance> Load(AssetReferenceScene scene, int priority, bool isActive) {
            
            if (_loadedHandles.Contains(scene))
                throw new Exception($"{scene} already loaded.");

            var opHandle = scene.LoadSceneAsync(LoadSceneMode.Additive, true, priority);
            opHandle.Completed += handle => {

                if (handle.Status is not AsyncOperationStatus.Succeeded) {
                    throw handle.OperationException;
                }
                // アクティブシーン設定
                if (isActive)
                    SceneManager.SetActiveScene(handle.Result.Scene);
            };

            _loadedHandles.Add(scene);
            return opHandle;
        }

        /// <summary>
        /// Unload a scene.
        /// </summary>
        /// <param name="sceneReference">The scene to unload.</param>
        /// <param name="sceneName">The name of the scene.</param>
        /// <param name="onComplete">Callback when the release operation is completed.</param>
        public void Unload(AssetReferenceScene sceneReference, string sceneName, Action onComplete) {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) {
                onComplete.Invoke();
                return;
            }
#endif
            var sceneInstance = SceneManager.GetSceneByName(sceneName);
            if (sceneInstance.IsValid() == false) {
                _loadedHandles.Remove(sceneReference);
                Debug.Log("is valid == false");
                onComplete.Invoke();
                return;
            }

            DeactivateGameObjects(sceneInstance);

            // Scenes loaded from SceneLoader (Addressable) are released using Addressable.
            if (_loadedHandles.Contains(sceneReference)) {
                _loadedHandles.Remove(sceneReference);
                var op = sceneReference.UnLoadScene();
                op.CompletedTypeless += _ => onComplete?.Invoke();
            } 
            // Scenes loaded from SceneManager are released using SceneManager.
            else {
                var op = SceneManager.UnloadSceneAsync(sceneInstance);
                op.completed += _ => onComplete?.Invoke();
            }
        }

        /// <summary>
        /// deactivate all scene game objects.
        /// </summary>
        /// <param name="sceneInstance"></param>
        private void DeactivateGameObjects(Scene sceneInstance) {
            if (sceneInstance.isLoaded == false)
                return;

            sceneInstance.GetRootGameObjects(_rootObjects);
            foreach (var obj in _rootObjects)
                obj.SetActive(false);
        }
    }
}
