using System;
using System.Collections.Generic;
using NetWork.Auto;

class BattleCmdAnalysisSpeed : BattleCmdAnalysisBase
{
     public void Init()
     {
     }

    public BattleCmdInfo DecodeCmd(BattleCommandData cmd)
    {
        BattleCmdInfo_Speed info = new BattleCmdInfo_Speed();

        info.Speed = BattleTool.ConvertIntToFloat(cmd.Argvs[0]);

        return info;
    }

    public BattleCommandData EncodeCmd(BattleCmdInfo info)
    {
        if (!(info is BattleCmdInfo_Speed))
        {
            return null;
        }
        BattleCmdInfo_Speed battleCmdInfo = info as BattleCmdInfo_Speed;
        BattleCommandData cmd = new BattleCommandData();
        cmd.Type = (int)(BattleCmdType.Speed);
        cmd.Argvs = new List<int>(1);
        cmd.Argvs.Add(BattleTool.ConvertFloatToInt(battleCmdInfo.Speed));

        return cmd;
    }

    public Type GetDataInfoType()
    {
        return typeof (BattleCmdInfo_Speed);
    }

    public BattleCmdType GetMessageInfoType()
    {
       return BattleCmdType.Speed;
    }
}