using System;
using Framework.Common;
using Framework.Event;

class BattleCmdHanlderStopMove : BattleCmdHandlerBase
{
    public Type GetHandlerInfoType()
    {
        return typeof(BattleCmdInfo_StopMove);
    }

    public void HandleCmd(BattleCmdInfo info)
    {
        // to do:
        BattleCmdInfo_StopMove moveInfo = info as BattleCmdInfo_StopMove;
        EventDispatcher.Instance.Broadcast(EventIdDefine.BattleCmdStopMove, moveInfo);
        //Debug.LogWarning(moveInfo.dir);
    }
}