namespace Framework.Task.Handler
{
    public class TaskHandler_5_DownloadtoMem : TaskHandlerBase
    {
        private Param m_Param;
        public class Param
        {
            public string url;
            public string filePath;
            public byte[] downloadContent;
        }
        public override void OnPrepare()
        {
            m_Param = m_Task.GetTaskParam() as Param;
        }
        public override void OnExec()
        {
            // begin download & write content to mem

        }
        public override void OnEnd()
        {

        }
    }
}