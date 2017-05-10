using Assets.Script.AssetBundle.InternalAssetHandler.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.AssetBundle.InternalAssetHandler
{
    class AssetExporter_Mesh : IInternalAssetsExporter
    {
        public Type GetHanlderAssetType()
        {
            return typeof(Mesh);
        }

        public void SaveAssets(UnityEngine.Object asset, string savePath)
        {
            Mesh tmp = Mesh.Instantiate(asset as Mesh);

            // create asset...
            AssetDatabase.CreateAsset(tmp, savePath + ".fbx");
        }
    }
}
