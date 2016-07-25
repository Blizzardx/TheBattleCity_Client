using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Tool;
using NetWork.Auto;
using UnityEngine;

public abstract class BattleCmdAnalysisBase
{
    private Dictionary<int, Action<BattleCommandData>> m_AnalyzerMap = new Dictionary<int, Action<BattleCommandData>>();
     
    protected void RegisterCmd(BattleCmdType type,Action<BattleCommandData> handler)
    {
        m_AnalyzerMap.Add((int)type,handler);
    }
    public bool CheckDecode(BattleCommandData cmd)
    {
        return m_AnalyzerMap.ContainsKey(cmd.Type);
    }
    public BattleCmdInfo DecodeCmd(BattleCommandData cmd)
    {
        m_AnalyzerMap[cmd.Type](cmd);
        return GetDataInfo();
    }
    public abstract BattleCommandData[] EncodeCmd(BattleCmdInfo param);
    public abstract BattleCmdInfo GetDataInfo();
    public abstract void Init();
    public abstract void ClearBuffer();
}