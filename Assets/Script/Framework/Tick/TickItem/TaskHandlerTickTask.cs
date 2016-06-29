using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Task;

namespace Framework.Tick
{
    class TaskHandlerTickTask : AbstractTickTask
    {
        protected override bool FirstRunExecute()
        {
            return true;
        }

        protected override int GetTickTime()
        {
            return 0;
        }

        protected override void Beat()
        {
            TaskManager.Instance.Update();
        }
    }
}
