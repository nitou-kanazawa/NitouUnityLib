using System.Collections;
using System.Collections.Generic;

namespace UnityEngine{


    public static class EventExtensions{


        /// ----------------------------------------------------------------------------
        // EventType

        public static bool IsRepaintOrLayout(this EventType self) {
            return self == EventType.Repaint || self == EventType.Layout; 
        }

    }
}
