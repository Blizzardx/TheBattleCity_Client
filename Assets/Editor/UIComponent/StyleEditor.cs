using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor class used to view panels.
/// </summary>


[CustomEditor(typeof (StyleLib))]
public class StyleEditor : UIRectEditor
{
    public StyleLib m_StyleElement = null;

    protected override void OnEnable()
    {

        try
        {
            m_StyleElement = target as StyleLib;

            base.OnEnable();
        }
        catch
        {
        }

    }
    protected override void DrawFinalProperties()
    {
        if (GUILayout.Button(@"生成样式表"))
        {
            CreateStyleFromPrefab();
        }
    }
    private void CreateStyleFromPrefab()
    {
        string result = "", temp = "", tempKey, enumStr = "", constStr = "";

        GameObject GO = m_StyleElement.gameObject;
        if (GO == null)
        {
            EditorUtility.DisplayDialog("DONE", "StylesLib不在层级中", "OK");
            return;
        }
        //
        enumStr += "None,\n";
        foreach (Transform child in GO.transform)
        {
            tempKey = child.name;
            temp = UILabelPlus.Serialize(child.gameObject.GetComponent<UILabel>());
            if (!string.IsNullOrEmpty(temp))
            {
                enumStr += tempKey + ",\n";
                //dictStr += "{\"" + tempKey + "\"," + tempKey + "},\n";
                temp = temp.Replace("\"", "\\\"");
                constStr += "\n public const string  " + tempKey + "= \" " + temp + "\";  \n";

            }
        }
        result += "/*this file is created by style tool,pls do NOT edit it*/\n\n\n";
        result += "  using System.Collections.Generic;\n";
        result += "  public class CSS {\n";
        result += "  public enum Styles{\n";
        result += enumStr + "\n";
        ;
        result += "  }\n\n";
        result += constStr + "\n}";


        StreamWriter sw = File.CreateText(Application.dataPath + "/Common/Script/Common/Tools/UI/Label/LabelStyleDefine.cs");
        sw.Write(result);
        sw.Flush();
        sw.Close();
        //DestroyImmediate(GO);
        EditorUtility.DisplayDialog("DONE",
            "已生成样式类 Common/Script/Common/Tools/UI/Label   LabelStyleDefine.cs  ,请右键 Scripts[refresh]使其立即生效", "OK");
    }
}