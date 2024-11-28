using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace nitou.UrdfHelper {

    public partial class UrdfData {

        public string Path;
        public string Folder;
        public bool Failed;
        public string Name;
        public readonly List<MaterialData> Materials;
        public readonly List<JointData> Joints;
        public readonly List<LinkData> Links;


        /// <summary>
        /// コンストラクタ．
        /// </summary>
        private UrdfData() {

        }


        public static UrdfData Create(string path){

            


            return new UrdfData();
        }
    }
}
