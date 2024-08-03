using UnityEngine;

namespace nitou {

    [System.AttributeUsage(
        System.AttributeTargets.Field, 
        AllowMultiple = false, 
        Inherited = true
    )]
    public class BreakVector3Attribute : PropertyAttribute {

        public string xLabel, yLabel, zLabel;

        public BreakVector3Attribute(string xLabel, string yLabel, string zLabel) {
            this.xLabel = xLabel;
            this.yLabel = yLabel;
            this.zLabel = zLabel;
        }
    }
}
