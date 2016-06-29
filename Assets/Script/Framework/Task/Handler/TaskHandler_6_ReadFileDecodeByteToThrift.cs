using System;
using Common.Tool;
using Communication;
using Thrift.Protocol;

namespace Framework.Task.Handler
{
    public class TaskHandler_6_ReadFileDecodeByteToThrift:TaskHandlerBase
    {
        private Param m_Param;
        public class Param
        {
            public string filePath;
            public TBase tbase;
            public Type type;
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
            // load file to mem
            byte[] file = FileUtils.ReadByteFile(m_Param.filePath);
            m_Param.tbase = Activator.CreateInstance(m_Param.type) as TBase;
            ThriftSerialize.DeSerialize(m_Param.tbase, file);
        }
        public override void OnEnd()
        {
            // do noting
        }
    }
}