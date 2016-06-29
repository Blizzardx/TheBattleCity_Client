using UnityEngine;
using System.Collections;

namespace Framework.Tick
{

    public class TimeTickTask : AbstractTickTask
    {
        protected override bool FirstRunExecute()
        {
            return true;
        }

        protected override int GetTickTime()
        {
            return TickTaskConstant.TICK_TIME_CORRECT;
        }

        protected override void Beat()
        {
            //TimeControl.Instance.Update();
        }
    }

}