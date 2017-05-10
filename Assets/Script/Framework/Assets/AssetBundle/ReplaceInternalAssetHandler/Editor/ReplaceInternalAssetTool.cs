using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.IO.Compression;
using System.Runtime.Serialization;

namespace Assets.Script.AssetBundle.ReplaceInternalAssetHandler.Editor
{
    public class FileMetaInfo
    {
        public string guId;
        public string type;
        public string fileId;
    }
    public class FileReplaceInfo
    {
        public FileMetaInfo metaInfo;
        public string filePath;
    }
    public class FileReplaceReport
    {
        public string mainAssetPath;
        public FileReplaceInfo lastInfo;
        public FileReplaceInfo currentInfo;
    }
    class ReplaceInternalAssetTool
    {
        private string[] m_IgnoreSuffixList = new string[]
       {
            ".svn",
            ".git",
            ".meta",
            ".cs",
       };
        private string[] m_IgnoreAssetSuffixList = new string[]
        {
            ".ttf",
            ".fbx",
            ".anim",
        };
        private string m_strReplacedInternalAssetPath;
        private string m_strShaderZipPath;
        private Dictionary<string, AssetInfo> m_ShaderMap;
        private Dictionary<string, AssetInfo> m_MatMap;
        private List<UnityEngine.Object> m_AllInternalAsset;
        private Dictionary<string,UnityEngine.Object> m_AllInternalAssetMap;
        private List<FileReplaceReport> m_Reporter;

        public ReplaceInternalAssetTool()
        {
            BuildInternalAssetMap();
        }
        public List<FileReplaceReport> ReplaceInternalAssetToBuildInAsset(string dataPath, string internalAssetsPath,string shaderzipPath)
        {
            m_Reporter = new List<FileReplaceReport>();

            m_strShaderZipPath = shaderzipPath;
            m_strReplacedInternalAssetPath = internalAssetsPath;
            
            HandlerShader();

            BeginReplace(dataPath);

            return m_Reporter;
        }


