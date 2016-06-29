using System;
using Common.Tool;
using Communication;
using Thrift.Protocol;

namespace Framework.Task.Handler
{
    public class TaskHandler_7_WriteFileEncodeThriftToByte:TaskHandlerBase
    {
        private Param m_Param;
        public class Param
        {
            public string   filePath;
            public TBase    tbase;
        }
        public override void OnPrepare()
        {
            var data = m_Task.GetTaskParam();
            if (!(data is Param))
            {
                m_ErrorException = new Exception("wrong param type");
                
                throw m_ErrorException;
            }
            m_Param = data as Param;
        }
        public override void OnExec()
        {
            byte[] data = ThriftSerialize.Serialize(m_Param.tbase);
            FileUtils.DeleteFile(m_Param.filePath);
            FileUtils.WriteByteFile(m_Param.filePath, data);
        }
        public override void OnEnd()
        {
            // do noting
        }
    }
}