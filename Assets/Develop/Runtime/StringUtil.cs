using UnityEngine;

namespace Project
{
    public static class StringUtil
    {

        public static float ParseFloat(string value) {
            if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var parsed)) {
                return parsed;
            } else {
                Debug.Log("Error parsing " + value + "!");
                return 0f;
            }
        }

    }
}
