using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using nitou.DesignPattern.Pooling;

namespace nitou
{
    /// <summary>
    /// 
    /// </summary>
    public static class RaycasterExtensions {

        /// <summary>
        /// Raycastの拡張メソッド．
        /// （毎回リストを生成するため、基本的には引数にリストを受け取る通常版を使用する．）
        /// </summary>
        public static List<RaycastResult> Raycast(this BaseRaycaster self, PointerEventData pointerEventData) {
            List<RaycastResult> results = new();
            self.Raycast(pointerEventData, results);
            return results;
        }


        /// <summary>
        /// GraphicRaycasterのリストからDragコンポーネントを取得します。
        /// </summary>
        public static T GetDragAtPosition<T>(this IEnumerable<BaseRaycaster> raycasters, PointerEventData pointerEventData) {
            return raycasters
                .Where(raycaster => raycaster != null)
                .SelectMany(raycaster => {
                    var results = new List<RaycastResult>();
                    raycaster.Raycast(pointerEventData, results);
                    return results;
                })
                .Select(result => result.gameObject.GetComponentInParent<T>())
                .FirstOrDefault(drag => drag != null);
        }


        /// <summary>
        /// 指定したスクリーン座標にUIがあるかどうか調べる
        /// </summary>
        public static bool OverlapUI(this IEnumerable<GraphicRaycaster> raycasters, PointerEventData pointerEventData) {

            var results = ListPool<RaycastResult>.New();
            bool isOverlap = false;

            try {
                // 各Raycasterで重なり判定を行う
                foreach (var raycaster in raycasters.WithoutNull()) {
                    raycaster.Raycast(pointerEventData, results);
                    if (results.Count > 0) {
                        isOverlap = true;
                        break;
                    }
                }
            } finally {
                results.Free();
            } 
            return isOverlap;
        }

    }
}
