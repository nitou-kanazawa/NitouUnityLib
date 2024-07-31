using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou{

    /// <summary>
    /// 範囲を<see cref="int"/>型で表す構造体
    /// </summary>
    public class RangeInt : IRangeValue<int>{

        [SerializeField] int _min;
        [SerializeField] int _max;

        public int Min {
            get => _min;
            set => _min = Mathf.Min(_min, value);
        }

        public int Max {
            get => _max;
            set => _max = Mathf.Max(_max, value);
        }

        /// <summary>
        /// 中央値
        /// </summary>
        public int Mid => _min + (_max - _min) / 2;

        /// <summary>
        /// 範囲の長さ
        /// </summary>
        public int Length => Mathf.Abs(_max - _min);

        /// <summary>
        /// 範囲内のランダムな値
        /// </summary>
        public int Random => _min < _max ? UnityEngine.Random.Range(_min, _max+1) : _min;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RangeInt(int min, int max) {
            _min = min;
            _max = Mathf.Max(min, max);
        }

        /// <summary>
        /// 値が範囲内か調べる
        /// </summary>
        public bool Contains(int value) {
            return value.IsInRange(_min, _max);
        }

        public float Clamp(int value) {
            return Mathf.Clamp(value, _min, _max);
        }

    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RangeInt))]
    public class RangeIntEditor : RangeValueEditor {

        protected override void ValidateValue(SerializedProperty minProperty, SerializedProperty maxProperty) {
            // 小さい数値を基準にして、大きい数値が小さい数値より小さくならないようにしてみよう。
            if (maxProperty.floatValue < minProperty.floatValue) {
                minProperty.floatValue = maxProperty.floatValue;
            }
        }

    }
#endif

}
