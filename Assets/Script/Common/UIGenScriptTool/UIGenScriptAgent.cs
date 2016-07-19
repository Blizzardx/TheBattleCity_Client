using System;
using System.Collections.Generic;
using System.Text;
using Common.Tool;
using UnityEngine;

public class UIGenScriptAgent : MonoBehaviour
{
    public string m_strClassName;
    public string m_strFileType;
    public string m_strTemplateFilePath;
    public string m_strTargetFilePath;

    public List<UIGenScriptInfo> m_InfoList;
    public List<GameObject> m_PrefabList;

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
        for (int i = 0; i < m_PrefabList.Count; ++i)
        {
            try
            {
                AutoGenScript(templateFileContent, m_PrefabList[i]);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
    private void AutoGenScript(string templateFile, GameObject prefab)
    {
        StringBuilder content = new StringBuilder(templateFile);
        StringBuilder headContent = new StringBuilder();
        StringBuilder bodyContent = new StringBuilder();

        CheckObject(ref headContent, ref bodyContent, prefab.transform);

        string className = m_strClassName.Replace("{0}", prefab.name);
        className = FixNameToUpper(className);

        content = content.Replace("{0}", className);
        content = content.Replace("{1}", headContent.ToString());
        content = content.Replace("{2}", bodyContent.ToString());

        FileUtils.WriteStringFile(m_strTargetFilePath + "/" + className + m_strFileType, content.ToString());
    }
    private void CheckObject(ref StringBuilder headContent, ref StringBuilder bodyContent, Transform root)
    {
        for (int i = 0; i < root.childCount; ++i)
        {
            CheckObject(ref headContent, ref bodyContent, root.GetChild(i));
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