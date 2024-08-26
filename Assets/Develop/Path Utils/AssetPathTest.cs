using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nitou;

namespace Project
{
    public class AssetPathTest : MonoBehaviour{

        
        void Start()
        {

            // AssetPathクラスの使用例
            var relativePath = "Textures/MyTexture.png";
            var assetPath = AssetPath.FromRelativePath(relativePath);
            Debug_.Log(relativePath);
            Debug_.Log(assetPath.ToAssetDatabasePath());
            Debug_.Log(assetPath.ToAbsolutePath());
            Debug_.Log(assetPath.ToSystemIOPath());


            // System.IO用のパスに変換
            string systemPath = assetPath.ToSystemIOPath();

            // 拡張子を変更
            var newAssetPath = assetPath.ChangeExtension(".jpg");

            // パスの検証
            bool isValid = assetPath.IsValidAssetPath();

            // アセットが存在するか確認
            bool exists = assetPath.Exists();


        }



    }
}
