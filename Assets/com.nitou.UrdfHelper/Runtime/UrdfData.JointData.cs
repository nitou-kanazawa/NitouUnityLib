using UnityEngine;

namespace nitou.UrdfHelper {
    public partial class UrdfData {

        public class JointData{

			public string Name;
			public string Type;
			public Vector3 OriginXYZ;
			public Vector3 OriginRPY;
			public Vector3 Axis;
			public string Parent;
			public string Child;
			public float Velocity;
			public float LowerLimit;
			public float UpperLimit;

			public JointData(string name, string type) {
				Name = name;
				Type = type;
				OriginXYZ = Vector3.zero;
				OriginRPY = Vector3.zero;
				Axis = new Vector3(1f, 0f, 0f);
				Parent = string.Empty;
				Child = string.Empty;
				Velocity = 0f;
				LowerLimit = 0f;
				UpperLimit = 0f;
			}
		}
    }
}