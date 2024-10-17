using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace nitou
{
    /// <summary>
    /// 
    /// </summary>
    public static class RaycasterExtensions {

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
    }
}
