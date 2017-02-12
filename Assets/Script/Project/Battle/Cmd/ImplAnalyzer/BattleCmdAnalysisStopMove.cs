using System.Collections.Generic;
using NetWork.Auto;
using UnityEngine;
using System;

public class BattleCmdAnalysisStopMove : BattleCmdAnalysisBase
{
    public void Init()
    {
        // do nothing
    }

    public BattleCmdInfo DecodeCmd(BattleCommandData cmd)
    {
        BattleCmdInfo_StopMove info = new BattleCmdInfo_StopMove();

        return info;
    }

    public BattleCommandData EncodeCmd(BattleCmdInfo info)
    {
        if (!(info is BattleCmdInfo_StopMove))
        {
            return null;
        }
        BattleCmdInfo_StopMove battleCmdInfoMove = info as BattleCmdInfo_StopMove;
        BattleCommandData cmd = new BattleCommandData();
        cmd.Type = (int)(BattleCmdType.StopMove);

        return cmd;
    }

    public Type GetDataInfoType()
    {
        return typeof(BattleCmdInfo_StopMove);
    }

    public BattleCmdType GetMessageInfoType()
    {
        return BattleCmdType.StopMove;
    }
}