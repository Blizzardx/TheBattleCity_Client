using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Framework.MoudleCore.UI
{
    public class ViewManagerMultiWindow
    {
        private UIWindowController  m_WindowController;
        private UIStateManager      m_StateManager;

        public ViewManagerMultiWindow()
        {
            TryInit();
        }
        public void TryInit()
        {
            if (null != m_StateManager)
            {
                return;
            }
            m_WindowController = new UIWindowController();
            m_StateManager = new UIStateManager();
        }
        public void OpenState()
        {
            
        }
        public void BackState()
        {
            
        }
        public void OpenWindow()
        {
            
        }

        public void HideWindow()
        {
            
        }

        public void CloseWindow()
        {
            
        }
    }
}
