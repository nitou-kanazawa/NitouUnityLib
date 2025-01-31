﻿using System;
using System.Collections.Generic;

namespace nitou.DesignPattern.Pooling {

    /// <summary>
    /// <see cref="Dictionary{TKey, TValue}"/>を効率的に再利用するための静的クラス．
    /// </summary>
    public static class DictionaryPool<TKey, TValue> {

        private static readonly object @lock = new();
        private static readonly Stack<Dictionary<TKey, TValue>> free = new();
        private static readonly HashSet<Dictionary<TKey, TValue>> busy = new();

        // Do not allow an IDictionary parameter here to avoid allocation on foreach
        public static Dictionary<TKey, TValue> New(Dictionary<TKey, TValue> source = null) {
            lock (@lock) {
                if (free.Count == 0) {
                    free.Push(new Dictionary<TKey, TValue>());
                }

                var dictionary = free.Pop();

                busy.Add(dictionary);

                if (source != null) {
                    foreach (var kvp in source) {
                        dictionary.Add(kvp.Key, kvp.Value);
                    }
                }

                return dictionary;
            }
        }

        public static void Free(Dictionary<TKey, TValue> dictionary) {
            lock (@lock) {
                if (!busy.Contains(dictionary)) {
                    throw new ArgumentException("The dictionary to free is not in use by the pool.", nameof(dictionary));
                }

                dictionary.Clear();

                busy.Remove(dictionary);

                free.Push(dictionary);
            }
        }
    }
}
