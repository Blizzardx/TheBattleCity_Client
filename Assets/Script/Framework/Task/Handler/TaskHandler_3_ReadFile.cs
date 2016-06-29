using System;
using Common.Tool;

namespace Framework.Task.Handler
{
    public class TaskHandler_3_ReadFile : TaskHandlerBase
    {
        private Param m_Param;

        public class Param
        {
            public string filePath;
            public object fileContent;
            public Type contentType;
        }
        public override void OnPrepare()
        {
            m_Param = m_Task.GetTaskParam() as Param;
        }
        public override void OnExec()
        {
            if (m_Param.contentType == typeof (byte[]))
            {
                m_Param.fileContent = FileUtils.ReadByteFile(m_Param.filePath);
            }
            else if (m_Param.contentType == typeof (string))
            {
                m_Param.fileContent = FileUtils.ReadStringFile(m_Param.filePath);
            }
            else
            {
                throw new Exception("wrong file content type");
            }
        }
        public override void OnEnd()
        {
            
        }
    }
}
