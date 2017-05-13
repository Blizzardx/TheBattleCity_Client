using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

class ProjectBuild : Editor
{

    //在这里找出你当前工程所有的场景文件，假设你只想把部分的scene文件打包 那么这里可以写你的条件判断 总之返回一个字符串数组。
    static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();

        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;
            if (e.enabled)
                names.Add(e.path);
        }
        return names.ToArray();
    }

    //得到项目的名称
    public static string projectName
    {
        get
        {
            //在这里分析shell传入的参数， 
            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("project"))
                {
                    return arg.Split("-"[0])[1];
                }
            }
            return "test";
        }
    }
    //shell脚本直接调用这个静态方法
    static public void BuildForIPhone()
    {
        //EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
        //DelUnusedStreamingAsset(RuntimePlatform.IPhonePlayer);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "USE_SHARE");
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), projectName, BuildTarget.iOS, BuildOptions.None);
    }
    static public void BuildiOS()
    {
        string outputPath = string.Empty;
        for (int i = 0; i < Environment.GetCommandLineArgs().Length; ++i)
        {
            string tmpArge = Environment.GetCommandLineArgs()[i];
            if (tmpArge.StartsWith("outputPath-"))
            {
                outputPath = tmpArge.Split('-')[1];
                Debug.Log("out put path : " + outputPath);
            }
        }
        if (string.IsNullOrEmpty(outputPath))
        {
            throw new Exception("Can't find output path in args");
        }
        //EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
        //DelUnusedStreamingAsset(RuntimePlatform.IPhonePlayer);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "USE_SHARE");
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), outputPath, BuildTarget.iOS, BuildOptions.None);
    }
    //shell脚本直接调用这个静态方法
    static public void BuildForAndroid()
    {
        // reset project setting befor build
        PlayerSettings.keystorePass = "123456";
        PlayerSettings.keyaliasPass = "123456";
        //EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        //DelUnusedStreamingAsset(RuntimePlatform.Android);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "USE_SHARE");
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), projectName, BuildTarget.Android, BuildOptions.None);
    }

    static public void BuildAndroid()
    {
        string outputPath = string.Empty;
        for (int i = 0; i < Environment.GetCommandLineArgs().Length; ++i)
        {
            string tmpArge = Environment.GetCommandLineArgs()[i];
            if (tmpArge.StartsWith("outputPath-"))
            {
                outputPath = tmpArge.Split('-')[1];
                Debug.Log("out put path : " + outputPath);
            }
        }
        if (string.IsNullOrEmpty(outputPath))
        {
            throw new Exception("Can't find output path in args");
        }
        // reset project setting befor build
        PlayerSettings.keystorePass = "123456";
        PlayerSettings.keyaliasPass = "123456";
        //EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        //DelUnusedStreamingAsset(RuntimePlatform.Android);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "USE_SHARE");
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), outputPath, BuildTarget.Android, BuildOptions.None);
    }
    static void BuildAndroidUseIL2CPP()
    {
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        BuildAndroid();
    }
    static void BuildAndroidDevelopment()
    {
        string outputPath = string.Empty;
        for (int i = 0; i < Environment.GetCommandLineArgs().Length; ++i)
        {
            string tmpArge = Environment.GetCommandLineArgs()[i];
            if (tmpArge.StartsWith("outputPath-"))
            {
                outputPath = tmpArge.Split('-')[1];
                Debug.Log("out put path : " + outputPath);
            }
        }
        if (string.IsNullOrEmpty(outputPath))
        {
            throw new Exception("Can't find output path in args");
        }
        // reset project setting befor build
        PlayerSettings.keystorePass = "123456";
        PlayerSettings.keyaliasPass = "123456";
        //EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        //DelUnusedStreamingAsset(RuntimePlatform.Android);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "USE_SHARE");
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), outputPath, BuildTarget.Android, BuildOptions.ConnectWithProfiler | BuildOptions.Development | BuildOptions.AllowDebugging);
    }
    static void DelUnusedStreamingAsset(RuntimePlatform keepTarget)
    {
        RuntimePlatform[] tmpList = new[] { RuntimePlatform.IPhonePlayer, RuntimePlatform.Android, RuntimePlatform.WindowsPlayer, RuntimePlatform.OSXPlayer };
        for (int i = 0; i < tmpList.Length; ++i)
        {
            var platform = tmpList[i];

            if (platform == keepTarget)
            {
                continue;
            }
            string path = Application.streamingAssetsPath + "/AssetBundles/" + GetPlatformFolderForAssetBundles(platform);
            DelAllFilesInDirectory(path);
        }
    }
    public static string GetPlatformFolderForAssetBundles(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                return "Windows";
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.OSXEditor:
                return "OSX";
            // Add more build platform for your own.
            // If you add more platforms, don't forget to add the same targets to GetPlatformFolderForAssetBundles(BuildTarget) function.
            default:
                return null;
        }
    }
    public static void DelAllFilesInDirectory(string path)
    {
        Debug.Log("Del " + path);
        if (!Directory.Exists(path))
        {
            return;
        }
        var files = Directory.GetFiles(path);
        for (int i = 0; i < files.Length; ++i)
        {
            Debug.Log("Del " + files[i]);
            File.Delete(files[i]);
        }
    }

    static void PreBuildSetting()
    {
    }

}

