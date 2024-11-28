using UnityEngine;

namespace nitou.UrdfHelper {
    public partial class UrdfData {

        public enum GeometryType { 
            Box, 
            Cylinder, 
            Sphere, 
            Mesh, 
            Empty 
        };


        public abstract class Geometry {            
            public GeometryType Type;
            
            public Geometry(GeometryType type) {
                Type = type;
            }

            public abstract string GetDataInfo();
        }


        public class Box : Geometry {
            public Vector3 Size;

            public Box(Vector3 size) : base(GeometryType.Box){
                Size = size;
            }

            public override string GetDataInfo() {
                return $"Size: {Size.ToString()}";
            }
        }


		public class Cylinder : Geometry {
			public float Length;
			public float Radius;
			
			public Cylinder(float length, float radius) : base(GeometryType.Cylinder) {
				Length = length;
				Radius = radius;
			}
			
			public override string GetDataInfo() {
				return $"Length: {Length.ToString()}\n" + $"Radius: {Radius.ToString()}";
			}
		}


		public class Sphere : Geometry {
			public float Radius;
			
			public Sphere(float radius) : base(GeometryType.Sphere) {
				Radius = radius;
			}
			
			public override string GetDataInfo() {
				return $"Radius: {Radius.ToString()}";
			}
		}


		public class Mesh : Geometry {			
			public string Path;
			public Vector3 Scale;
			
			public Mesh(string path, Vector3 scale) : base(GeometryType.Mesh) {
				Path = path;
				Scale = scale;
			}
			
			public override string GetDataInfo() {
				return $"Path: {Path}\n" + $"Scale: {Scale.ToString("F4")}";
			}
		}


		public class Empty : Geometry {
			
			public Empty() : base(GeometryType.Empty) {

			}
			
			public override string GetDataInfo() {
				return "No Geometry";
			}
		}
	}
}
