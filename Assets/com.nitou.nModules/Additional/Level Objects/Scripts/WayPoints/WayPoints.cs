using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.LevelObjects{

    /// <summary>
    /// WayPointを管理するコンポーネント
    /// </summary>
    public class WayPoints : MonoBehaviour{

        public enum PointType {
            Grobal,
            Local,
        }

        [SerializeField] List<WayPoint> _points = new();

        /// <summary>
        /// 
        /// </summary>
        public List<WayPoint> Points => _points;


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            //Gizmos_.DrawLines(_points.Select(x => x.position).ToList(), Colors.GreenYellow);
        }
#endif

    }
}
