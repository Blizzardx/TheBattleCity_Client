using System.Text;
using UnityEngine;
[System.Serializable]
public class UIGenScriptInfo
{
    public enum MemberType
    {
        Public,
        Private,
        Protected,
    }
    public string m_strResourceNamePerfix;
    public string m_strClassTypeName;
    public string m_strClassMemberName;
    public MemberType m_MemberType;
    public string m_strFindFunctionName;
    private string m_strObjectName;

    public string GetHead()
    {
        StringBuilder head = new StringBuilder();
        head.Append('\t');
        head.Append('\t');

        switch (m_MemberType)
        {
            case MemberType.Private:
                head.Append("private ");
                break;
            case MemberType.Protected:
                head.Append("protected ");
                break;
            case MemberType.Public:
                head.Append("public ");
                break;
        }
        head.Append(m_strClassTypeName);
        string objNameWithoutPrefix = m_strObjectName.Substring(m_strResourceNamePerfix.Length);
        if (string.IsNullOrEmpty(objNameWithoutPrefix))
        {
            Debug.LogWarning("error node name " + m_strObjectName);
            return string.Empty;
        }
        objNameWithoutPrefix = UIGenScriptAgent.FixNameToUpper(objNameWithoutPrefix);
        StringBuilder memberName = new StringBuilder(m_strClassMemberName);
        memberName = memberName.Replace("{0}", objNameWithoutPrefix);
        head.Append(" " + memberName);
        head.Append(";");

        return head.ToString();
    }
    public string GetBody()
    {
        StringBuilder body = new StringBuilder();
        body.Append('\t');
        body.Append('\t');

        string objNameWithoutPrefix = m_strObjectName.Substring(m_strResourceNamePerfix.Length);
        if (string.IsNullOrEmpty(objNameWithoutPrefix))
        {
            Debug.LogWarning("error node name " + m_strObjectName);
            return string.Empty;
        }
        objNameWithoutPrefix = UIGenScriptAgent.FixNameToUpper(objNameWithoutPrefix);
        StringBuilder memberName = new StringBuilder(m_strClassMemberName);
        memberName = memberName.Replace("{0}", objNameWithoutPrefix);

        body.Append(memberName + " = ");
        StringBuilder findFunc = new StringBuilder(m_strFindFunctionName);
        if (findFunc.ToString().IndexOf("{0}") >= 0)
        {
            findFunc = findFunc.Replace("{0}", m_strClassTypeName);
        }
        if (findFunc.ToString().IndexOf("{1}") >= 0)
        {
            findFunc = findFunc.Replace("{1}", m_strObjectName);
        }
        body.Append(findFunc);
        body.Append(";");

        return body.ToString();
    }
    public void SetObjectName(string prefabName)
    {
        m_strObjectName = prefabName;
    }
}