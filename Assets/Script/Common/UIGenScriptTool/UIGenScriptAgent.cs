using System;
using System.Collections.Generic;
using System.Text;
using Common.Tool;
using UnityEngine;

[System.Serializable]
public class UIGenScriptCustomRootInfo
{
    public string m_strCustomRootPerfix;
    public string m_strClassName;
    public string m_strFileType;
    public string m_strTemplateFilePath;
    public string m_strTargetFilePath;
    public string m_strNameSpace;
}
public class UIGenScriptAgent : MonoBehaviour
{
    public string m_strClassName;
    public string m_strFileType;
    public string m_strTemplateFilePath;
    public string m_strTargetFilePath;
    public string m_strNameSpace;
    public List<UIGenScriptInfo>            m_InfoList;
    public List<GameObject>                 m_PrefabList;
    public List<UIGenScriptCustomRootInfo>  m_CustomRootInfo;

    [ContextMenu("Execute")]
    public void Run()
    {
        AutoGenScript();
    }
    public void AutoGenScript()
    {
        // load template file
        var templateFileContent = FileUtils.ReadStringFile(m_strTemplateFilePath);

        if (string.IsNullOrEmpty(templateFileContent))
        {
            return;
        }
        // add namespace if need
        if (!string.IsNullOrEmpty(m_strNameSpace))
        {
            if (templateFileContent.IndexOf("{3}") >= 0)
            {
                templateFileContent = templateFileContent.Replace("{3}", m_strNameSpace);
            }
        }
        for (int i = 0; i < m_PrefabList.Count; ++i)
        {
            try
            {
                string className = m_strClassName.Replace("{0}", m_PrefabList[i].name);
                className = FixNameToUpper(className);
                AutoGenScript(false,templateFileContent, m_PrefabList[i].transform,className,m_strTargetFilePath,m_strFileType);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
    private void AutoGenScript(bool isCustomRoot, string templateFile, Transform root,string className,string outputPath,string filetype)
    {
        StringBuilder content = new StringBuilder(templateFile);
        StringBuilder headContent = new StringBuilder();
        StringBuilder bodyContent = new StringBuilder();

        CheckObject(true,isCustomRoot,ref headContent, ref bodyContent, root);

        content = content.Replace("{0}", className);
        content = content.Replace("{1}", headContent.ToString());
        content = content.Replace("{2}", bodyContent.ToString());

        FileUtils.WriteStringFile(outputPath + "/" + className + filetype, content.ToString());
    }
    private void CheckObject(bool isRoot ,bool isCustomRoot,ref StringBuilder headContent, ref StringBuilder bodyContent, Transform root)
    {
        int index = 0;
        if (isCustomRoot || !CheckIsCustomRoot(root,ref index))
        {
            for (int i = 0; i < root.childCount; ++i)
            {
                CheckObject(false,isCustomRoot,ref headContent, ref bodyContent, root.GetChild(i));
            }
        }
        else
        {
            GenCustomRootScript(m_CustomRootInfo[index], root);
        }
        if (isRoot)
        {
            return;
        }
        for (int i = 0; i < m_InfoList.Count; ++i)
        {
            if (root.name.StartsWith(m_InfoList[i].m_strResourceNamePerfix))
            {
                Debug.Log(root.name);
                m_InfoList[i].SetObjectName(root.name);
                string head = m_InfoList[i].GetHead();
                string body = m_InfoList[i].GetBody();
                if (string.IsNullOrEmpty(head) || string.IsNullOrEmpty(body))
                {
                    continue;
                }
                headContent.Append(head);
                headContent.Append("\n");
                bodyContent.Append(body);
                bodyContent.Append("\n");
                break;
            }
        }
    }
    private bool CheckIsCustomRoot(Transform root,ref int index)
    {
        for (int i = 0; i < m_CustomRootInfo.Count; ++i)
        {
            if (root.name.IndexOf(m_CustomRootInfo[i].m_strCustomRootPerfix) >= 0)
            {
                index = i;
                return true;
            }
        }
        return false;
    }
    private void GenCustomRootScript(UIGenScriptCustomRootInfo info,Transform root)
    {
        // load template file
        var templateFileContent = FileUtils.ReadStringFile(info.m_strTemplateFilePath);

        if (string.IsNullOrEmpty(templateFileContent))
        {
            return;
        }

        // add namespace if need
        if (!string.IsNullOrEmpty(info.m_strNameSpace))
        {
            if (templateFileContent.IndexOf("{3}") >= 0)
            {
                templateFileContent = templateFileContent.Replace("{3}", info.m_strNameSpace);
            }
        }

        string className = info.m_strClassName.Replace("{0}", root.name);
        className = FixNameToUpper(className);
        AutoGenScript(true,templateFileContent, root, className, info.m_strTargetFilePath, info.m_strFileType);
    }
    public static string FixNameToUpper(string name)
    {
        if (name.Length < 1)
        {
            return string.Empty;
        }
        string tmpName = "" + name[0];
        tmpName.ToUpper();
        tmpName = tmpName + name.Substring(1);
        return tmpName;
    }
}