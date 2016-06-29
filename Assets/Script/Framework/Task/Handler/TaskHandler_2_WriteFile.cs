using System;
using Common.Tool;

namespace Framework.Task.Handler
{
    public class TaskHandler_2_WriteFile : TaskHandlerBase
    {
        private Param m_Param;
        public class Param
        {
            public string filePath;
            public object fileContent;
        }
        public override void OnPrepare()
        {
            m_Param = m_Task.GetTaskParam() as Param;
        }
        public override void OnExec()
        {
            if (m_Param.fileContent is string)
            {
                FileUtils.WriteStringFile(m_Param.filePath, m_Param.fileContent as string);
            }
            else if(m_Param.fileContent is byte[])
            {
                FileUtils.WriteByteFile(m_Param.filePath,m_Param.fileContent as byte[]);
            }
            else
            {
                throw new Exception("file content type error"); ;
            }
        }
        public override void OnEnd()
        {
        }
    }
}
