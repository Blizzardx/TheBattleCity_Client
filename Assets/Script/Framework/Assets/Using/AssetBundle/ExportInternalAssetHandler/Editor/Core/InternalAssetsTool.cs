using Assets.Script.AssetBundle.InternalAssetHandler.Editor;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.AssetBundle.InternalAssetHandler
{
    class InternalAssetsExportTool
    {       
        private Dictionary<System.Type, IInternalAssetsExporter> m_HandlerMap;
        private string m_strOutputPath;

        public InternalAssetsExportTool()
        {
            m_HandlerMap = new Dictionary<System.Type, IInternalAssetsExporter>();
            m_HandlerMap.Add(typeof(Material), new AssetExporter_Material());
            //m_HandlerMap.Add(typeof(Mesh), new AssetExporter_Mesh());
            //m_HandlerMap.Add(typeof(Shader), new AssetExporter_Shader());
            //m_HandlerMap.Add(typeof(Sprite), new AssetExporter_Sprite());
            //m_HandlerMap.Add(typeof(Texture2D), new AssetExporter_Texture2D());
        }
        public void ExportInternalAsset(string outputPath)
        {
            m_strOutputPath = outputPath;
            ExportInternalAssets();
        }
        private void ExportInternalAssets()
        {
            Object[] UnityAssets1 = AssetDatabase.LoadAllAssetsAtPath("Resources/unity_builtin_extra");
            Object[] UnityAssets2 = AssetDatabase.LoadAllAssetsAtPath("Library/unity default resources");
            List<Object> allInternalAssets = new List<Object>();
            allInternalAssets.AddRange(UnityAssets1);
            allInternalAssets.AddRange(UnityAssets2);

            //HashSet<string> typeList = new HashSet<string>();

            //foreach (var asset in allInternalAssets)
            //{
            //    if (!typeList.Contains(asset.GetType().ToString()))
            //    {
            //        typeList.Add(asset.GetType().ToString());
            //    }
            //}
            //foreach (var type in typeList)
            //{
            //    Debug.Log(type);
            //}
            foreach(var asset in allInternalAssets)
            {
                IInternalAssetsExporter handler = null;
                m_HandlerMap.TryGetValue(asset.GetType(), out handler);
                if(null != handler)
                {
                    var realPath = Application.dataPath + "/" + m_strOutputPath + "/" + asset.name;
                    AssetInfo info = new AssetInfo(realPath);
                    EnsureFolderByFilePath(info.GetFullPath());
                    handler.SaveAssets(asset,info.GetRelativePath());
                }
                else
                {
                    Debug.Log("Can't load asset hanlder by type " + asset.GetType());
                }
                //break;
            }
        }
        private void EnsureFolderByFilePath(string filepath)
        {
            filepath = filepath.Replace('\\', '/');
            filepath = filepath.Substring(0, filepath.LastIndexOf('/'));

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
        }
    }
}
