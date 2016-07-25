using System;
using System.Collections.Generic;
using Common.Tool;
using NetWork.Auto;


public class BattleCmdAnalysisManager 
{
    private List<BattleCmdAnalysisBase> m_CmdHanderList;
    private Dictionary<Type, BattleCmdAnalysisBase> m_AnalyzerToInfoMap;
    public void Initialize()
    {
        if (null != m_AnalyzerToInfoMap)
        {
            return;
        }
        m_AnalyzerToInfoMap = new Dictionary<Type, BattleCmdAnalysisBase>();
        var list = ReflectionManager.Instance.GetTypeByBase(typeof(BattleCmdAnalysisBase));
        m_CmdHanderList = new List<BattleCmdAnalysisBase>(list.Count);
        for (int i = 0; i < list.Count; ++i)
        {
            BattleCmdAnalysisBase analysis = Activator.CreateInstance(list[i]) as BattleCmdAnalysisBase;
            m_CmdHanderList.Add(analysis);
            analysis.Init();
            m_AnalyzerToInfoMap.Add(analysis.GetDataInfo().GetType(), analysis);
        }
    }
    public List<BattleCmdInfo> DecodeCmd(int charId,List<BattleCommandData> cmdList)
    {
        List<BattleCmdInfo> realList = new List<BattleCmdInfo>();
        BattleCmdAnalysisBase lastCmd = null;
        for (int i = 0; i < cmdList.Count; ++i)
        {
            var cmd = cmdList[i];
            foreach (var handler in m_CmdHanderList)
            {
                if (handler.CheckDecode(cmd))
                {
                    if (lastCmd != handler)
                    {
                        if (null != lastCmd)
                        {
                            lastCmd.GetDataInfo().SetCharId(charId);
                            realList.Add(lastCmd.GetDataInfo());
                        }
                        handler.ClearBuffer();
                        lastCmd = handler;
                    }
                    handler.DecodeCmd(cmd);
                    break;
                }
            }
        }
        return realList;
    }
    public BattleCommandData[] EncodeBattleCmd(BattleCmdInfo info)
    {
        Type type = info.GetType();
        BattleCmdAnalysisBase analyzer = null;
        m_AnalyzerToInfoMap.TryGetValue(type, out analyzer);
        if (null != analyzer)
        {
            return analyzer.EncodeCmd(info);
        }
        return null;
    }
}