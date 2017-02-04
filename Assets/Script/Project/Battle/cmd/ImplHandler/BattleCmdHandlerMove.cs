using System;
using Framework.Common;
using Framework.Event;
using UnityEngine;

class BattleCmdHandlerMove : BattleCmdHandlerBase
{
    public Type GetHandlerInfoType()
    {
        return typeof (BattleCmdInfo_Move);
    }

    public void HandleCmd(BattleCmdInfo info)
    {
        // to do:
        BattleCmdInfo_Move moveInfo = info as BattleCmdInfo_Move;
        EventDispatcher.Instance.Broadcast(EventIdDefine.BattleCmdMove,moveInfo);
        //Debug.LogWarning(moveInfo.dir);
    }
}