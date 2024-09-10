using System;

namespace nitou{

    public static class EnumExtensions {

        public static bool IsAnyOf<TEnum>(this TEnum value, params TEnum[] values) 
            where TEnum : Enum {
            
            foreach (var val in values) {
                if (value.Equals(val)) {
                    return true;
                }
            }
            return false;
        }

    }
}
