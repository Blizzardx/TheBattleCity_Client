using Assets.Script.AssetBundle.InternalAssetHandler.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.AssetBundle.InternalAssetHandler
{
    class AssetExporter_Sprite : IInternalAssetsExporter
    {
        public Type GetHanlderAssetType()
        {
            return typeof(Sprite);
        }

        public void SaveAssets(UnityEngine.Object asset, string savePath)
        {
            Sprite tmp = Sprite.Instantiate(asset as Sprite);

            // create asset...
            AssetDatabase.CreateAsset(tmp, savePath + ".sprite");
        }
    }
}
