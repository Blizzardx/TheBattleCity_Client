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
        var list = ReflectionManager.Instance.GetTypeByBase(typeof (TriggerOnAppInit));
        List<TriggerOnAppInit> instanceList = new List<TriggerOnAppInit>(list.Count);
        for (int i = 0; i < list.Count; ++i)
        {
            TriggerOnAppInit constructerInstance = Activator.CreateInstance(list[i])  as TriggerOnAppInit;
            instanceList.Add(constructerInstance);
        }
        instanceList.Sort(SortById);
        for (int i = 0; i < instanceList.Count; ++i)
        {
            instanceList[i].Init();
        }
    }

    private int SortById(TriggerOnAppInit x, TriggerOnAppInit y)
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