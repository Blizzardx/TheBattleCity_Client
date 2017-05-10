using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorCommon 
{
    //print persisten data path
    [MenuItem("Editors/Common/打印配置中心目录")]
    private static void PrintDataPath()
    {
        Debug.Log(Application.persistentDataPath);
    }
    [MenuItem("Editors/Common/打开配置中心")]
    private static void OpenDataPath()
    {
        EditorUtility.OpenFilePanel("", Application.persistentDataPath, "");
    }
     [MenuItem("Editors/Common/打开资源目录")]
    private static void OpenActionDataPath()
    {
        EditorUtility.OpenFilePanel("", Application.dataPath + "/EditorCommon/EditorResources/mmAdv/1.0/", "");
    }
    [MenuItem("Editors/Common/删除配置中心")]
    private static void ClearDataPath()
    {
        Directory.Delete(Application.persistentDataPath, true);
    }
    [MenuItem("Editors/Common/删除空文件夹")]
    public static void DelEmptyFolder()
    {
        DelEmptyFolder(new DirectoryInfo(Application.dataPath));
    }

    private static void DelEmptyFolder(DirectoryInfo directory)
    {
        if (directory.FullName.StartsWith((Application.dataPath + "\\OhterPlugins").Replace('/', '\\')) ||
            directory.FullName.StartsWith((Application.dataPath + "\\Plugins").Replace('/','\\')))
        {
            return;
        }

        DirectoryInfo folder = directory;
        FileInfo[] files = folder.GetFiles();
        if (files.Length == 0)
        {
            Debug.Log("Del : " + directory.FullName);
            Directory.Delete(directory.FullName);
            File.Delete(directory.FullName + ".meta");
            return; 
        }
        DirectoryInfo[] folders = folder.GetDirectories();
        for (int i = 0; i < folders.Length; ++i)
        {
            DelEmptyFolder(folders[i]);
        }

    }
}
