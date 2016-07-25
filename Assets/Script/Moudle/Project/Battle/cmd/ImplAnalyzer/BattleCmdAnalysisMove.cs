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
        MoveInfo info = new MoveInfo();

        info.dir = new Vector3(
            BattleTool.ConvertIntToFloat(cmd.Argvs[0]),// x
            BattleTool.ConvertIntToFloat(cmd.Argvs[1]),// t
            BattleTool.ConvertIntToFloat(cmd.Argvs[2]) // z
            );
        info.speed = BattleTool.ConvertIntToFloat(cmd.Argvs[3]);

        return info;
    }

    public BattleCommandData EncodeCmd(BattleCmdInfo info)
    {
        if (!(info is MoveInfo))
        {
            return null;
        }
        MoveInfo moveInfo = info as MoveInfo;
        BattleCommandData cmd = new BattleCommandData();
        cmd.Type = (int) (BattleCmdType.Move);
        cmd.Argvs = new List<int>(4);
        cmd.Argvs[0] = BattleTool.ConvertFloatToInt(moveInfo.dir.x);
        cmd.Argvs[1] = BattleTool.ConvertFloatToInt(moveInfo.dir.y);
        cmd.Argvs[2] = BattleTool.ConvertFloatToInt(moveInfo.dir.z);
        cmd.Argvs[3] = BattleTool.ConvertFloatToInt(moveInfo.speed);
        
        return cmd;
    }

    public Type GetDataInfoType()
    {
        return typeof (MoveInfo);
    }

    public BattleCmdType GetMessageInfoType()
    {
        return BattleCmdType.Move;
    }
}