using NetWork.Auto;
using UnityEngine;

public class BattleCmdAnalysisMove : BattleCmdAnalysisBase
{
    private MoveInfo m_MoveInfo;
    public override void Init()
    {
        RegisterCmd(BattleCmdType.Move_X, Move_X);
        RegisterCmd(BattleCmdType.Move_Y, Move_Y);
        RegisterCmd(BattleCmdType.Move_Z, Move_Z);
        RegisterCmd(BattleCmdType.Move_Speed, Move_Speed);
    }
    public override void ClearBuffer()
    {
        m_MoveInfo = new MoveInfo();
    }
    public override BattleCommandData[] EncodeCmd(BattleCmdInfo param)
    {
        if (!(param is MoveInfo))
        {
            return null;
        }
        MoveInfo info = param as MoveInfo;
        BattleCommandData[] data = new BattleCommandData[4];
        data[0].Type = (int)(BattleCmdType.Move_X);
        data[1].Argv = BattleTool.ConvertFloatToInt(info.dir.x);

        data[1].Type = (int)(BattleCmdType.Move_Y);
        data[1].Argv = BattleTool.ConvertFloatToInt(info.dir.y);

        data[2].Type = (int)(BattleCmdType.Move_Z);
        data[1].Argv = BattleTool.ConvertFloatToInt(info.dir.z);

        data[3].Type = (int)(BattleCmdType.Move_Speed);
        data[1].Argv = BattleTool.ConvertFloatToInt(info.speed);
        return data;
    }
    public override BattleCmdInfo GetDataInfo()
    {
        return m_MoveInfo;
    }
    private void Move_X(BattleCommandData obj)
    {
        m_MoveInfo.dir.x = BattleTool.ConvertIntToFloat(obj.Argv);
    }
    private void Move_Y(BattleCommandData obj)
    {
        m_MoveInfo.dir.y = BattleTool.ConvertIntToFloat(obj.Argv);
    }
    private void Move_Z(BattleCommandData obj)
    {
        m_MoveInfo.dir.z = BattleTool.ConvertIntToFloat(obj.Argv);
    }
    private void Move_Speed(BattleCommandData obj)
    {
        m_MoveInfo.speed = BattleTool.ConvertIntToFloat(obj.Argv);
    }
}