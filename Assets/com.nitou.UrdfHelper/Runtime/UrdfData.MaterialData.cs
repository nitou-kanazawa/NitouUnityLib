using System.Xml.Linq;
using UnityEngine;

// [REF]
//  ROS Doc: Create your own urdf file https://wiki.ros.org/urdf/Tutorials/Create%20your%20own%20urdf%20file

namespace nitou.UrdfHelper {
    public partial class UrdfData {

        public class MaterialData {

            public string Name;
            public Color Color;
            public string Texture;

            public MaterialData(string name) {
                Name = name;
                Color = Color.white;
                Texture = string.Empty;
            }


            /// ----------------------------------------------------------------------------
            #region Static Method (データ変換)

            /// <summary>
            /// 識別用の文字列．
            /// </summary>
            public static readonly string NAME_KEY = "material";

            /// <summary>
            /// シリアライズ用クラスからXML要素に変換する．
            /// </summary>
            public static XElement ToXElement(MaterialData material) {
                var xMaterial = new XElement(NAME_KEY, 
                    new XAttribute("name", material.Name),
                    material.Color.ToXElement(),
                    new XElement("texture", material.Texture)
                );
                return xMaterial;
            }

            /// <summary>
            /// XML要素からシリアライズ用クラスに変換する．
            /// </summary>
            public static MaterialData FromXElement(XElement xMaterial) {

                var name = xMaterial.Attribute("name")?.Value ?? "Unnamed";

                var xColor = xMaterial.Element("color");
                var color = xColor.ReadColor(Color.white);

                var texture = xMaterial.Element("texture")?.Value ?? string.Empty;

                return new MaterialData(name) {
                    Color = color,
                    Texture = texture
                };
            }
            #endregion

        }
    }


    public static class XmlUtils {

        /// <summary>
        /// XML要素からColorを読み取る.
        /// </summary>
        /// <param name="xElement">色情報を持つXML要素</param>
        /// <param name="defaultColor">指定された属性がない場合のデフォルト値</param>
        /// <returns>XML要素から読み取ったColor</returns>
        public static Color ReadColor(this XElement xElement, Color defaultColor) {
            if (xElement == null)
                return defaultColor;

            float r = float.Parse(xElement.Attribute("r")?.Value ?? defaultColor.r.ToString());
            float g = float.Parse(xElement.Attribute("g")?.Value ?? defaultColor.g.ToString());
            float b = float.Parse(xElement.Attribute("b")?.Value ?? defaultColor.b.ToString());
            float a = float.Parse(xElement.Attribute("a")?.Value ?? defaultColor.a.ToString());

            return new Color(r, g, b, a);
        }

        /// <summary>
        /// ColorをXML要素として出力する
        /// </summary>
        /// <param name="color">Colorデータ</param>
        /// <returns>Color情報を含むXElement</returns>
        public static XElement ToXElement(this Color color) {
            return new XElement("color",
                new XAttribute("r", color.r),
                new XAttribute("g", color.g),
                new XAttribute("b", color.b),
                new XAttribute("a", color.a)
            );
        }
    }



}
