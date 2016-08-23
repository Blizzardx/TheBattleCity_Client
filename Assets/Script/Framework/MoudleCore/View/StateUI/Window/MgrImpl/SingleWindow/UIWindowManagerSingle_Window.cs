namespace Framework.MoudleCore.UI
{
    public class UIWindowManagerSingle_Window
    {
        public enum Layer
        {
            Common,
            Top1,
            Top2,
            Top3,
        }

        private ViewManagerSingleWindow m_Controller;

        public UIWindowManagerSingle_Window()
        {
            m_Controller = new ViewManagerSingleWindow();
        }

        public void Open<T>(object param = null, Layer layer = Layer.Common) where T : UIWindowBase
        {
            m_Controller.Open<T>(param, (int) layer);
        }

        public void Hide<T>() where T : UIWindowBase
        {
            m_Controller.Hide<T>();
        }

        public void Close<T>() where T : UIWindowBase
        {
            m_Controller.Close<T>();
        }
    }
}