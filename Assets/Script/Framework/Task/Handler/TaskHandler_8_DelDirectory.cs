using UnityEngine;
using System.Collections;
using System.IO;

namespace Framework.Task.Handler
{
    public class TaskHandler_8_DelDirectory : TaskHandlerBase
    {
        public override void OnPrepare()
        {
        }

        public override void OnExec()
        {
            string dir = m_Task.GetTaskParam() as string;
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }
        }

        public override void OnEnd()
        {
        }
    }
}