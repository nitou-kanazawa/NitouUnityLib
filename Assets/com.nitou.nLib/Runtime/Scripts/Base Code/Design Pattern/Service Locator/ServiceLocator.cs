using System;
using System.Collections.Generic;

// [参考]
//  qiita: Unityでサービスロケーター(ServiceLocator)を活用する https://qiita.com/ozaki_shinya/items/9eb0f827caa6a4108888

namespace nitou.DesignPattern {

    /// <summary>
    /// シンプルな実装のサービスロケータ
    /// </summary>
    public static class ServiceLocator {

        // 単一インスタンス用
        private static Dictionary<Type, object> _instanceDict = new ();

        // 都度インスタンス生成用
        private static Dictionary<Type, Type> _typeDict = new　();

        /// <summary>
        /// 単一インスタンスを登録する
        /// 呼び直すと上書き登録する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="instance">インスタンス</param>
        public static void Register<T>(T instance) where T : class {
            _instanceDict[typeof(T)] = instance;
        }

        /// <summary>
        /// 型を登録する
        /// このメソッドで登録するとResolveしたときに都度インスタンス生成する
        /// </summary>
        /// <typeparam name="TContract">抽象型</typeparam>
        /// <typeparam name="TConcrete">具現型</typeparam>
        public static void Register<TContract, TConcrete>() where TContract : class {
            _typeDict[typeof(TContract)] = typeof(TConcrete);
        }

        /// <summary>
        /// 型を指定して登録されているインスタンスを取得する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <returns>インスタンス</returns>
        public static T Resolve<T>() where T : class {
            T instance = default;

            Type type = typeof(T);

            if (_instanceDict.ContainsKey(type)) {
                // 事前に生成された単一インスタンスを返す
                instance = _instanceDict[type] as T;
                return instance;
            }

            if (_typeDict.ContainsKey(type)) {
                // インスタンスを生成して返す
                instance = Activator.CreateInstance(_typeDict[type]) as T;
                return instance;
            }

            if (instance == null) {
                Debug_.LogWarning($"Locator: {typeof(T).Name} not found.");
            }
            return instance;
        }
    }
}
