using UnityEngine;

namespace nitou.UrdfHelper {
    public partial class UrdfData {


        public class LinkData {

			public string Name;
			public Geometry Geometry;
			public string Material;
			public Vector3 XYZ;
			public Vector3 RPY;

			public LinkData(string name) {
				Name = name;
				Geometry = new Empty();
				Material = string.Empty;
				XYZ = Vector3.zero;
				RPY = Vector3.zero;
			}
		}

    }
}
