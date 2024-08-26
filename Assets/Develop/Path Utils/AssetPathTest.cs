using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nitou;

namespace Project
{
    public class AssetPathTest : MonoBehaviour{

        [SerializeField] AssetPath path;

        void Start()
        {

            // AssetPathクラスの使用例
            var relativePath = "Textures/MyTexture.png";
            var assetPath = AssetPath.FromRelativePath(relativePath);
            Debug_.Log($"relativePath : {relativePath}");
            Debug_.Log($"AssetDatabasePath : {assetPath.ToAssetDatabasePath()}");
            Debug_.Log($"AbsolutePath : {assetPath.ToAbsolutePath()}");
            Debug_.Log($"To String : {assetPath.ToString()}");

            Debug_.Log("--------------------");

            // 拡張子を変更
            var newAssetPath = assetPath.ChangeExtension(".jpg");
            Debug_.Log($"relativePath : {newAssetPath.ToRelativePath()}");
            Debug_.Log($"AssetDatabasePath : {newAssetPath.ToAssetDatabasePath()}");


        }



    }
}
