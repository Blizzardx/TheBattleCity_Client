using System;

class BattleCmdHandlerSpeed : BattleCmdHandlerBase
{
    public Type GetHandlerInfoType()
    {
        return typeof (BattleCmdInfo_Speed);
    }

    public void HandleCmd(BattleCmdInfo info)
    {
        // to do
    }
}