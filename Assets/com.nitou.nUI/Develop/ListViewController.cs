using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro;

namespace Develop {

    [RequireComponent(typeof(LoopScrollRect))]
    [DisallowMultipleComponent]
    public sealed class ListViewController : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource {

        [SerializeField] LoopScrollRect _scrollRect;
        [SerializeField] GameObject _prefab;

        public int totalCount = -1;
        public float offset =100f;
        private ObjectPool<GameObject> _pool;


        /// ----------------------------------------------------------------------------
        // Lifecycle Events

        private void Start() {
            // オブジェクトプールを作成
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateInstance,
                actionOnGet: OnGet,
                actionOnRelease: OnReturn);

            _scrollRect.prefabSource = this;
            _scrollRect.dataSource = this;
            _scrollRect.totalCount = totalCount;
            _scrollRect.RefillCells(0, offset);
        }


        /// ----------------------------------------------------------------------------

        // LoopScrollPrefabSourceの実装
        // GameObjectが新しく表示のために必要になった時に呼ばれる
        GameObject LoopScrollPrefabSource.GetObject(int index) {
            return _pool.Get();
        }

        // LoopScrollPrefabSourceの実装
        // GameObjectが不要になった時に呼ばれる
        void LoopScrollPrefabSource.ReturnObject(Transform trans) {
            _pool.Release(trans.gameObject);
        }

        // LoopScrollDataSourceの実装
        // 要素が表示される時の処理を書く
        void LoopScrollDataSource.ProvideData(Transform trans, int index) {

            if (trans.TryGetComponent<ListCell>(out var cell)) {
                cell.SetIndex(index);
            }
        }



        /// ----------------------------------------------------------------------------
        // Private Method

        private GameObject CreateInstance() {
            return Instantiate(_prefab);
        }

        private void OnGet(GameObject instance) {
            instance.SetActive(true);
        }

        private void OnReturn(GameObject instance) {
            instance.transform.SetParent(transform);
            instance.SetActive(false);
        }
    }
}