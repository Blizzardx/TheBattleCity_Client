using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.Framework.Assets.NewAssetTest.Editor
{
    class AssetbundlePacker
    {
        private class PackerInfo
        {
            public PackerInfo()
            {
                
            }
            public PackerInfo(string path, string bundleName)
            {
                this.path = path;
                this.bundleName = bundleName;
            }
            public string path;
            public string bundleName;
        }
        private string[] m_IgnoreList = new string[] {".meta",".svn",".cs"};
        private string m_strUGUIAtlasPath;
        private string m_strNGUIAtlasPath;
        private string m_strDependentAssetRootPath;
        private string m_strIndependentAssetPath;
        private string m_strOutputPath;

        private TextureImporterType     m_TextureType;
        private TextureImporterFormat   m_TextureFormate;

        public void BuildAssetBundle
            (   string UGUIAtlasPath,
                string NGUIAtlasPath,
                string DependentAssetRootPath,
                string IndependentAssetPath,
                string OutputPath, 
                TextureImporterType   TextureType,
                TextureImporterFormat TextureFormate)
        {
            m_strUGUIAtlasPath = UGUIAtlasPath;
            m_strNGUIAtlasPath = NGUIAtlasPath;
            m_strDependentAssetRootPath = DependentAssetRootPath;
            m_strIndependentAssetPath = IndependentAssetPath;
            m_strOutputPath = OutputPath;

            m_TextureFormate = TextureFormate;
            m_TextureType = TextureType;
            SetBundleName();
        }
        private void SetBundleName()
        {
            Exception e = null;

            // initialize
            CheckParamter(ref e);
            if (!CheckException(e))
            {
                return;
            }

            // set dependents bundle name
            SetDependentsBundleName(ref e);
            if (!CheckException(e))
            {
                return;
            }

            // set independents bundle name
            SetIndependentsBundleName(ref e);
            if (!CheckException(e))
            {
                return;
            }
            
            // set UGUI Atlas bundle name
            SetUGUIAtlasBundleName(ref e);
            if (!CheckException(e))
            {
                return;
            }
            
            // set NGUI Atlas bundle name
            SetNGUIAtlasBundleName(ref e);
            if (!CheckException(e))
            {
                return;
            }

            // Build
            BeginBuildAssetBundle(ref e);
            if (!CheckException(e))
            {
                return;
            }
        }
        private void CheckParamter(ref Exception e)
        {
            if (!Directory.Exists(m_strDependentAssetRootPath))
            {
                e = new Exception("can't fine dependent asset root path");
                return;
            }
            if (!Directory.Exists(m_strIndependentAssetPath))
            {
                e = new Exception("can't fine independent asset root path");
                return;
            }
            if (!Directory.Exists(m_strOutputPath))
            {
                e = new Exception("can't fine output path");
                return;
            }
        }
        private void SetDependentsBundleName(ref Exception e)
        {
            List<PackerInfo> list = new List<PackerInfo>();
            var dir = new DirectoryInfo(m_strDependentAssetRootPath);
            var files = dir.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; ++i)
            {
                if (!IsInIgnoreList(files[i].Name))
                {
                    // add to list
                    list.Add(new PackerInfo(files[i].FullName,string.Empty));
                }
            }
            Dictionary<string, int> refCountMap = new Dictionary<string, int>();

            for (int i = 0; i < list.Count; ++i)
            {
                var elem = list[i];
                var depList = AssetDatabase.GetDependencies(elem.path);
                for (int j = 0; j < depList.Length; ++j)
                {
                    if (refCountMap.ContainsKey(depList[j]))
                    {
                        ++ refCountMap[depList[j]];
                    }
                    else
                    {
                        refCountMap.Add(depList[j],1);
                    }
                }
            }
            
        }
        private void SetIndependentsBundleName(ref Exception e)
        {
            
        }
        private void SetUGUIAtlasBundleName(ref Exception e)
        {
            if (!Directory.Exists(m_strUGUIAtlasPath))
            {
                return;
            }
        }
        private void SetNGUIAtlasBundleName(ref Exception e)
        {
            if (!Directory.Exists(m_strNGUIAtlasPath))
            {
                return;
            }
        }
        private void BeginBuildAssetBundle(ref Exception e)
        {
        }
        private bool CheckException(Exception e)
        {
            if (e != null)
            {
                Debug.LogException(e);
                return false;
            }
            return true;
        }
        private void DoSetBundleName(List<PackerInfo> list)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                PackerInfo info = list[i];
                AssetImporter tmp = AssetImporter.GetAtPath(info.path);
                if (tmp != null)
                {
                    tmp.assetBundleName = info.bundleName;
                }
                else
                {
                    Debug.LogWarning("path inexist " + info.path);
                }
            }
        }
        private bool IsAssetNameVailed(string tmpName)
        {
            if (tmpName.Length <= 0)
            {
                return false;
            }
            var tmpList = tmpName.Split('/');
            if (tmpList.Length > 0)
            {
                tmpName = tmpList[tmpList.Length - 1];
            }
            //Debug.Log("check name " + str);
            var res = tmpName.Split(' ');
            return res.Length <= 1;
        }
        private bool IsInIgnoreList(string path)
        {
            for (int i = 0; i < m_IgnoreList.Length; ++i)
            {
                if (path.EndsWith(m_IgnoreList[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
