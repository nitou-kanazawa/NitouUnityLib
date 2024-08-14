using UnityEngine;

namespace nitou.HitSystem{

    /// <summary>
    /// ヒット情報を格納するデータクラス
    /// </summary>
    public class HitData{

        /// <summary>
        /// コンタクト座標
        /// </summary>
        public readonly Vector3 position;
        
        /// <summary>
        /// 方向
        /// </summary>
        public readonly Vector3 direction;

        // ヒットストップ
        public readonly bool hitStop = false;
        public readonly float stopDuration = 0f;


        // 内部処理用
        private readonly float _time;

        /// <summary>
        /// 経過時間
        /// </summary>
        public float ElaposedTime => Time.time - _time;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HitData(Vector3 position, Vector3 direction) {
            this.position = position;
            this.direction = direction.normalized;

            // 時間を記録
            _time = Time.time;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HitData(Vector3 position, Vector3 direction, bool hitStop) {
            // 
            this.position = position;
            this.direction = direction.normalized;

            // ヒットストップ
            this.hitStop = hitStop;
            this.stopDuration = Mathf.Max(0, 0.2f);

            // 時間を記録
            _time = Time.time;
        }

        
        /// ----------------------------------------------------------------------------

        /// <summary>
        /// ギズモを表示する
        /// </summary>
        public void DrawGizmo(Color color) {
            Gizmos_.DrawSphere(position, 0.1f, color);
            Gizmos_.DrawRayArrow(position, direction, color);
        }
    }
}
