using UnityEngine;

namespace Common.Tool
{

    public static class SVNTool
    {
        private static int m_iReversion = -1;

        public static int GetReversion()
        {
            if (m_iReversion == -1)
            {
                m_iReversion = GetSvnBuildVersion();
            }
            return m_iReversion;
        }
        public static int GetSvnBuildVersion()
        {
            var file = Resources.Load<TextAsset>("BuildInfo");
            if (file != null && !string.IsNullOrEmpty(file.text))
            {
                Debug.Log(file.text);
                return GetReversion(file.text);
            }
            return -1;
        }
        public static int GetReversion(string content)
        {
            int index = content.LastIndexOf("Revision: ");
            index += 10;
            int index2 = content.LastIndexOf("Node Kind: ");
            string tmpContent = content.Substring(index, index2 - index);
            int revision = int.Parse(tmpContent);
            Debug.Log("revision : " + revision);
            return revision;
        }
    }
}
