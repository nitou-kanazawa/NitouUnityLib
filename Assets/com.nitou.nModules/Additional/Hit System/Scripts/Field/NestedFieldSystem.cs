using UnityEngine;

namespace nitou.HitSystem {
    using nitou.BachProcessor;
    public partial class NestedField {


        private class NestedFieldSystem :
            SystemBase<NestedField, NestedFieldSystem>,
            IEarlyUpdate {

            /// <summary>
            /// 1度に検出できるコリジョンの最大数.
            /// </summary>
            private const int CAPACITY = 50;

            private readonly Collider[] _results = new Collider[CAPACITY];

            /// <summary>
            /// システムの実行順序
            /// </summary>
            int ISystemBase.Order => 0;

            /// ----------------------------------------------------------------------------
            // Method

            private void OnDestroy() => UnregisterAllComponents();

            /// <summary>
            /// 更新処理
            /// </summary>
            void IEarlyUpdate.OnUpdate() {

                // Initialize components
                foreach (var component in Components) {
                    component.PrepareFrame();
                }

                // Update collision detection using Physics
                foreach (var component in Components) {
                    component.OnUpdate(in _results);
                }


            }
        }
    }
}
