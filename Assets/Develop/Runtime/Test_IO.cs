using UnityEngine;

using nitou;
using System.IO;

namespace Project
{
    public class Test_IO : MonoBehaviour
    {


        private void Start() {


            var dataPath = Application.persistentDataPath;
            Debug_.Log(dataPath);


            var names = new string[] { "Scripts", "Prefabs", "Editor"};

            foreach(var name in names)
                Directory.CreateDirectory(Path.Combine(dataPath, name));

        }
    }
}
