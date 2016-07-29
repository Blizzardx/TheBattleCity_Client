using System;

public  interface BattleCmdHandlerBase
{
    Type GetHandlerInfoType();

    void HandleCmd(BattleCmdInfo info);
}