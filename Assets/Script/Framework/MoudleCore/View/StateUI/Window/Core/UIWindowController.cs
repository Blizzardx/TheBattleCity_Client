using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Framework.MoudleCore.UI
{
    public class UIWindowController
    {
        private LinkedList<UIWindowBase> m_ActivedWindowList;

        #region public interface

        public UIWindowController()
        {
            m_ActivedWindowList = new LinkedList<UIWindowBase>();
        }
        public UIWindowBase Open<T>(object param,int deepth = 0) where T : UIWindowBase
        {
            return Open(typeof (T), param,deepth);
        }
        public UIWindowBase Open(Type type, object param,int deepth = 0)
        {
            // create window instance
            UIWindowBase window = Activator.CreateInstance(type) as UIWindowBase;
            if (null == window)
            {
                Debug.LogError("Can't open window by type " + type);
                return null;
            }
            // open window
            Open(window, param, deepth);
            return window;
        }
        public void Open(UIWindowBase window, object param, int deepth = 0)
        {
            if (null == window)
            {
                Debug.LogError("Can't open window ");
                return;
            }
            // do init
            window.Init(param, deepth);

            // do open
            window.Open(param);

            // add to active window map
            var node = m_ActivedWindowList.First;
            while (node != null)
            {
                if (node.Value.GetDeepth() > deepth)
                {
                    break;
                }
                node = node.Next;
            }
            if (null == node)
            {
                m_ActivedWindowList.AddLast(window);
            }
            else
            {
                m_ActivedWindowList.AddBefore(node, window);
            }
            // reset deepth
            RefreshDeepth();
        }
        public void Hide(UIWindowBase window)
        {
            if (null == window)
            {
                Debug.LogError("can't hide window");
                return;
            }
            // do hide
            window.Hide();
            // remove from active window map
            m_ActivedWindowList.Remove(window);
            // reset deepth
            RefreshDeepth();
        }
        public void Close(UIWindowBase window)
        {
            if (null == window)
            {
                Debug.LogError("can't close window");
                return;
            }
            // do hide
            window.Close();
            // remove from active window map
            m_ActivedWindowList.Remove(window);
            // reset deepth
            RefreshDeepth();
        }
        #endregion

        #region system fucntion
        protected void RefreshDeepth()
        {
            UIWindowBase lastWindow = null;
            foreach (var elem in m_ActivedWindowList)
            {
                elem.ResetDeepth(lastWindow);
                lastWindow = elem;
            }
        }
        #endregion
    }
}