        private void BeginReplace(string dataPath)
        {
            var dataPathPerfix = Application.dataPath.Substring(0, Application.dataPath.IndexOf("Assets"));
            var realPath = Application.dataPath + "/" + dataPath;
            var dir = new DirectoryInfo(realPath);
            var files = dir.GetFiles("*", SearchOption.AllDirectories);

            Dictionary<string, AssetInfo> allAssetsMap = new Dictionary<string, AssetInfo>();
            for (var i = 0; i < files.Length; ++i)
            {
                AssetInfo info = new AssetInfo(files[i].FullName);

                if (info.IsInSuffixList(m_IgnoreSuffixList))
                {
                    continue;
                }

                var deps = AssetDatabase.GetDependencies(info.GetRelativePath());
                foreach (var elem in deps)
                {
                    var assetInfo = new AssetInfo(dataPathPerfix + elem);
                    if (!allAssetsMap.ContainsKey(assetInfo.GetFullPath()))
                    {
                        allAssetsMap.Add(assetInfo.GetFullPath(), assetInfo);
                    }
                }
            }
            foreach (var elem in allAssetsMap)
            {
                var internalDepList = CheckIsDepInternalAssets(elem.Value);
                if (null == internalDepList)
                {
                    continue;
                }
                ReplaceDataPath(elem.Value, internalDepList);
            }
        }
        private void HandlerShader()
        {
            m_MatMap = new Dictionary<string, AssetInfo>();
            m_ShaderMap = new Dictionary<string, AssetInfo>();

            var dataPathPerfix = Application.dataPath.Substring(0, Application.dataPath.IndexOf("Assets"));
            var realPath = Application.dataPath + "/" + m_strReplacedInternalAssetPath;
            var dir = new DirectoryInfo(realPath);
            var files = dir.GetFiles("*", SearchOption.AllDirectories);
            foreach(var file in files)
            {
                AssetInfo info = new AssetInfo(file.FullName);
                if(info.GetFileSuffix() == ".shader")
                {
                    var relativePath = info.GetRelativePath();
                    var shader = AssetDatabase.LoadAssetAtPath<Shader>(relativePath);
                    if (string.IsNullOrEmpty(shader.name))
                    {
                        Debug.LogError(shader.name);
                    }
                    else
                    {
                        if (m_ShaderMap.ContainsKey(shader.name))
                        {
                            Debug.LogError(shader.name);
                        }
                        else
                        {
                            m_ShaderMap.Add(shader.name, info);
                        }
                    }
                }
                else if(info.GetFileSuffix() == ".mat")
                {
                    var mat = AssetDatabase.LoadAssetAtPath<Material>(info.GetRelativePath());
                    m_MatMap.Add(mat.name, info);
                }
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
        private List<string> CheckIsDepInternalAssets(AssetInfo info)
        {
            if (info.IsInSuffixList(m_IgnoreAssetSuffixList))
            {
                return null;
            }            var assets = EditorUtility.CollectDependencies(new UnityEngine.Object[] { AssetDatabase.LoadMainAssetAtPath(info.GetRelativePath()) });
            List<string> depList = new List<string>();
            foreach (var asset in assets)
            {
                if (asset == null)
                {
                    Debug.LogError(info.GetFullPath());
                    continue;
                }
                string path = AssetDatabase.GetAssetPath(asset) + '/' + asset.name;
                if (!path.StartsWith("Assets"))
                {
                    depList.Add(path);
                }
            }
            return depList;
        }
        private void ReplaceDataPath(AssetInfo info, List<string> depInternalAssets)
        {
            if(depInternalAssets.Count == 0)
            {
                return;
            }
            foreach(var elem in depInternalAssets)
            {
                //Debug.Log(elem + " " + info.GetFullPath());
                var assetName = FixInternalAssetPath(elem);
                if(m_ShaderMap.ContainsKey(assetName))
                {
                    // do replace
                    Debug.LogFormat("try Replace {0} to {1} ", elem, assetName);
                    DoReplace(info,elem,m_ShaderMap[assetName].GetFullPath(),typeof(Shader));
                }
                else if(m_MatMap.ContainsKey(assetName))
                {
                    // do replace
                    Debug.LogFormat("try Replace {0} to {1} ", elem, assetName);
                    DoReplace(info, elem, m_MatMap[assetName].GetFullPath(), typeof(Material));
                }
            }
        }
        private void DoReplace(AssetInfo mainAssets,string internalAssetPath,string newAssetPath, Type type)
        {
            var reporte = GenReport(mainAssets, internalAssetPath, newAssetPath,type);
            if (null == reporte)
            {
                return;
            }
            var file = File.ReadAllText(mainAssets.GetFullPath());

            //{fileID: 46, guid: 0000000000000000f000000000000000, type: 0}
            string tmpContent1 = string.Format("fileID: {0}, guid: {1}, type: {2}", 
                reporte.currentInfo.metaInfo.fileId, 
                reporte.currentInfo.metaInfo.guId, 
                reporte.currentInfo.metaInfo.type);
            string tmpContent2 = string.Format("fileID: {0}, guid: {1}, type: {2}",
                reporte.lastInfo.metaInfo.fileId,
                reporte.lastInfo.metaInfo.guId,
                reporte.lastInfo.metaInfo.type);

            file = file.Replace(tmpContent2, tmpContent1);

            File.WriteAllText(mainAssets.GetFullPath(), file);

            // add to reporter list
            m_Reporter.Add(reporte);
        }
        private FileReplaceReport GenReport(AssetInfo mainAssets, string internalAssetPath, string newAssetPath,Type type)
        {
            FileMetaInfo newInfo = new FileMetaInfo();
            AssetInfo newAssetInfo = new AssetInfo(newAssetPath);
            newInfo.guId = AssetDatabase.AssetPathToGUID(newAssetInfo.GetRelativePath());
            newInfo.fileId = AssetDatabase.LoadMainAssetAtPath(newAssetInfo.GetRelativePath()).GetFileID().ToString();
            newInfo.type = (type == typeof(Material)) ? "2" : "3";
            //newInfo.type =  "2" ;

            FileMetaInfo currentInfo = new FileMetaInfo();
            currentInfo.guId = GetInternalAssetsGUID(internalAssetPath);
            if (!m_AllInternalAssetMap.ContainsKey(internalAssetPath + type))
            {
                Debug.LogError("assets can't find at internal asset map " + internalAssetPath + type);
                return null;
            }
            currentInfo.fileId = m_AllInternalAssetMap[internalAssetPath+ type].GetFileID().ToString();
            currentInfo.type = "0";

            FileReplaceReport reporter = new FileReplaceReport();
            reporter.currentInfo = new FileReplaceInfo();
            reporter.currentInfo.filePath = newAssetPath;
            reporter.currentInfo.metaInfo = newInfo;

            reporter.lastInfo = new FileReplaceInfo();
            reporter.lastInfo.filePath = internalAssetPath;
            reporter.lastInfo.metaInfo = currentInfo;

            reporter.mainAssetPath = mainAssets.GetFullPath();

            return reporter;
        }
        private void BuildInternalAssetMap()
        {
            m_AllInternalAssetMap = new Dictionary<string, UnityEngine.Object>();
            m_AllInternalAsset = new List<UnityEngine.Object>();
            UnityEngine.Object[] UnityAssets = null;
            string[] paths = new string[] { "Resources/unity_builtin_extra", "Library/unity default resources" };

            foreach(var path in paths)
            {
                UnityAssets = AssetDatabase.LoadAllAssetsAtPath(path);
                m_AllInternalAsset.AddRange(UnityAssets);
                foreach (var elem in UnityAssets)
                {
                    var key = path + "/" + elem.name + elem.GetType();
                    if (m_AllInternalAssetMap.ContainsKey(key))
                    {
                        Debug.LogError(key);
                    }
                    else
                    {
                        m_AllInternalAssetMap.Add(key, elem);
                    }
                }
            }
        }
        private string GetInternalAssetsGUID(string path)
        {
            string internalPathPerfix = "Resources/unity_builtin_extra";
            if (path.StartsWith(internalPathPerfix))
            {
                return AssetDatabase.AssetPathToGUID(internalPathPerfix);
            }
            internalPathPerfix = "Library/unity default resources";
            if (path.StartsWith(internalPathPerfix))
            {
                return AssetDatabase.AssetPathToGUID(internalPathPerfix);
            }
            return string.Empty;
        }
        private string FixInternalAssetPath(string path)
        {
            string internalPathPerfix = "Resources/unity_builtin_extra/";
            if(path.StartsWith(internalPathPerfix))
            {
                return path.Replace(internalPathPerfix, "");
            }
            internalPathPerfix = "Library/unity default resources/";
            if (path.StartsWith(internalPathPerfix))
            {
                return path.Replace(internalPathPerfix, "");
            }
            return string.Empty;
        }
        private void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        byte[] file = new byte[255];
                        int size = 0;
                        int index = 0;
                        do
                        {
                            size = decompressedFileStream.Read(file, index, 255);
                            decompressedFileStream.Write(file, index, size);
                            index += size;
                        }
                        while (size != 0);
                    }
                }
            }
        }
    }
}
