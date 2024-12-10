using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

// [参考]
//  LIGHT11: Reflectionを使ってメソッドを呼んだり変数を書き換えたりキャッシュして高速化したりする https://light11.hatenadiary.com/entry/2019/05/27/213206

namespace UnityScreenNavigator.Runtime.Core.Page {

    /// <summary>
    /// <see cref="PageContainer"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class PageContainerExtensions {

        /// ----------------------------------------------------------------------------
        // Public Method (Pageの追加)

        /// <summary>
        /// 返り値でPageを返すPush()の拡張メソッド
        /// </summary>
        public static async UniTask<TPage> PushPage<TPage>(this PageContainer container,
            string resourceKey, bool playAnimation, bool stack = true,
            string pageId = null, bool loadAsync = true,
            Action<(string pageId, TPage page)> onLoad = null) 
            where TPage : Page {

            if (container == null) throw new ArgumentNullException(nameof(container));

            // Page読み込み
            TPage page = null;
            await container.Push<TPage>(resourceKey, playAnimation, stack, pageId, loadAsync,
                x => {
                    page = x.page;
                    onLoad?.Invoke(x);
                });
            return page;
        }

        /// <summary>
        /// 返り値でPageを返すPush()の拡張メソッド
        /// </summary>
        public static async UniTask<Page> PushPage(this PageContainer container,
            string resourceKey, bool playAnimation, bool stack = true,
            string pageId = null, bool loadAsync = true,
            Action<(string pageId, Page page)> onLoad = null) {

            if (container == null) throw new ArgumentNullException(nameof(container));

            // Page読み込み
            Page page = null;
            await container.Push<Page>(resourceKey, playAnimation, stack, pageId, loadAsync,
                x => {
                    page = x.page;
                    onLoad?.Invoke(x);
                });
            return page;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Pageの取得)

        /// <summary>
        /// アクティブなModalを取得する
        /// </summary>
        public static Page GetActivePage(this PageContainer container) {
            if (container.Pages.Count == 0) return null;

            var pageId = container.OrderedPagesIds[container.Pages.Count - 1];
            return container.Pages[pageId];
        }

        /// <summary>
        /// アクティブなModalを取得する
        /// </summary>
        public static bool TryGetActivePage(this PageContainer container, out Page page) {
            page = container.GetActivePage();
            return page != null;
        }

        /// <summary>
        /// キーを指定してModalを取得する
        /// </summary>
        public static bool TryGetPage(this PageContainer container, string resourceKey, out Page page) {
            return container.Pages.TryGetValue(resourceKey, out page);
        }

        /// <summary>
        /// キーを指定してModalを取得する
        /// </summary>
        public static bool TryGetPage<TPage>(this PageContainer container, string resourceKey, out TPage page) 
            where TPage : Page{
                        
            if(container.Pages.TryGetValue(resourceKey, out var tmp) && tmp is TPage) {
                page = (TPage)tmp;
                return true;
            }

            page = default;
            return false;
        }


        /// ----------------------------------------------------------------------------
        // Public Method ()

        public static IDisposable SetUninteractable(this PageContainer container) {
            container.Interactable = false;

            return Disposable.Create(() => {
                container.Interactable = true;
            });
        }

        /// <summary>
        /// リフレクションでコンテナキー変数を書き換える拡張メソッド
        /// </summary>
        public static void SetContainerKey(this PageContainer container, string key) {
            var fieldInfo = container.GetType()
                .GetField("_name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null) fieldInfo.SetValue(container, key);
        }
    }
}
