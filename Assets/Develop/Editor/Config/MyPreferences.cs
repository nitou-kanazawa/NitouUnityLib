#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Project.EditorScripts {

    [FilePath("MyPreferences.asset", FilePathAttribute.Location.PreferencesFolder)]
    public class MyPreferences : ScriptableSingleton<MyPreferences>{


        public void Save() => base.Save(saveAsText: true);
    }
}
#endif