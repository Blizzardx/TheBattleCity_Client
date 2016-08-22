using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public class UIStateManager
{
    public class StateInfo
    {
        public UIStateBase handler;
        public object param;
        public Type type;
    }

    private Dictionary<Type, int>           m_StateIndexMap;
    private Dictionary<string, StateInfo>   m_StateCashe;
    private LinkedList<StateInfo>           m_StateStack;

    #region public interface
    public void TryInit()
    {
        if (null != m_StateIndexMap)
        {
            return;
        }
        m_StateIndexMap = new Dictionary<Type, int>();
        m_StateCashe = new Dictionary<string, StateInfo>();
        m_StateStack = new LinkedList<StateInfo>();
        HashSet<int> tmpSet = new HashSet<int>();
        var list = ReflectionManager.Instance.GetTypeByBase(typeof (UIStateBase));
        for (int i = 0; i < list.Count; ++i)
        {
            int id = PraseTypeToId(list[i]);
            m_StateIndexMap.Add(list[i],id);
            if (tmpSet.Contains(id))
            {
                Debug.LogError("state id already exist " + list[i].ToString());
            }
            else
            {
                tmpSet.Add(id);
            }
        }
    } 
    public void OpenStage<T>(object param, bool isJump = false, bool isClear = false) where T : UIStateBase
    {
        OpenStage(typeof (T),param,isJump,isClear);
    }
    public void BackStage(bool isClearRes = false)
    {
        // pop
        StateInfo info = m_StateStack.First.Value;
        if (null == m_StateStack.First.Value)
        {
            return;
        }
        // remove from stack
        m_StateStack.RemoveFirst();
        if (isClearRes)
        {
            // remove from cashe
            RemoveFromCashe(info);
        }
        else
        {
            info.handler.Hide();
        }
        info = m_StateStack.First.Value;
        if (null != info)
        {
            if (info.handler != null)
            {
                // resume
                info.handler.Resume();
            }
            else
            {
                // remove
                m_StateStack.RemoveFirst();
                // reopen
                OpenStage(info.type, info.param);
            }
        }
    }
    public void OpenWindow(UIWindowBase window,object param)
    {
        StateInfo info = m_StateStack.First.Value;
        if (null == info)
        {
            return;
        }
        info.handler.OpenWindow(window, param);
    }
    #endregion

    #region system function
    protected void OpenStage(Type type,object param,bool isJump = false,bool isClear = false)
    {
        TryInit();
        var currentTop = m_StateStack.First.Value;
        if (null != currentTop)
        {
            // override 
            currentTop.handler.Cover();
        }

        // clear stack if jump
        if (isJump)
        {
            ClearStack();
        }

        // try get stage from cashe
        string key = PraseStakeToId() + PraseTypeToId(type);
        StateInfo info = null;
        m_StateCashe.TryGetValue(key, out info);

        if (null == info || info.handler == null)
        {
            info = new StateInfo();
            info.param = param;
            info.handler = Activator.CreateInstance(type) as UIStateBase;
            info.type = type;
            m_StateCashe.Add(key, info);

            // create new & initialize
            info.handler.Init(param,key);
        }

        // do open
        info.param = param;
        info.handler.Open(param);

        // check clear
        if (isClear)
        {
            ReleaseStackResource();
        }
        // add to stack
        m_StateStack.AddFirst(info);
    }
    private void ReleaseStackResource()
    {
        // do clear
        foreach (var elem in m_StateStack)
        {
            // remove from cashe
            RemoveFromCashe(elem);
            elem.handler = null;
        }
    }
    private void ClearStack()
    {
        // do clear
        foreach (var elem in m_StateStack)
        {
            // remove from cashe
            RemoveFromCashe(elem);
        }
        m_StateStack.Clear();
    }
    private void RemoveFromCashe(StateInfo info)
    {
        info.handler.Close();
        m_StateCashe.Remove(info.handler.GetKey());
    }
    private string PraseStakeToId()
    {
        StringBuilder s = new StringBuilder();
        foreach (var elem in m_StateStack)
        {
            s.Append(m_StateIndexMap[elem.handler.GetType()]);
        }
        return s.ToString();
    }
    private int PraseTypeToId(Type type)
    {
        string classname = type.Name;
        var list = classname.Split('_');
        int id = -1;
        if (list != null && list.Length >= 1)
        {
            int.TryParse(list[1], out id);
        }
        if (id == -1)
        {
            Debug.LogError("error on parse state type by name " + classname);
        }
        return id;
    }
    #endregion
}
