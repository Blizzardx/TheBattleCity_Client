using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Network;

namespace Framework.Tick
{
    public class NetworkTickTask : AbstractTickTask
    {
        protected override bool FirstRunExecute()
        {
            return false;
        }

        protected override int GetTickTime()
        {
            return TickTaskConstant.TICK_NETWORK;
        }

        protected override void Beat()
        {
            NetworkManager.Instance.Update();
        }
    }
}