using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.MoudleCore.UI
{
    public class ViewManagerSingleWindow
    {
        private Dictionary<Type, UIWindowBase>  m_WindowStore;
        private UIWindowController              m_WindowController;

        #region public interface
        public ViewManagerSingleWindow()
        {
            m_WindowStore = new Dictionary<Type, UIWindowBase>();
            m_WindowController = new UIWindowController();
        }
        public void Open<T>(object param = null,int layer = 0) where T : UIWindowBase
        {
            Open(typeof (T), param,(int)layer);
        }
        public void Hide<T>() where T : UIWindowBase
        {
            Hide(typeof(T));   
        }
        public void Close<T>() where T : UIWindowBase
        {
            Close(typeof(T));
        }
        #endregion

        #region system function
        protected void Open(Type type, object param,int deepth)
        {
            UIWindowBase window = null;
            m_WindowStore.TryGetValue(type, out window);
            if (null == window)
            {
                window = m_WindowController.Open(type, param, deepth);
                // add to store
                m_WindowStore.Add(type, window);
            }
            else
            {
                m_WindowController.Open(window, param, deepth);
            }
        }
        protected void Hide(Type type)
        {
            UIWindowBase window = null;
            m_WindowStore.TryGetValue(type, out window);
            if (null == window)
            {
                Debug.LogError("Can't hide window");
                return;
            }
            m_WindowController.Hide(window);
        }
        protected void Close(Type type)
        {
            UIWindowBase window = null;
            m_WindowStore.TryGetValue(type, out window);
            if (null == window)
            {
                Debug.LogError("Can't close window");
                return;
            }
            // remove from store
            m_WindowStore.Remove(type);
            // do close
            m_WindowController.Close(window);
        }
        #endregion
    }


}
