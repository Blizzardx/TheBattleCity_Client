using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.Framework.Assets.NewAssetTest.Editor
{
    class AssetbundlePacker
    {
        private string[] m_IgnoreList = new string[] {".meta",".svn",".cs"};
        private string m_strUGUIAtlasPath;
        private string m_strNGUIAtlasPath;
        private string m_strDependentAssetRootPath;
        private string m_strOutputPath;

        private TextureImporterType     m_TextureType;
        private TextureImporterFormat   m_TextureFormate;

        #region public interface
        public void BeginSetBundleName
            (   string UGUIAtlasPath,
                string NGUIAtlasPath,
                string DependentAssetRootPath,
                string OutputPath, 
                TextureImporterType   TextureType,
                TextureImporterFormat TextureFormate)
        {
            m_strUGUIAtlasPath = UGUIAtlasPath;
            m_strNGUIAtlasPath = NGUIAtlasPath;
            m_strDependentAssetRootPath = DependentAssetRootPath;
            m_strOutputPath = OutputPath;

            m_TextureFormate = TextureFormate;
            m_TextureType = TextureType;
            SetBundleName();
        }
        public void BeginBuild()
        {
            Exception e = null;
            BeginBuildAssetBundle(ref e);
            CheckAndPrintException(e);
        }
        public void ResetAllBundleName(string AppDataPath)
        {
            if (Directory.Exists(AppDataPath))
            {
                var dir = new DirectoryInfo(AppDataPath);
                var files = dir.GetFiles("*", SearchOption.AllDirectories);
                for (var i = 0; i < files.Length; ++i)
                {
                    var fileInfo = files[i];
                    // is in ignore list
                    if (!IsInIgnoreList(fileInfo.FullName))
                    {
                        AssetImporter tmp = AssetImporter.GetAtPath(GetNameByPath(fileInfo.FullName));
                        if (tmp != null)
                        {
                            tmp.assetBundleName = null;
                        }
                    }
                }
            }
        }
        public void GenAssetToBundleFindMap()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region system function
        private void SetBundleName()
        {
            Exception e = null;

            // initialize
            CheckParamter(ref e);
            if (!CheckAndPrintException(e))
            {
                return;
            }

            // check bundle name
            if (!CheckAssetNameIsVailed())
            {
                return;
            }

            // set dependents bundle name
            SetDependentsBundleName(ref e);
            if (!CheckAndPrintException(e))
            {
                return;
            }
            
            // set UGUI Atlas bundle name
            SetUGUIAtlasBundleName(ref e);
            if (!CheckAndPrintException(e))
            {
                return;
            }
            
            // set NGUI Atlas bundle name
            SetNGUIAtlasBundleName(ref e);
            if (!CheckAndPrintException(e))
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
            if (!Directory.Exists(m_strOutputPath))
            {
                e = new Exception("can't fine output path");
                return;
            }
        }
        private void SetDependentsBundleName(ref Exception e)
        {
            List<string> list = new List<string>();
            var dir = new DirectoryInfo(m_strDependentAssetRootPath);
            var files = dir.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; ++i)
            {
                if (!IsInIgnoreList(files[i].Name))
                {
                    // add to list
                    list.Add(files[i].FullName);
                }
            }
            Dictionary<string, int> refCountMap = new Dictionary<string, int>();
            Dictionary<string, string> refFindMap = new Dictionary<string, string>();
            for (int i = 0; i < list.Count; ++i)
            {
                var elem = list[i];
                var depList = AssetDatabase.GetDependencies(GetNameByPath(elem));
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
                    // mark 
                    if (refFindMap.ContainsKey(depList[i]))
                    {
                        refFindMap.Add(depList[i], elem);
                    }
                    else
                    {
                        refFindMap[depList[i]] = elem;
                    }
                }
            }

            foreach (var elem in refCountMap)
            {
                string bundleName = string.Empty;
                if (elem.Value == 1 )
                {
                    // check is resource in build path
                    if (IsPathInBuildPath(elem.Key))
                    {
                        bundleName = GetBundleNameByPath(elem.Key);
                        DoSetBundleName(elem.Key, bundleName);
                    }
                    // check resource is scene file
                    else if (IsPathSceneFile(refFindMap[elem.Key]))
                    {
                        DoSetBundleName(elem.Key, GetSceneDepBundleName(refFindMap[elem.Key]));
                    }
                    else
                    {
                        //
                        bundleName = GetBundleNameByPath(refFindMap[elem.Key]);
                        DoSetBundleName(elem.Key, bundleName);
                    }
                }
                else
                {
                    bundleName = GetBundleNameByPath(elem.Key);
                    DoSetBundleName(elem.Key, bundleName);
                }
            }
        }
        private void SetUGUIAtlasBundleName(ref Exception e)
        {
            if (!Directory.Exists(m_strUGUIAtlasPath))
            {
                return;
            }

            List<string> list = new List<string>();
            var dir = new DirectoryInfo(m_strUGUIAtlasPath);
            var files = dir.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; ++i)
            {
                if (!IsInIgnoreList(files[i].Name))
                {
                    // add to list
                    list.Add(files[i].FullName);
                }
            }
            for (int i = 0; i < list.Count; ++i)
            {
                var importer = TextureImporter.GetAtPath(list[i]) as TextureImporter;
                if (importer != null)
                {
                    importer.spritePackingTag = list[i];
                    importer.textureType = TextureImporterType.Sprite;
                    importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
                    importer.maxTextureSize = 1024;
                    importer.mipmapEnabled = false;
                    importer.filterMode = FilterMode.Trilinear;

                    importer.assetBundleName = list[i];
                    importer.SaveAndReimport();
                }
            }
        }
        private void SetNGUIAtlasBundleName(ref Exception e)
        {
            if (!Directory.Exists(m_strNGUIAtlasPath))
            {
                return;
            }
            List<string> list = new List<string>();
            var dir = new DirectoryInfo(m_strUGUIAtlasPath);
            var files = dir.GetFiles("*.prefab", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; ++i)
            {
                if (!IsInIgnoreList(files[i].Name))
                {
                    // add to list
                    list.Add(files[i].FullName);
                }
            }
            for (int i = 0; i < list.Count; ++i)
            {
                var elem = list[i];
                var depList = AssetDatabase.GetDependencies(GetNameByPath(elem));
                for (int j = 0; j < depList.Length; ++j)
                {
                    if (!IsPathInNGUIPath(depList[j]))
                    {
                        string errorMsg = "NGUI Atlas is not in correct path "+ depList[i];
                        Debug.LogError(errorMsg);
                        e = new Exception(errorMsg);
                        return;
                    }
                    DoSetBundleName(elem, elem + ".bundle");
                }
            }
        }
        private void BeginBuildAssetBundle(ref Exception e)
        {
        }
        private bool CheckAndPrintException(Exception e)
        {
            if (e != null)
            {
                Debug.LogException(e);
                return false;
            }
            return true;
        }
        private void DoSetBundleName(string path,string bundleName)
        {
            AssetImporter tmp = AssetImporter.GetAtPath(path);
            if (tmp != null)
            {
                tmp.assetBundleName = bundleName;
            }
            else
            {
                Debug.LogWarning("path inexist " + path);
            }
        }
        private string GetBundleNameByPath(string path)
        {
            int index = path.LastIndexOf('/');
            return path.Substring(index + 1) + ".bundle";
        }
        private bool CheckAssetNameIsVailed()
        {
            string[] pathList = new string[] { m_strUGUIAtlasPath ,m_strNGUIAtlasPath,m_strDependentAssetRootPath};
            for (int i = 0; i < pathList.Length; ++i)
            {
                pathList[i] = GetNameByPath(pathList[i]);
            }

            var depList = AssetDatabase.GetDependencies(pathList);
            bool isOk = true;
            HashSet<string> nameStore = new HashSet<string>();

            for (int i = 0; i < depList.Length; ++i)
            {
                var name = depList[i];
                if (!IsAssetNameVailed(name))
                {
                    Debug.LogError(" name is not vailed : " + name);
                    isOk = false;
                }
                if (nameStore.Contains(name))
                {
                    Debug.LogError(" name is alreay exist : " + name);
                    isOk = false;
                }
                else
                {
                    nameStore.Add(name);
                }
            }
            return isOk;
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
        private bool IsPathInBuildPath(string path)
        {
            return path.StartsWith(m_strDependentAssetRootPath);
        }
        private bool IsPathInNGUIPath(string path)
        {
            return path.StartsWith(m_strNGUIAtlasPath);
        }
        private bool IsPathSceneFile(string path)
        {
            return path.EndsWith(".unity");
        }
        private string GetSceneDepBundleName(string path)
        {
            return path+"_scene";
        }
        private string GetNameByPath(string path)
        {
            var fullName = path.Replace('\\', '/');
            var index = fullName.IndexOf("Assets/");
            if (index != -1)
            {
                return fullName.Substring(index);
            }
            return fullName;
        }
        #endregion

    }
}
