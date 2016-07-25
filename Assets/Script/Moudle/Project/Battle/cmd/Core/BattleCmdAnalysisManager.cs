using System;
using System.Collections.Generic;
using Common.Tool;
using NetWork.Auto;
using UnityEngine;


public class BattleCmdAnalysisManager 
{
    private Dictionary<Type, BattleCmdAnalysisBase> m_AnalyzerToInfoMap;
    private Dictionary<int, BattleCmdAnalysisBase> m_AnalyzerToTypeMap;
    public void Initialize()
    {
        if (null != m_AnalyzerToInfoMap)
        {
            return;
        }
        m_AnalyzerToInfoMap = new Dictionary<Type, BattleCmdAnalysisBase>();
        var list = ReflectionManager.Instance.GetTypeByBase(typeof(BattleCmdAnalysisBase));
        for (int i = 0; i < list.Count; ++i)
        {
            BattleCmdAnalysisBase analysis = Activator.CreateInstance(list[i]) as BattleCmdAnalysisBase;
            analysis.Init();
            m_AnalyzerToInfoMap.Add(analysis.GetDataInfoType(), analysis);
            m_AnalyzerToTypeMap.Add((int)(analysis.GetMessageInfoType()), analysis);
        }
    }
    public List<BattleCmdInfo> DecodeCmd(int charId,List<BattleCommandData> cmdList)
    {
        List<BattleCmdInfo> realList = new List<BattleCmdInfo>();
        for (int i = 0; i < cmdList.Count; ++i)
        {
            var cmd = cmdList[i];
            BattleCmdAnalysisBase analyzer = null;
            m_AnalyzerToTypeMap.TryGetValue(cmd.Type, out analyzer);
            if (null == analyzer)
            {
                Debug.LogError("can't decode cmd by type " + cmd.Type);
                continue;
            }
            BattleCmdInfo realCmd = null;
            try
            {
                realCmd = analyzer.DecodeCmd(cmd);
            }
            catch (Exception e)
            {
                Debug.LogError("error on decode cmd  type: " + cmd.Type);
                Debug.LogException(e);
            }
            if (null == realCmd)
            {
                Debug.LogError("error on decode cmd  type: " + cmd.Type);
                continue;
            }
            realList.Add(realCmd);
        }
        return realList;
    }
    public BattleCommandData EncodeBattleCmd(BattleCmdInfo info)
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