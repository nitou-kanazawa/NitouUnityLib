using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// [参考]
//  _: Stack<T> クラス https://learn.microsoft.com/ja-jp/dotnet/api/system.collections.generic.stack-1?view=net-8.0

namespace nitou {

	/// <summary>
	/// <see cref="Stack{T}"/>に最大要素数の制限をかけたコレクション
	/// </summary>
    public class FixedSizeStack<T> : IEnumerable<T>, IReadOnlyCollection<T> {

        private readonly int _maxSize;
        private readonly LinkedList<T> _list;

		/// <summary>
		/// 現在の要素数
		/// </summary>
        public int Count => _list.Count;

		/// <summary>
		/// 空かどうか
		/// </summary>
        public bool IsEmpty => _list.Count == 0;


        /// ----------------------------------------------------------------------------
        // Public Method

		/// <summary>
		/// コンストラクタ 
		/// </summary>
        public FixedSizeStack(int maxSize) {
            if (maxSize <= 0) {
                throw new ArgumentException("maxSize must be greater than zero.", nameof(maxSize));
            }
            _maxSize = maxSize;
            _list = new LinkedList<T>();
        }

		/// <summary>
		/// 指定した要素が含まれているかどうかを確認する
		/// </summary>
		public bool Contains(T item) {
			return _list.Contains(item);
		}

		/// <summary>
		/// スタックを配列に変換する
		/// </summary>
		public T[] ToArray() {
			return _list.ToArray();
		}

		public IEnumerator<T> GetEnumerator() {
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}


		/// ----------------------------------------------------------------------------
		#region Public Method (要素の追加/取得)

		/// <summary>
		/// 先頭に要素を追加する
		/// </summary>
		public void Push(T item) {
            if (_list.Count == _maxSize) {
                // Remove the oldest item (bottom of the stack)
                _list.RemoveLast();
            }
            // Add the new item to the top of the stack
            _list.AddFirst(item);
        }

        /// <summary>
        /// 先頭から要素を取り出す
        /// </summary>
        public T Pop() {
            if (_list.Count == 0) {
                throw new InvalidOperationException("The stack is empty.");
            }
            // Remove the top item
            var value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }

        /// <summary>
        /// 要素を取り出さずに確認する
        /// </summary>
        public T Peek() {
            if (_list.Count == 0) {
                throw new InvalidOperationException("The stack is empty.");
            }
            // Return the top item without removing it
            return _list.First.Value;
        }

		/// <summary>
		/// 先頭の要素を取り出さずに確認する。成功した場合はtrueを返し、要素をoutパラメーターに格納する
		/// </summary>
		public bool TryPeek(out T result) {
			if (_list.Count == 0) {
				result = default;
				return false;
			}
			result = _list.First.Value;
			return true;
		}

		/// <summary>
		/// 先頭から要素を取り出す。成功した場合はtrueを返し、要素をoutパラメーターに格納する
		/// </summary>
		public bool TryPop(out T result) {
			if (_list.Count == 0) {
				result = default;
				return false;
			}
			result = _list.First.Value;
			_list.RemoveFirst();
			return true;
		}

		/// <summary>
		/// 全ての要素を削除する
		/// </summary>
		public void Clear() {
			_list.Clear();
		}
		#endregion

    }
}

