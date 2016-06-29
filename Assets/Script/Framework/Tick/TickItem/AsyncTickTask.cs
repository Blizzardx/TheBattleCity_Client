using Framework.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Tick
{
    public class AsyncTickTask : AbstractTickTask
    {
        protected override bool FirstRunExecute()
        {
            return false;
        }

        protected override int GetTickTime()
        {
            return TickTaskConstant.TICK_ASYNC;
        }

        protected override void Beat()
        {
            AsyncManager.Update();
        }
    }

}

