using UnityEngine;
using System.Collections;
using Framework.Message;

namespace Framework.Tick
{
    public class MessageTickTask : AbstractTickTask
    {
        protected override bool FirstRunExecute()
        {
            return true;
        }

        protected override int GetTickTime()
        {
            return TickTaskConstant.TICK_MESSAGE;
        }

        protected override void Beat()
        {
            MessageDispatcher.Instance.Update();
        }
    }
}