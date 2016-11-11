using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using NUnit.Framework.Constraints;
using Text.Tool;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.Framework.Assets.NewAssetTest.Editor
{
    class AssetbundlePacker
    {
        private class AssetbundleRefInfo
        {
            public AssetbundleRefInfo()
            {
                
            }

            public AssetbundleRefInfo(string name, int refCount,int treeDeepth)
            {
                this.name = name;
                this.refCount = refCount;
                this.treeDeepth = treeDeepth;
            }
            public string name;
            public int refCount;
            public int treeDeepth;
        }
        private string[] m_IgnoreList = new string[] {".meta",".svn",".cs"};
        private string m_strUGUIAtlasPath;
        private string m_strNGUIAtlasPath;
        private string m_strDependentAssetRootPath;
        private string m_strOutputPath;

        private TextureImporterType     m_TextureType;
        private TextureImporterFormat   m_TextureFormate;

        private Dictionary<string, List<string>> m_Bundle2AssetFindMap;
        private Dictionary<string, string>      m_Asset2BundleFindMap;

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
            m_Bundle2AssetFindMap = new Dictionary<string, List<string>>();
            m_Asset2BundleFindMap = new Dictionary<string, string>();

            BeginSetBundleName();
        }
        public void BeginBuild()
        {
            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget);
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
                        //AssetImporter tmp = AssetImporter.GetAtPath(GetNameByPath(fileInfo.FullName));
                        //if (tmp != null)
                        //{
                        //    tmp.assetBundleName = null;
                        //}
                    }
                }
            }
        }
        public void GenAssetToBundleFindMap()
        {
            Dictionary<string, object> b2aMap = new Dictionary<string, object>();
            foreach (var elem in m_Bundle2AssetFindMap)
            {
                List<object> list = new List<object>();
                foreach (var tmpElem in elem.Value)
                {
                    list.Add(tmpElem);
                }
                b2aMap.Add(elem.Key, list);
            }
            FileUtils.WriteStringFile(m_strOutputPath + "/B2A.txt",Json.Serialize(b2aMap));

            Dictionary<string, object> a2bMap = new Dictionary<string, object>();
            foreach (var elem in m_Asset2BundleFindMap)
            {
                a2bMap.Add(elem.Key, elem);
            }
            FileUtils.WriteStringFile(m_strOutputPath + "/A2B.txt", Json.Serialize(a2bMap));

            //// test code -begin
            //foreach (var elem in m_Bundle2AssetFindMap)
            //{
            //    string path = Application.streamingAssetsPath + "/AssetBundles/iOS/" + elem.Key;
            //    if (!File.Exists(path))
            //    {
            //        Debug.LogError("can't fine bundle "+ path);
            //    }
            //}
            //// test code -end
        }
        #endregion

        #region system function

        private void BeginSetBundleName()
        {
            if (!CheckDirectory())
            {
                return;
            }
            if (!SetDependentPathBundleName())
            {
                return;
            }
        }
        private bool CheckDirectory()
        {
            // fix path to mutiplantform
            m_strDependentAssetRootPath = FixPathToMutiPlantformFormate(m_strDependentAssetRootPath);
            m_strNGUIAtlasPath = FixPathToMutiPlantformFormate(m_strNGUIAtlasPath);
            m_strUGUIAtlasPath = FixPathToMutiPlantformFormate(m_strUGUIAtlasPath);
            m_strOutputPath = FixPathToMutiPlantformFormate(m_strOutputPath);

            if (Directory.Exists(m_strDependentAssetRootPath))
            {
                if (!IsPathInDataPath(m_strDependentAssetRootPath))
                {
                    Debug.LogError("Path is not in build path " + m_strDependentAssetRootPath);
                    return false;
                }
            }
            else
            {
                Debug.LogWarning("Cant' find path " + m_strDependentAssetRootPath);
            }
            if (Directory.Exists(m_strNGUIAtlasPath))
            {
                if (!IsPathInDataPath(m_strNGUIAtlasPath))
                {
                    Debug.LogError("Path is not in build path " + m_strNGUIAtlasPath);
                    return false;
                }
            }
            else
            {
                Debug.LogWarning("Cant' find path " + m_strNGUIAtlasPath);
            }
            if (Directory.Exists(m_strUGUIAtlasPath))
            {
                if (!IsPathInDataPath(m_strUGUIAtlasPath))
                {
                    Debug.LogError("Path is not in build path " + m_strUGUIAtlasPath);
                    return false;
                }
            }
            else
            {
                Debug.LogWarning("Cant' find path " + m_strUGUIAtlasPath);
            }
            FileUtils.EnsureFolder(m_strOutputPath);

            return true;
        }
        private bool SetDependentPathBundleName()
        {
            Dictionary<string, List<string>> allAssetDepMap = new Dictionary<string, List<string>>();
            Dictionary<string, int> allAssetRefMap = new Dictionary<string, int>();

            List<FileInfo> allFileList = new List<FileInfo>();

            var dir = new DirectoryInfo(m_strDependentAssetRootPath);
            var files = dir.GetFiles("*", SearchOption.AllDirectories);
            for (var i = 0; i < files.Length; ++i)
            {
                var fileInfo = files[i];
                if (IsInIgnoreList(fileInfo.Name))
                {
                    continue;
                }
                // add to ready list
                allFileList.Add(files[i]);
            }

            // check name
            if (!CheckIsSameNameFileInList(allFileList))
            {
                return false;
            }
            List<string> allFileNameList = new List<string>(allFileList.Count);
            for (int i = 0; i < allFileList.Count; ++i)
            {
                var file = allFileList[i];
                // get relate path
                string relatePath = FixPathToRelatePathFormate(FixPathToMutiPlantformFormate(file.FullName));
                // record name
                allFileNameList.Add(relatePath);
            }
            var allDepList = AssetDatabase.GetDependencies(allFileNameList.ToArray());
            // get refrence count
            for (int i = 0; i < allDepList.Length; ++i)
            {
                string relatePath = allDepList[i];
                // get all dep asset
                var depList = AssetDatabase.GetDependencies(relatePath);
                // add to dep map
                allAssetDepMap.Add(relatePath,new List<string>(depList));
                foreach (var asset in depList)
                {
                    if (IsInIgnoreList(asset))
                    {
                        continue;
                    }
                    // update refrence count
                    AddRefCount(asset, allAssetRefMap);
                }
            }

            // check asset name
            if (!CheckIsNameVailedInList(allAssetRefMap.Keys.ToArray()))
            {
                return false;
            }

            // build all asset list
            List<AssetbundleRefInfo> allAssetList = new List<AssetbundleRefInfo>();
            foreach (var elem in allAssetRefMap)
            {
                int treeDeepth = GetTreeDeepth(elem.Key);
                allAssetList.Add(new AssetbundleRefInfo(elem.Key,elem.Value,treeDeepth));
            }

            // sort asset by tree deepth 
            allAssetList.Sort((l, r) =>
            {
                if (l.treeDeepth > r.treeDeepth)
                {
                    return -1;
                }
                if (l.treeDeepth < r.treeDeepth)
                {
                    return 1;
                }
                return 0;
            });
            Dictionary<string, List<string>> bundleGroupMap = new Dictionary<string, List<string>>();
            HashSet<string> markList = new HashSet<string>();

            // set dep root path folder assetbundle name
            SetBundleNameByRoot(allFileNameList, bundleGroupMap, allAssetRefMap, markList);

            // set other dep root path folder assetbundle name
            for (int i = 0; i < allAssetList.Count; ++i)
            {
                SetBundleNameByRoot(null, allAssetList[i].name, allAssetList[i].name, bundleGroupMap, allAssetRefMap, markList);
            }
            
            // set bundle name
            foreach (var elem in bundleGroupMap)
            {
                // is scene file
                bool isScene = IsAssetIsSceneFile(elem.Key);

                string bundleName = GetBundleNameByPath(elem.Key);
                for (int i = 0; i < elem.Value.Count; ++i)
                {
                    if (isScene && elem.Value[i] != elem.Key)
                    {
                        string sceneBundleName = GetBundleSceneNameByPath(elem.Key);
                        DoSetBundleName(sceneBundleName, elem.Value[i]);
                    }
                    else
                    {
                        DoSetBundleName(bundleName, elem.Value[i]);
                    }
                }
            }
            return true;
        }
        private void DoSetBundleName(string bundleName, string path)
        {
            // set to lower
            bundleName = bundleName.ToLower();

            AssetImporter tmp = AssetImporter.GetAtPath(path);
            if (tmp != null)
            {
                //tmp.assetBundleName = bundleName;
                //tmp.SaveAndReimport();

                // test code - begin
                if (tmp.assetBundleName != bundleName)
                {
                    Debug.LogError(bundleName+" " + path);
                }
                // test code - end

                // build Asset to Asset bundle find map
                m_Asset2BundleFindMap.Add(path, bundleName);
                // build Asset bundle to Asset find map
                if (!m_Bundle2AssetFindMap.ContainsKey(bundleName))
                {
                    m_Bundle2AssetFindMap.Add(bundleName,new List<string>() {path});
                }
                else
                {
                    m_Bundle2AssetFindMap[bundleName].Add(path);
                }
            }
            else
            {
                Debug.LogWarning("error on convert to asset importer " + path);
            }
        }
        private void SetBundleNameByRoot
            (List<string> allfileList, 
            Dictionary<string, List<string>> bundleGroupMap, 
            Dictionary<string, int> allAssetRefMap, 
            HashSet<string> markList)
        {
            for (int i = 0; i < allfileList.Count; ++i)
            {
                string name = allfileList[i];
                string bundleName = GetBundleNameByPath(name);
                SetBundleNameByRoot(name,bundleName,bundleGroupMap,allAssetRefMap,markList);
            }
        }
        private void SetBundleNameByRoot
            (string assetName, 
            string bundleName,
            Dictionary<string, List<string>> bundleGroupMap, 
            Dictionary<string, int> allAssetRefMap, 
            HashSet<string> markList)
        {
            if (markList.Contains(assetName))
            {
                return;
            }
            int rootRefCount = allAssetRefMap[assetName];

            var deps = AssetDatabase.GetDependencies(assetName, true);
            for (int i = 0; i < deps.Length; ++i)
            {
                int selfRefCount = allAssetRefMap[deps[i]];
                if (rootRefCount == selfRefCount + 1)
                {
                    // add to list
                    markList.Add(deps[i]);
                    // add to bundle list
                    if (!bundleGroupMap.ContainsKey(bundleName))
                    {
                        bundleGroupMap.Add(bundleName,new List<string>());
                    }
                    bundleGroupMap[bundleName].Add(deps[i]);
                }
                SetBundleNameByRoot(deps[i], bundleName, bundleGroupMap, allAssetRefMap, markList);
            }
        }
        private void SetBundleNameByRoot
            (string rootName, 
            string selfName,
            string curbundlename,
            Dictionary<string, List<string>> bundleGroupMap,
            Dictionary<string, int> allAssetRefMap, 
            HashSet<string> markList)
        {
            if (markList.Contains(selfName))
            {
                return;
            }
            if (null == rootName)
            {
                bundleGroupMap.Add(curbundlename, new List<string>() { selfName });
            }
            else
            {
                int rootRefCount = allAssetRefMap[rootName];
                int slefRefCount = allAssetRefMap[selfName];

                if (rootRefCount == slefRefCount - 1)
                {
                    // add self to root group
                    bundleGroupMap[curbundlename].Add(selfName);
                }
                else
                {
                    // add self to new group
                    curbundlename = selfName;
                    bundleGroupMap.Add(curbundlename, new List<string>() {selfName});
                }
            }

            // mark
            markList.Add(selfName);

            var deps = AssetDatabase.GetDependencies(selfName, false);
            for (int i = 0; i < deps.Length; ++i)
            {
                if (IsInIgnoreList(deps[i]))
                {
                    continue;
                }
                string name = deps[i];
                SetBundleNameByRoot(selfName, name, curbundlename,bundleGroupMap, allAssetRefMap,markList);
            }
        }
        private void AddRefCount(string path,Dictionary<string,int> refCountMap )
        {
            if (refCountMap.ContainsKey(path))
            {
                ++ refCountMap[path];
            }
            else
            {
                refCountMap.Add(path, 1);
            }
        }
        private int GetTreeDeepth(string path)
        {
            int num = -1;
            var deps = AssetDatabase.GetDependencies(path, false);
            if (deps.Length == 0)
            {
                return 1;
            }
            for (int j = 0; j < deps.Length; j++)
            {
                int tmp = GetTreeDeepth(deps[j]);
                if (tmp > num)
                {
                    num = tmp;
                }
            }

            return num + 1;
        }
        #endregion

        #region tool
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
        private bool IsVailed(string str)
        {
            if (str.Length <= 0)
            {
                return false;
            }
            var tmpList = str.Split('/');
            if (tmpList.Length > 0)
            {
                str = tmpList[tmpList.Length - 1];
            }
            //Debug.Log("check name " + str);
            var res = str.Split(' ');
            return res.Length <= 1;
        }
        private bool IsPathInDataPath(string path)
        {
            return path.StartsWith(Application.dataPath);
        }
        private string FixPathToMutiPlantformFormate(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            return path.Replace('\\', '/');
        }
        private string FixPathToRelatePathFormate(string path)
        {
            string res = path;
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            var index = path.IndexOf("Assets/");
            if (index != -1)
            {
                res = path.Substring(index);
            }
            return res;
        }
        private bool CheckIsSameNameFileInList(List<FileInfo> list)
        {
            if (null == list || list.Count == 0)
            {
                return false;
            }
            bool res = true;
            HashSet<string> nameList = new HashSet<string>();
            for (int i = 0; i < list.Count; ++i)
            {
                var file = list[i];
                if (nameList.Contains(file.Name))
                {
                    res = false;
                    Debug.LogError("Same name asset " + file.Name);
                }
                else
                {
                    nameList.Add(file.Name);
                }
            }
            return res;
        }
        private bool CheckIsNameVailedInList(string[] fileList)
        {
            if (null == fileList || fileList.Length == 0)
            {
                return true;
            }
            bool res = true;
            for (int i = 0; i < fileList.Length; ++i)
            {
                if (!IsVailed(fileList[i]))
                {
                    res = false;
                    Debug.LogError("Asset Name is NotVailed " + fileList[i]);
                }
            }
            return res;
        }
        private string GetBundleNameByPath(string path)
        {
            // path is start with Assets/.....
            int startIndex = path.LastIndexOf('/');
            path = path.Substring(startIndex+1);

            int index = path.LastIndexOf('.');
            if (index != -1)
            {
                path = path.Substring(0, index);
            }
            return path + ".bundle";
            return path.Replace('/','_') + ".bundle";
        }
        private string GetBundleSceneNameByPath(string path)
        {
            // path is start with Assets/.....
            int startIndex = path.LastIndexOf('/');
            path = path.Substring(startIndex + 1);
            int index = path.LastIndexOf('.');
            if (index != -1)
            {
                path = path.Substring(0, index);
            }
            return path + "unity.bundle";
            return path.Replace('/','_')  + ".bundle";
        }
        private bool IsAssetIsSceneFile(string path)
        {
            return path.EndsWith(".unity");
        }
        private bool IsPathIncludePath(string sourcePath, string targetpath)
        {
            return targetpath.StartsWith(sourcePath);
        }
        private void TestPrintRefCount(Dictionary<string, int> refCountMap)
        {
            Dictionary<string, object> tmpMap = new Dictionary<string, object>();
            foreach (var elem in refCountMap)
            {
                tmpMap.Add(elem.Key, elem);
            }
            FileUtils.WriteStringFile(m_strOutputPath + "/RefCount.txt", Json.Serialize(tmpMap));
        }
        #endregion

    }
}
