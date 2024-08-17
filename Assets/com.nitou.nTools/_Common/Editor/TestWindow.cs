using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace nitou.Tools{

    [InitializeOnLoad]
    public class TestWindow : EditorWindow{

        [SerializeField] Material _material2;

        static TestWindow() {

            //Debug.Log($"{name} constructor");

        }
        

    }
}
