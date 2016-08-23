namespace Framework.MoudleCore.UI
{
    public class UIWindowManagerSingle_Tips
    {
        public enum Layer
        {
            Tips,
            Tip1,
            Tip2,
            Tip3,
        }

        private ViewManagerSingleWindow m_Controller;

        public UIWindowManagerSingle_Tips()
        {
            m_Controller = new ViewManagerSingleWindow();
        }

        public void Open<T>(object param = null, Layer layer = Layer.Tips) where T : UIWindowBase
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