using System;
using System.Collections.Generic;
using NetWork.Auto;
using UnityEngine;

public class BattleCmdAnalysisMove : BattleCmdAnalysisBase
{
    public void Init()
    {
        // do nothing
    }

    public BattleCmdInfo DecodeCmd(BattleCommandData cmd)
    {
        BattleCmdInfo_Move info = new BattleCmdInfo_Move();

        info.dir = new Vector3(
            BattleTool.ConvertIntToFloat(cmd.Argvs[0]),// x
            BattleTool.ConvertIntToFloat(cmd.Argvs[1]),// t
            BattleTool.ConvertIntToFloat(cmd.Argvs[2]) // z
            );

        return info;
    }

    public BattleCommandData EncodeCmd(BattleCmdInfo info)
    {
        if (!(info is BattleCmdInfo_Move))
        {
            return null;
        }
        BattleCmdInfo_Move battleCmdInfoMove = info as BattleCmdInfo_Move;
        BattleCommandData cmd = new BattleCommandData();
        cmd.Type = (int) (BattleCmdType.Move);
        cmd.Argvs = new List<int>(3);
        cmd.Argvs.Add(BattleTool.ConvertFloatToInt(battleCmdInfoMove.dir.x));
        cmd.Argvs.Add(BattleTool.ConvertFloatToInt(battleCmdInfoMove.dir.y));
        cmd.Argvs.Add(BattleTool.ConvertFloatToInt(battleCmdInfoMove.dir.z));
        
        return cmd;
    }

    public Type GetDataInfoType()
    {
        return typeof (BattleCmdInfo_Move);
    }

    public BattleCmdType GetMessageInfoType()
    {
        return BattleCmdType.Move;
    }
}