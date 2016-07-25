using System;
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
        
        Debug.LogWarning(moveInfo.dir);
    }
}