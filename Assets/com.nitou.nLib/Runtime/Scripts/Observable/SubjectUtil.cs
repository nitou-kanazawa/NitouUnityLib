using UniRx;
using UnityEngine;

namespace nitou {
    /// <summary>
    /// <see cref="Subject{T}"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class SubjectUtil {

        /// <summary>
        /// 
        /// </summary>
        public static void DisposeSubject<T>(ref Subject<T> subject) {

            if (subject != null) {

                try {
                    subject.OnCompleted();
                } finally {
                    subject.Dispose();
                    subject = null;
                }
            }

        }

    }
}
