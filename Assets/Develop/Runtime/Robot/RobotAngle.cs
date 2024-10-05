using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Robots {

    /// <summary>
    /// ロボットのエンドポイントの位置姿勢
    /// </summary>
    public class RobotPosition {

        public Vector3 Position { get; private set; }
        public Quaternion Orientation { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RobotPosition(Vector3 position, Quaternion orientation) {
            Position = position;
            Orientation = orientation;
        }

        /// <summary>
        /// セッタ
        /// </summary>
        public void UpdatePosition(Vector3 position, Quaternion orientation) {
            Position = position;
            Orientation = orientation;
        }

        public override string ToString() {
            return $"Position: {Position}, Orientation: {Orientation.eulerAngles}";
        }
    }


    /// <summary>
    /// ロボットの関節角度
    /// </summary>
    public class RobotAngle {
        
        private readonly float[] _jointAngles;

        public int Dof => _jointAngles.Length;

        public RobotAngle(int dof) {
            if (dof < 6 || dof > 7) {
                throw new System.ArgumentException("DOF must be 6 or 7");
            }

            _jointAngles = new float[dof];
        }

        public void SetJointAngle(int jointIndex, float angle) {
            if (jointIndex < 0 || jointIndex >= _jointAngles.Length) {
                throw new System.IndexOutOfRangeException("Invalid joint index");
            }
            _jointAngles[jointIndex] = angle;
        }

        public float GetJointAngle(int jointIndex) {
            if (jointIndex < 0 || jointIndex >= _jointAngles.Length) {
                throw new System.IndexOutOfRangeException("Invalid joint index");
            }
            return _jointAngles[jointIndex];
        }

        public float[] GetAllJointAngles() {
            return (float[])_jointAngles.Clone();
        }

        // ToString メソッドのオーバーライド
        public override string ToString() {
            return $"JointAngles: [{string.Join(", ", _jointAngles)}]";
        }
    }

}
