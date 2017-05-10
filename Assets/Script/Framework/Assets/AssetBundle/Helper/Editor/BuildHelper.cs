using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.AssetBundle.BuildAssetbundleTool.Editor;
using UnityEditor;
using Assets.Script.AssetBundle.InternalAssetHandler;
using Assets.Script.AssetBundle.ReplaceInternalAssetHandler.Editor;
using System.Reflection;

public class BuildHelper
{
    [MenuItem("BuildAssetbundle/Build")]
    static void BuildAB()
    {
        BuildAssetbundleTool tool = new BuildAssetbundleTool();
       var e = tool.Build("Data", "Pack", "UGUI", "StreamingAssets/AssetsBundle");
        if(null != e)
        {
            Debug.LogException(e);
        }
        Debug.Log("Done");
    }
    [MenuItem("BuildAssetbundle/ForceBuild")]
    static void BuildABForce()
    {
        var output = "Assets/StreamingAssets/AssetsBundle";
        var res = BuildPipeline.BuildAssetBundles
       (output,
       BuildAssetBundleOptions.DeterministicAssetBundle |
       //  BuildAssetBundleOptions.UncompressedAssetBundle |
       BuildAssetBundleOptions.StrictMode,
       EditorUserBuildSettings.activeBuildTarget);
       
        if (null == res)
        {
            Debug.LogError("Build with error");
        }
        Debug.Log("Done");
    }
    [MenuItem("BuildAssetbundle/ExportInternalAssets")]
    static void UnpackInternalAssets()
    {
        InternalAssetsExportTool tool = new InternalAssetsExportTool();
        tool.ExportInternalAsset("InternalAssets");
        //AssetInfo info = new AssetInfo("E:\\Project\\unityProjcet\\ABPacker\\Assets\\Art\\tmp 1.mat");
        //var assets = EditorUtility.CollectDependencies(new Object[] { AssetDatabase.LoadMainAssetAtPath(info.GetRelativePath()) });
        //foreach (var asset in assets)
        //{
        //    Debug.Log(AssetDatabase.GetAssetPath(asset) + '/' + asset.name);
        //}
        //var deps = AssetDatabase.GetDependencies(info.GetRelativePath());
        //foreach (var asset in deps)
        //{
        //    Debug.Log(asset);
        //}
    }
    [MenuItem("BuildAssetbundle/AutoReplaceInternalAssetsWithCleanup")]
    static void ReplaceInternalAssetsWithCleanup()
    {
        var internalAssetsExportPath = "InternalAssets";
        if (Directory.Exists(Application.dataPath + "/" + internalAssetsExportPath))
        {
            Directory.Delete(Application.dataPath + "/" + internalAssetsExportPath, true);
        }
        Directory.CreateDirectory(Application.dataPath + "/" + internalAssetsExportPath);
        // copy build in shader to target folder
        var subpath = Application.dataPath.Substring(0, Application.dataPath.IndexOf("Assets")) + internalAssetsExportPath;
        CopyFileOrDirectory(subpath + "/", Application.dataPath + "/" + internalAssetsExportPath + "/");

        // refresh and reload
        Refresh();

        // export default material to datapath
        InternalAssetsExportTool ExportTool = new InternalAssetsExportTool();
        ExportTool.ExportInternalAsset("InternalAssets");

        Refresh();

        ReplaceInternalAssetTool tool = new ReplaceInternalAssetTool();
        // replace default mat to use 
        tool.ReplaceInternalAssetToBuildInAsset("InternalAssets", "InternalAssets", "");

        Refresh();

        tool = new ReplaceInternalAssetTool();
        // replace other asset to use replaced assets     
        tool.ReplaceInternalAssetToBuildInAsset("Data", "InternalAssets", "");

        Refresh();
    }
    [MenuItem("BuildAssetbundle/ReplaceInternalAssets")]
    static public void ReplaceInternalAssets()
    {
        var tool = new ReplaceInternalAssetTool();
        // replace other asset to use replaced assets     
        tool.ReplaceInternalAssetToBuildInAsset("Data", "InternalAssets", "");

        Refresh();
    }

    #region tool
    static void Refresh()
    {
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        AssetDatabase.SaveAssets();
    }
    static void CopyFileOrDirectory(string from,string to)
    {
        var dir = new DirectoryInfo(from);
        var files = dir.GetFiles("*", SearchOption.AllDirectories);
        foreach(var file in files)
        {
            var relatePath = file.FullName.Substring(from.Length);
            var targetPath = to + relatePath;
            EnsureFolderByFilePath(targetPath);
            File.Copy(file.FullName, targetPath);
        }
    }
    static void EnsureFolderByFilePath(string filepath)
    {
        filepath = filepath.Replace('\\', '/');
        filepath = filepath.Substring(0, filepath.LastIndexOf('/'));

        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }
    }
    #endregion
}
