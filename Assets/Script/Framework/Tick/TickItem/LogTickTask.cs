using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Log;

namespace Framework.Tick
{
    public class LogTickTask : AbstractTickTask
    {
        protected override bool FirstRunExecute()
        {
            return true;
        }

        protected override int GetTickTime()
        {
            return 2000;
        }

        protected override void Beat()
        {
            LogManager.Instance.Update();
        }
    }

}