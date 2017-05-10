using Assets.Script.AssetBundle.InternalAssetHandler.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.AssetBundle.InternalAssetHandler
{
    class AssetExporter_Material : IInternalAssetsExporter
    {
        public Type GetHanlderAssetType()
        {
            return typeof(Material);
        }

        public void SaveAssets(UnityEngine.Object asset, string savePath)
        {
            Material tmp = Material.Instantiate(asset as Material);

            // create asset...
            AssetDatabase.CreateAsset(tmp, savePath + ".mat");
        }
    }
}
