using Assets.Script.AssetBundle.InternalAssetHandler.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.AssetBundle.InternalAssetHandler
{
    class AssetExporter_Texture2D : IInternalAssetsExporter
    {
        public Type GetHanlderAssetType()
        {
            return typeof(Texture2D);
        }

        public void SaveAssets(UnityEngine.Object asset, string savePath)
        {            
            //Texture2D tmp = Texture.Instantiate(asset as Texture2D);
            //var res = tmp.EncodeToPNG();
            //System.IO.File.WriteAllBytes(savePath + ".png", res);
            // create asset...
            //AssetDatabase.CreateAsset(asset, savePath + ".png");
            
        }
    }
}
