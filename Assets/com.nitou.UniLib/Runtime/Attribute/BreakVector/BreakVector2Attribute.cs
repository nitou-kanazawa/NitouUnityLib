using UnityEngine;

namespace nitou {

    /// <summary>
    /// Vector2の各成分 (x,y) を独立してインスペクタ表示させる属性
    /// </summary>
    [System.AttributeUsage(
        System.AttributeTargets.Field, 
        AllowMultiple = false, 
        Inherited = true
    )]
    public class BreakVector2Attribute : PropertyAttribute {

        public string xLabel, yLabel;

        public BreakVector2Attribute(string xLabel, string yLabel) {
            this.xLabel = xLabel;
            this.yLabel = yLabel;
        }
    }
}
