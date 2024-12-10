using System;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace UnityScreenNavigator.Runtime.Core.Modal{

    /// <summary>
    /// <see cref="ModalContainer"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class ModalContainerExtensions {


        /// <summary>
        /// コンテナが空かどうか．
        /// </summary>
        public static bool IsEmpty(this ModalContainer container) {
            return container.Modals.Count == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public async static UniTask<TModal> PushModal<TModal>(this ModalContainer self,
            string resourceKey, bool playAnimation, string modalId = null, bool loadAsync = true,
            Action<(string modalId, TModal modal)> onLoad = null) where TModal: Modal{

            // Modalの追加
            TModal modal = null;
            await self.Push<TModal>(resourceKey, playAnimation, modalId, loadAsync,
                x => {
                    modal = x.modal;
                    onLoad?.Invoke(x);
                });

            return modal;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Modalの取得)

        /// <summary>
        /// アクティブなModalを取得する
        /// </summary>
        public static Modal GetActiveModal(this ModalContainer container) {
            if (container.Modals.Count <= 0) return null;

            var modalId = container.OrderedModalIds[container.Modals.Count - 1];
            return container.Modals[modalId];
        }

        /// <summary>
        /// アクティブなModalを取得する
        /// </summary>
        public static bool TryGetActiveModal(this ModalContainer container, out Modal modal) {
            modal = container.GetActiveModal();
            return modal != null;
        }

        /// <summary>
        /// アクティブなModalを取得する
        /// </summary>
        public static Modal GetPreviousModal(this ModalContainer container) {
            if (container.Modals.Count <= 1) return null;

            var pageId = container.OrderedModalIds[container.Modals.Count - 2];
            return container.Modals[pageId];
        }


        /// ----------------------------------------------------------------------------
        // Public Method ()

        /// <summary>
        /// リフレクションでコンテナキー変数を書き換える拡張メソッド
        /// </summary>
        public static void SetContainerKey(this ModalContainer container, string key) {
            var fieldInfo = container.GetType()
                .GetField("_name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null) fieldInfo.SetValue(container, key);
        }
    }
}
