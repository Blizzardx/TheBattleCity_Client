using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Assets.Scripts.AssetBundle.BuildAssetbundleTool.Editor
{
    public class BuildAssetElementChildInfo
    {
        public int refCount;
        public AssetInfo assetInfo;
    }
    public class BuildAssetElemmentInfo
    {
        public AssetInfo mainAssetInfo;
        public List<BuildAssetElementChildInfo> depAssetsList;
    }
    public class BuildAssetsRefrenceFilter
    {
        // first key - refrence count ,second key - assets group ,main assets 
        // value - child list
        public Dictionary<int, Dictionary<AssetInfo, List<BuildAssetElementChildInfo>>> filter;
    }
    public class BuildAssetbundleTool
    {
        private string m_strRootPath;
        private string m_strDataPath;
        private string m_strPackByFolderPath;
        private string m_strUguiPath;
        private string m_strOutputPath;
        private Exception m_ErrorInfo;
        private string[] m_IgnoreSuffixList = new string[]
        {
            ".svn",
            ".git",
            ".meta",
            ".cs",
        };
        private string[] m_DataPathWhiteList = new string[]
        {
            ".prefab",
            ".unity",
            ".mat",
            ".wav",
            ".mp3",
        };

        #region public interface
        public Exception Build(string dataPath, string packByFolderPath, string uguiPath, string outputPath)
        {
            m_strRootPath = Application.dataPath;
            m_strDataPath = dataPath;
            m_strPackByFolderPath = packByFolderPath;
            m_strUguiPath = uguiPath;
            m_strOutputPath = outputPath;
            m_ErrorInfo = null;

            // make sure output path is include in StreamingAssets
            CheckOutputPaht(m_strOutputPath);
            if (m_ErrorInfo != null)
            {
                return m_ErrorInfo;
            }
            List<string> paths = new List<string>() { dataPath, packByFolderPath, uguiPath, outputPath };
            // check path argument
            CheckInputArgument(paths);
            if (m_ErrorInfo != null)
            {
                return m_ErrorInfo;
            }
            // check all assets in root path
            CheckAllAssets();
            if (m_ErrorInfo != null)
            {
                return m_ErrorInfo;
            }
            // check all packed assets ( with deps )
            CheckAllPackAssets();
            if (m_ErrorInfo != null)
            {
                return m_ErrorInfo;
            }
            // handler ugui ,pack atlas
            SetAndPackUguiAtlas();
            if (m_ErrorInfo != null)
            {
                return m_ErrorInfo;
            }
            // begin build asset bundle & out put asset bundle
            BuildBundlesAndOutput();

            return m_ErrorInfo;
        }
        #endregion

        #region system function
        private void CheckInputArgument(List<string> paths)
        {
            // check datapath is include in other path
            for (int i = 0; i < paths.Count; ++i)
            {
                for (int j = 0; j < paths.Count; ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (CheckPathInIncludeIn(paths[i], paths[j]))
                    {
                        m_ErrorInfo = new Exception(string.Format("Path {0} is Include In {1}", paths[j], paths[i]));
                        return;
                    }
                }
                // check folder content
                var folderPath = Application.dataPath + "/" + paths[i];
                if(!Directory.Exists(folderPath))
                {
                    m_ErrorInfo = new Exception("Path is not exist " + folderPath);
                    return;
                }
            }
            return;
        }
        private void CheckOutputPaht(string m_strOutputPath)
        {
            var realOutputPath = Application.dataPath + "/" + m_strOutputPath;
            if(!Directory.Exists(realOutputPath))
            {
                Directory.CreateDirectory(realOutputPath);
            }
            if(!CheckFileInInFolder(realOutputPath,Application.streamingAssetsPath))
            {
                m_ErrorInfo = new Exception("out put path must include in streaming assets");
            }
        }
        private void CheckAllAssets()
        {
            List<BuildAssetElemmentInfo> allAssetList = new List<BuildAssetElemmentInfo>();

            var dataPathPerfix = Application.dataPath.Substring(0, Application.dataPath.IndexOf("Assets"));
            var realPath = Application.dataPath + "/" + m_strDataPath;
            var dir = new DirectoryInfo(realPath);
            var files = dir.GetFiles("*", SearchOption.AllDirectories);
            
            for (var i = 0; i < files.Length; ++i)
            {                
                AssetInfo info = new AssetInfo(files[i].FullName);

                if (info.IsInSuffixList(m_IgnoreSuffixList))
                {
                    continue;
                }
                BuildAssetElemmentInfo assetInfo = new BuildAssetElemmentInfo();
                assetInfo.mainAssetInfo = info;

                var deps = AssetDatabase.GetDependencies(assetInfo.mainAssetInfo.GetRelativePath());
                assetInfo.depAssetsList = new List<BuildAssetElementChildInfo>(deps.Length);

                foreach(var elem in deps)
                {
                    BuildAssetElementChildInfo childInfo = new BuildAssetElementChildInfo();
                    childInfo.assetInfo = new AssetInfo(dataPathPerfix + elem);
                    childInfo.refCount = 0;
                    assetInfo.depAssetsList.Add(childInfo);
                }

                allAssetList.Add(assetInfo);
            }
            //确保需要打包的主资源不重名
            if(IsMainAssetsHaveSameName(allAssetList))
            {
                return;
            }
            //确保需要打包的主资源在白名单中
            if (!IsMainAsseetsInWhiteList(allAssetList))
            {
                return;
            }
            // 确保所有资源以及引用的资源不在ugui 和 pack by folder 目录中
            if (IsAssetsInTargetDataPath(allAssetList))
            {
                return;
            }
            //清理列表  查找 mainasset 出现在 别的 dep asset list 中的情况
            CleanList(allAssetList);
            // 构建引用数
            BuildRefrenceCountMap(allAssetList);
            // 创建 引用数 - 主资源 - （依赖资源数组）的筛选器
            var filter = FilterByRerenceCount(allAssetList);
            // 根据筛选器重置bundle 名字 
            SetAssetbundleNameByFilter(filter);
        }
        private bool IsMainAsseetsInWhiteList(List<BuildAssetElemmentInfo> allAssetList)
        {
            foreach(var elem in allAssetList)
            {
                if (!elem.mainAssetInfo.IsInSuffixList(m_DataPathWhiteList))
                {
                    m_ErrorInfo = new Exception("Asset is not in white list " + elem.mainAssetInfo.GetFullPath());
                    return false;
                }
            }
            return true;
        }
        private void CheckAllPackAssets()
        {

        }
        private void SetAndPackUguiAtlas()
        {

        }
        private void BuildBundlesAndOutput()
        {
            var output = "Assets/" + m_strOutputPath;
            var res = BuildPipeline.BuildAssetBundles
           (output,
           BuildAssetBundleOptions.DeterministicAssetBundle |
           //  BuildAssetBundleOptions.UncompressedAssetBundle |
           BuildAssetBundleOptions.StrictMode,
           EditorUserBuildSettings.activeBuildTarget);
            if(null == res)
            {
                m_ErrorInfo = new Exception("Build with error");
            }
        }
        #endregion

        #region tool
        private bool CheckPathInIncludeIn(string targetPath, string sourcePath)
        {
            targetPath = targetPath.Replace('\\', '/');
            targetPath = targetPath.ToLower();

            sourcePath = sourcePath.Replace('\\', '/');
            sourcePath = sourcePath.ToLower();

            return targetPath.Contains(sourcePath);
        }
        private bool CheckFileInInFolder(string filePath, string folderPath)
        {
            filePath = filePath.Replace('\\', '/');
            filePath = filePath.ToLower();

            folderPath = folderPath.Replace('\\', '/');
            folderPath = folderPath.ToLower();

            return filePath.StartsWith(folderPath);
        }
        private bool IsAssetsInTargetDataPath(List<BuildAssetElemmentInfo> targetList)
        {
            List<string> checkPathList = new List<string>()
            {
                Application.dataPath + "/" + m_strPackByFolderPath,
                //Application.dataPath + "/" + m_strUguiPath,
            };
            foreach (var elem in targetList)
            {
                foreach (var subElem in elem.depAssetsList)
                {
                    foreach(var path in checkPathList)
                    {
                        if(CheckFileInInFolder(subElem.assetInfo.GetFullPath(),path))
                        {
                            m_ErrorInfo = new Exception(subElem.assetInfo.GetFullPath() + " can't at folder " + path + " " + elem.mainAssetInfo.GetFullPath());
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool IsMainAssetsHaveSameName(List<BuildAssetElemmentInfo> targetList)
        {
            HashSet<string> tmpSet = new HashSet<string>();
            foreach(var elem in targetList)
            {
                if(tmpSet.Contains(elem.mainAssetInfo.GetFileNameWithoutSuffix()))
                {
                    m_ErrorInfo = new Exception(elem.mainAssetInfo.GetFullPath());
                    return true;
                }
                tmpSet.Add(elem.mainAssetInfo.GetFileNameWithoutSuffix());
            }
            return false;
        }
        private void CleanList(List<BuildAssetElemmentInfo> targetList)
        {
            for(int i=0;i<targetList.Count;++i)
            {
                var assets = targetList[i];
                for(int j=0;j<targetList.Count;++j)
                {
                    if(i == j)
                    {
                        continue;
                    }
                    var curAssets = targetList[j];
                    bool isNeedClean = false;
                    foreach(var elem in curAssets.depAssetsList)
                    {
                        if(elem.assetInfo.GetFullPath() == assets.mainAssetInfo.GetFullPath())
                        {
                            // mark
                            isNeedClean = true;
                            break;
                        }
                    }
                    if(isNeedClean)
                    {
                        // do clean
                        for(int k=0;k<curAssets.depAssetsList.Count;)
                        {
                            bool needRemove = false;
                            for(int k1=0;k1<assets.depAssetsList.Count;++k1)
                            {
                                if(assets.depAssetsList[k1].assetInfo.GetFullPath() == curAssets.depAssetsList[k].assetInfo.GetFullPath())
                                {
                                    needRemove = true;
                                    break;
                                }
                            }
                            if(needRemove)
                            {
                                curAssets.depAssetsList.RemoveAt(k);
                            }
                            else
                            {
                                ++k;
                            }
                        }
                    }
                }
            }
        }
        private void BuildRefrenceCountMap(List<BuildAssetElemmentInfo> targetList)
        {
            Dictionary<string, int> refrenceCountMap = new Dictionary<string, int>();
            foreach(var elem in targetList)
            {
                foreach(var subElem in elem.depAssetsList)
                {
                    if (!refrenceCountMap.ContainsKey(subElem.assetInfo.GetFullPath()))
                    {
                        refrenceCountMap.Add(subElem.assetInfo.GetFullPath(), 1);
                    }
                    else
                    {
                        ++refrenceCountMap[subElem.assetInfo.GetFullPath()];
                    }
                }
            }
            // write & save refrence count
            foreach (var elem in targetList)
            {
                foreach (var subElem in elem.depAssetsList)
                {
                    subElem.refCount = refrenceCountMap[subElem.assetInfo.GetFullPath()];
                }
            }
        }
        private BuildAssetsRefrenceFilter FilterByRerenceCount(List<BuildAssetElemmentInfo> targetList)
        {
            BuildAssetsRefrenceFilter res = new BuildAssetsRefrenceFilter();
            // first key - refrence count ,second key - assets group ,main assets 
            // value - child list
            res.filter = new Dictionary<int, Dictionary<AssetInfo, List<BuildAssetElementChildInfo>>>();

            foreach (var elem in targetList)
            {
                foreach (var subElem in elem.depAssetsList)
                {
                    if (!res.filter.ContainsKey(subElem.refCount))
                    {
                        res.filter.Add(subElem.refCount, new Dictionary<AssetInfo, List<BuildAssetElementChildInfo>>());
                    }
                    if(!res.filter[subElem.refCount].ContainsKey(elem.mainAssetInfo))
                    {
                        res.filter[subElem.refCount].Add(elem.mainAssetInfo, new List<BuildAssetElementChildInfo>());
                    }
                    res.filter[subElem.refCount][elem.mainAssetInfo].Add(subElem);
                }
            }
            return res;
        }
        private void SetAssetbundleNameByFilter(BuildAssetsRefrenceFilter filter)
        {
            foreach(var elem in filter.filter)
            {
                int refrenceCount = elem.Key;
                if (refrenceCount == 0)
                {
                    Debug.LogError("error refrence count ");
                    continue;
                }
                else if (refrenceCount == 1)
                {
                    foreach (var subElem in elem.Value)
                    {
                        var mainAsset = subElem.Key;
                        foreach (var asset in subElem.Value)
                        {
                            SetAssetBundleName(asset.assetInfo, mainAsset);
                        }
                    }
                }
                else
                {
                    HandleAssets(refrenceCount, elem.Value);
                }
            }
        }
        private void HandleAssets(int refrenceCount, Dictionary<AssetInfo, List<BuildAssetElementChildInfo>> infoList)
        {
            Dictionary<string, AssetInfo> allAssetList = new Dictionary<string, AssetInfo>();
            HashSet<string> doneAssetList = new HashSet<string>();

            foreach (var elem in infoList)
            {
                foreach(var subElem in elem.Value)
                {
                    if(!allAssetList.ContainsKey(subElem.assetInfo.GetFullPath()))
                    {
                        allAssetList.Add(subElem.assetInfo.GetFullPath(),subElem.assetInfo);
                    }
                }
            }
            foreach(var asset in allAssetList)
            {
                if (doneAssetList.Contains(asset.Key))
                {
                    continue;
                }
                int tmpIndex = 0;
                List<AssetInfo> finalList = new List<AssetInfo>();

                foreach (var elem in infoList)
                {
                    foreach (var subElem in elem.Value)
                    {
                        if(subElem.assetInfo.GetFullPath() == asset.Key)
                        {
                            ++tmpIndex;
                            if (finalList.Count == 0)
                            {
                                finalList = InitFinalList(elem.Value, asset.Key);
                            }
                            else
                            {
                                finalList = CombineToSaveSameItem(finalList, elem.Value, asset.Key);
                            }
                            break;
                        }
                    }
                    if(tmpIndex == refrenceCount)
                    {
                        break;
                    }
                }
                // add self to list
                AssetInfo self = new AssetInfo(asset.Key);
                finalList.Add(self);
                foreach(var elem in finalList)
                {
                    SetAssetBundleName(elem, self);
                    // add to done list
                    doneAssetList.Add(asset.Key);
                }
            }
        }
        private List<AssetInfo> InitFinalList(List<BuildAssetElementChildInfo> targetList, string mainAssetFullPath)
        {
            List<AssetInfo> res = new List<AssetInfo>();
            foreach (var elem in targetList)
            {
                if(elem.assetInfo.GetFullPath() == mainAssetFullPath)
                {
                    continue;
                }
                res.Add(elem.assetInfo);
            }
            return res;
        }
        private List<AssetInfo> CombineToSaveSameItem(List<AssetInfo> finalList, List<BuildAssetElementChildInfo> targetList, string mainAssetFullPath)
        {
            List<AssetInfo> res = new List<AssetInfo>();
            var tmpList = InitFinalList(targetList, mainAssetFullPath);

            Dictionary<string, AssetInfo> tmpMap = new Dictionary<string, AssetInfo>();
            foreach(var elem in finalList)
            {
                if(!tmpMap.ContainsKey(elem.GetFullPath()))
                {
                    tmpMap.Add(elem.GetFullPath(), elem);
                }
                else
                {
                    Debug.LogError("sth wrong");
                }
            }
            foreach(var elem in tmpList)
            {
                if(tmpMap.ContainsKey(elem.GetFullPath()))
                {
                    res.Add(elem);
                }
            }
            return res;
        }
        private void SetAssetBundleName(AssetInfo targetAsset,AssetInfo mainAsset)
        {
            string bundleName = mainAsset.GetFileNameWithoutSuffix() + ".pkg";
            var importer = AssetImporter.GetAtPath(targetAsset.GetRelativePath());
            importer.assetBundleName = bundleName;
        }
        #endregion
    }
}
