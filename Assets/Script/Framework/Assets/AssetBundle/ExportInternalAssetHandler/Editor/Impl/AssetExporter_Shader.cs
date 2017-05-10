using Assets.Script.AssetBundle.InternalAssetHandler.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.AssetBundle.InternalAssetHandler
{
    class AssetExporter_Shader : IInternalAssetsExporter
    {
        public Type GetHanlderAssetType()
        {
            return typeof(Shader);
        }

        public void SaveAssets(UnityEngine.Object asset, string savePath)
        {
            Shader tmp = Shader.Instantiate(asset as Shader);

            // create asset...
            AssetDatabase.CreateAsset(tmp, savePath + ".shader");
        }
    }
}
