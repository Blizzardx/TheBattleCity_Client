using System;
using Framework.Common;
using Framework.Event;
using UnityEngine;

class BattleCmdHandlerSpeed : BattleCmdHandlerBase
{
    public Type GetHandlerInfoType()
    {
        return typeof (BattleCmdInfo_Speed);
    }

    public void HandleCmd(BattleCmdInfo info)
    {
        // to do
        BattleCmdInfo_Speed speedInfo = info as BattleCmdInfo_Speed;
        EventDispatcher.Instance.Broadcast(EventIdDefine.BattleCmdSpeed, speedInfo);
    }
}