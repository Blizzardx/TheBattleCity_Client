using Common.Tool;

namespace Framework.Task.Handler
{
    public class TaskHandler_1_DelFile : TaskHandlerBase
    {
        public override void OnPrepare()
        {
            
        }
        public override void OnExec()
        {
            FileUtils.DeleteFile(m_Task.GetTaskParam() as string);
        }
        public override void OnEnd()
        {
        }
    }
}
