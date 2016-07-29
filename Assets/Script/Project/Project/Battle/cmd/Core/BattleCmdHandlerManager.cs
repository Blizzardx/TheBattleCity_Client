using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BattleCmdHandlerManager
{
    private Dictionary<Type, BattleCmdHandlerBase> m_HandlerToInfoMap;

    public void Initialize()
    {
        if (null != m_HandlerToInfoMap)
        {
            return;
        }
        m_HandlerToInfoMap = new Dictionary<Type, BattleCmdHandlerBase>();
        var list = ReflectionManager.Instance.GetTypeByBase(typeof(BattleCmdHandlerBase));
        for (int i = 0; i < list.Count; ++i)
        {
            BattleCmdHandlerBase handler = Activator.CreateInstance(list[i]) as BattleCmdHandlerBase;
            m_HandlerToInfoMap.Add(handler.GetHandlerInfoType(), handler);
        }
    }
    public void HandlerCmd(List<BattleCmdInfo> cmdList)
    {
        for (int i = 0; i < cmdList.Count; ++i)
        {
            BattleCmdInfo info = cmdList[i];
            BattleCmdHandlerBase handler = null;
            m_HandlerToInfoMap.TryGetValue(info.GetType(), out handler);
            if (null == handler)
            {
                Debug.LogError("Can't decode cmd " + info.GetType());
                continue;
            }
            try
            {
                handler.HandleCmd(info);
            }
            catch (Exception e)
            {
                Debug.LogError("error on handler cmd " + info.GetType());
                Debug.LogException(e);
            }

        }
    }
}