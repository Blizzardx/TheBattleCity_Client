using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.AssetBundle.InternalAssetHandler.Editor
{
    interface IInternalAssetsExporter
    {
        Type GetHanlderAssetType();
        void SaveAssets(UnityEngine.Object assets,string savePath);
    }
}
