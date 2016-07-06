using System;
using Common.Tool;
using System.Collections.Generic;
using System.Reflection;

public class ReflectionManager : Singleton<ReflectionManager>
{
    private Dictionary<string, Type> m_ClassFindMap;
    private List<Type> m_ClassList;
 
    private bool m_bIsInit;
    public ReflectionManager()
    {
        //
        CheckInit();
    }
    public void CheckInit()
    {
        if (m_bIsInit)
        {
            return;
        }
        m_ClassFindMap = new Dictionary<string, Type>();
        Assembly assem = Assembly.GetAssembly(typeof(ReflectionManager));
        m_ClassList=new List<Type>(assem.GetTypes());

        for (int i = 0; i < m_ClassList.Count; ++i)
        {
            var elem = m_ClassList[i];
            if (!m_ClassFindMap.ContainsKey(elem.Name))
            {
                m_ClassFindMap.Add(elem.Name, elem);
            }
        }
    }
    public List<Type> GetTypeByBase(Type baseType)
    {
        List<Type> resList = new List<Type>();
        for (int i = 0; i < m_ClassList.Count; ++i)
        {
            var elem = m_ClassList[i];
            if (elem.BaseType == baseType)
            {
                // add to list
                resList.Add(elem);
            }
            else if (!elem.IsInterface && !elem.IsAbstract && baseType.IsInterface && baseType.IsAssignableFrom(elem))
            {
                // add to list
                resList.Add(elem);
            }
        }
        return resList;
    }
}
