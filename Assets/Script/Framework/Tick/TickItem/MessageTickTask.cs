using UnityEngine;
using System.Collections;

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
        MessageManager.Instance.Update();
    }
}
