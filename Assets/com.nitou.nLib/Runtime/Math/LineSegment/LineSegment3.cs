using System.Linq;
using UnityEngine;

namespace nitou {

    /// <summary>
    /// ������\���\����
    /// </summary>
    [System.Serializable]
    public struct LineSegment3 {

        public Vector3 start;
        public Vector3 end;


        /// ----------------------------------------------------------------------------
        // Property

        /// <summary>
        /// ���_
        /// </summary>
        public Vector3 Center => (start + end) * 0.5f;

        /// <summary>
        /// �����x�N�g��
        /// </summary>
        public Vector3 Vector => end - start;


        /// ----------------------------------------------------------------------------
        // Public Method (��{���\�b�h)

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LineSegment3(Vector3 start, Vector3 end) {
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// ����
        /// </summary>
        public LineSegment3 Clone() {
            return new LineSegment3(start, end);
        }

        /// <summary>
        /// �����x�N�g��
        /// </summary>
        public float Distance() {
            return Vector3.Distance(start, end);
        }

        /// <summary>
        /// �p�����[�^�i0~1�j���w�肵�Đ�����̓_���擾����
        /// </summary>
        public Vector3 GetPoint(float t) {
            return Vector3.Lerp(start, end, t);
        }

        /// <summary>
        /// ���������w�肵�āA������̓_����擾����
        /// </summary>
        public Vector3[] GetPoints(int num) {
            if (num <= 0) throw new System.InvalidOperationException("The number of divisions must be a positive integer greater than zero.");

            return EnumerableUtil.Linspace(start, end, num).ToArray();
        }

        /// <summary>
        /// �Q�̐��������s�����肷��
        /// </summary>
        public bool IsParallel(LineSegment3 other, float tolerance = 0.01f) {
            var cross = Vector3.Cross(this.Vector.normalized, other.Vector.normalized);
            return cross.magnitude <= tolerance;
        }

        /// <summary>
        /// Checks if a point is on the line segment.
        /// </summary>
        public bool ContainsPoint(Vector3 point, float tolerance = 0.01f) {
            var toPoint = point - start;
            
            // ������������@���Ƃ���ʂɎˉe
            var projection = Vector3.Project(toPoint, this.Vector);
            
            // 
            return Vector3.Distance(projection, toPoint) <= tolerance &&
                   Vector3.Dot(projection, this.Vector) >= 0 &&
                   projection.magnitude <= this.Vector.magnitude;
        }

        /// <summary>
        /// �ŋߖT�_���v�Z����
        /// </summary>
        public Vector3 GetNearestPoint(Vector3 point) {
            var lineVector = Vector;
            var lineToPoint = point - start;
            var t = Vector3.Dot(lineToPoint, lineVector) / Vector3.Dot(lineVector, lineVector);
            t = Mathf.Clamp01(t);
            return GetPoint(t);
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// Translates the line segment by a given vector.
        /// </summary>
        public LineSegment3 Translate(Vector3 translation) {
            return new LineSegment3(start + translation, end + translation);
        }

        /// <summary>
        /// Rotates the line segment around a pivot point by a given rotation.
        /// </summary>
        public LineSegment3 Rotate(Quaternion rotation) {
            var rotatedStart = rotation * start;
            var rotatedEnd = rotation * end;
            return new LineSegment3(rotatedStart, rotatedEnd);
        }

        /// <summary>
        /// Rotates the line segment around a pivot point by a given rotation.
        /// </summary>
        public LineSegment3 Rotate(Quaternion rotation, Vector3 pivot) {
            var rotatedStart = rotation * (start - pivot) + pivot;
            var rotatedEnd = rotation * (end - pivot) + pivot;
            return new LineSegment3(rotatedStart, rotatedEnd);
        }

        /// <summary>
        /// Scales the line segment from its center.
        /// </summary>
        public LineSegment3 Scale(float scale) {
            var center = this.Center;
            var scaledVector = Vector * scale / 2f;
            return new LineSegment3(center - scaledVector, center + scaledVector);
        }

        /// <summary>
        /// Transforms the line segment by a given Transform.
        /// </summary>
        public LineSegment3 TransformBy(Transform transform) {
            var transformedStart = transform.TransformPoint(start);
            var transformedEnd = transform.TransformPoint(end);
            return new LineSegment3(transformedStart, transformedEnd);
        }
    }


    /// <summary>
    /// <see cref="LineSegment3"/> �Ɋ֘A�����g�����\�b�h�W
    /// </summary>
    public static class LineSegment3Extensions {

        /// ----------------------------------------------------------------------------
        #region Transform

        /// <summary>
        /// Converts a Transform's position and forward direction into a line segment.
        /// </summary>
        public static LineSegment3 ToLineSegment(this Transform transform, float length) {
            var start = transform.position;
            var end = transform.position + transform.forward * length;
            return new LineSegment3(start, end);
        }

        /// <summary>
        /// Transforms the line segment by a given Transform.
        /// </summary>
        public static LineSegment3 TransformLineSegment(this Transform transform, LineSegment3 line) {
            return line.TransformBy(transform);
        }
        #endregion
    }

}