using System;
using System.Collections.Generic;
using Common.Tool;

public class HandlerManager : Singleton<HandlerManager>
{
    private Dictionary<Type, HandlerBase> m_HandlerStore;

    private void AutoRegister()
    {
        var list = ReflectionManager.Instance.GetTypeByBase(typeof(HandlerBase));
        for (int i = 0; i < list.Count; ++i)
        {
            var elem = list[i];
            HandlerBase modelInstance = Activator.CreateInstance(elem) as HandlerBase;
            modelInstance.Create();
            m_HandlerStore.Add(elem, modelInstance);
        }
    }
    public T GetHandler<T>() where T : HandlerBase
    {
        CheckInit();
        HandlerBase res = null;
        m_HandlerStore.TryGetValue(typeof(T), out res);
        return res as T;
    }
    public void CheckInit()
    {
        if (null != m_HandlerStore)
        {
            return;
        }
        m_HandlerStore = new Dictionary<Type, HandlerBase>();
        AutoRegister();
    }
    public void Destroy()
    {
        foreach (var elem in m_HandlerStore)
        {
            elem.Value.Create();
        }
        m_HandlerStore = null;
    }
}
