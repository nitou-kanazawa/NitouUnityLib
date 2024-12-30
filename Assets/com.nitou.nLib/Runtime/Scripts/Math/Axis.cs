using UnityEngine;

namespace nitou {

    public enum Axis {
        X,
        Y,
        Z,
    }

    public static class AxsisExtensions {

        public static Vector3 ToVector3(this Axis axis) {
            return axis switch {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => throw new System.NotImplementedException()
            };
        }
    }

}
