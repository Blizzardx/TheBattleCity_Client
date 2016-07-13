using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Tool;
using Framework.Common;

public class TriggerOnAppInitManager:Singleton<TriggerOnAppInitManager>
{
    public void Init()
    {
        var list = ReflectionManager.Instance.GetTypeByBase(typeof (SystemEventTrigger));
        List<SystemEventTrigger> instanceList = new List<SystemEventTrigger>(list.Count);
        for (int i = 0; i < list.Count; ++i)
        {
            SystemEventTrigger constructerInstance = Activator.CreateInstance(list[i])  as SystemEventTrigger;
            instanceList.Add(constructerInstance);
        }
        instanceList.Sort(SortById);
        for (int i = 0; i < instanceList.Count; ++i)
        {
            instanceList[i].Init();
        }
    }

    private int SortById(SystemEventTrigger x, SystemEventTrigger y)
    {
        if (x.GetSortId() < y.GetSortId())
        {
            return -1;
        }
        else if (x.GetSortId() > y.GetSortId())
        {
            return 1;
        }
        return 0;
    }
}