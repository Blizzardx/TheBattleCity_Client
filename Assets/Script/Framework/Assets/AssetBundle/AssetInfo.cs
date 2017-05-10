using UnityEngine;
using System.IO;


public class AssetInfo
{
    #region property
    private string m_strFullPath;
    private string m_strRelativePath;
    private string m_strFileName;
    private string m_strFileSuffix;
    private string m_strFullPathWithoutSuffix;
    private string m_strRelativePathWithoutSuffix;
    private string m_strFileNameWithoutSuffix;
    #endregion

    #region public interface
    public AssetInfo(FileInfo info)
    {
        Initialize(info.FullName);
    }
    public AssetInfo(string fullPath)
    {
        Initialize(fullPath);
    }
    public bool IsInSuffixList(string[] suffixList)
    {
        for (int i = 0; i < suffixList.Length; ++i)
        {
            if (m_strFullPath.ToLower().EndsWith(suffixList[i].ToLower()))
            {
                return true;
            }
        }
        return false;
    }
    public bool IsInRelativePerfixList(string[] perfixList)
    {
        for (int i = 0; i < perfixList.Length; ++i)
        {
            if (m_strRelativePath.ToLower().StartsWith(perfixList[i].ToLower()))
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region get
    public string GetFullPath()
    {
        return m_strFullPath;
    }
    public string GetRelativePath()
    {
        return m_strRelativePath;
    }
    public string GetFileName()
    {
        return m_strFileName;
    }
    public string GetFileSuffix()
    {
        return m_strFileSuffix;
    }
    public string GetFullPathWithoutSuffix()
    {
        return m_strFullPathWithoutSuffix;
    }
    public string GetRelativePathWithoutSuffix()
    {
        return m_strRelativePathWithoutSuffix;
    }
    public string GetFileNameWithoutSuffix()
    {
        return m_strFileNameWithoutSuffix;
    }
    #endregion

    #region system function
    private void Initialize(string fullPath)
    {
        m_strFullPath = fullPath.Replace('\\', '/');
        int index = m_strFullPath.IndexOf("Assets/");
        if (index == -1)
        {
            Debug.LogWarning("wrong file name " + fullPath);
            return;
        }
        m_strRelativePath = m_strFullPath.Substring(index);

        index = m_strFullPath.LastIndexOf('/');
        if (index == -1)
        {
            Debug.LogWarning("wrong file name " + fullPath);
            return;
        }
        m_strFileName = m_strFullPath.Substring(index + 1);

        index = m_strFullPath.LastIndexOf('.');
        if (index == -1)
        {
            m_strFileSuffix = string.Empty;
        }
        else
        {
            m_strFileSuffix = m_strFullPath.Substring(index);
        }
        InitWithSuffix();
    }
    private void InitWithSuffix()
    {
        if (string.IsNullOrEmpty(m_strFileSuffix))
        {
            m_strFullPathWithoutSuffix = m_strFullPath;
            m_strRelativePathWithoutSuffix = m_strRelativePath;
            m_strFileNameWithoutSuffix = m_strFileName;
        }
        else
        {
            int length = m_strFullPath.Length - m_strFileSuffix.Length;
            if (length <= 0)
            {
                Debug.LogError("CheckName " + m_strFullPath);
            }
            else
            {
                m_strFullPathWithoutSuffix = m_strFullPath.Substring(0, length);
            }

            length = m_strRelativePath.Length - m_strFileSuffix.Length;
            if (length <= 0)
            {
                Debug.LogError("CheckName " + m_strFullPath);
            }
            else
            {
                m_strRelativePathWithoutSuffix = m_strRelativePath.Substring(0, length);
            }

            length = m_strFileName.Length - m_strFileSuffix.Length;
            if (length <= 0)
            {
                Debug.LogError("CheckName " + m_strFullPath);
            }
            else
            {
                m_strFileNameWithoutSuffix = m_strFileName.Substring(0, length);
            }
        }
    }
    #endregion
}