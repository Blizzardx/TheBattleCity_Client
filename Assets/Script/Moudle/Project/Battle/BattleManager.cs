using System.Collections.Generic;
using Framework.Common;
using Framework.Event;
using Framework.Network;
using Framework.Tick;
using NetWork.Auto;
using UnityEngine;

public class BattleManager
{
    public enum UseFrameStatus
    {
        Wait,
        Exec,
        Skip,
    }
    private const int m_iMinExeFrameCount = 8;
    private const int m_iMaxExeFrameCount = 12;
    private const int m_iExeLogicFrameSpace = 30;
    
    private List<BattlePlayerController>    m_PlayerList;
    private Queue<BattleFrameData>          m_LogicFrameQueue = new Queue<BattleFrameData>();
    private UseFrameStatus                  m_FrameStatus;
    private float   m_fDuringTime   = -1.0f;
    private float   m_fLastTime;
    private int     m_iClientFrame = 0;
    private BattleCmdAnalysisManager m_CmdAnayzerMgr = new BattleCmdAnalysisManager();
    private BattleCmdHandlerManager m_CmdHandlerMgr = new BattleCmdHandlerManager();

    #region public interface
    public void Initialize()
    {
        m_CmdAnayzerMgr.Initialize();
        m_CmdHandlerMgr.Initialize();

        m_fDuringTime = 0;
        m_FrameStatus = UseFrameStatus.Wait;
        CustomTickTask.Instance.RegisterToUpdateList(Update);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BeginLoadBattle, OnRecieveBeginLoadBattle);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleBegin,OnRecieveBattleBegin);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleEnd, OnRecieveBattleEnd);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleLogicFrame, OnRecieveLogicFrame);
        
    }
    public void InitBattleScene()
    {
        m_PlayerList = new List<BattlePlayerController>();
        var list = ModelManager.Instance.GetModel<RoomModel>().GetPlayerInfoList();
        for (int i = 0; i < list.Count; ++i)
        {
            m_PlayerList.Add(new BattlePlayerController(list[i]));
        }
    }
    public void SendCmd(BattleCmdInfo cmd)
    {
        var realCmd = m_CmdAnayzerMgr.EncodeBattleCmd(cmd);
        if (null == realCmd)
        {
            Debug.LogError("can't encode cmd " + cmd.GetType());
            return;
        }
        CSBattleLogicFrame msg = new CSBattleLogicFrame();
        msg.CommandData = new List<BattleCommandData>(realCmd);
        NetworkManager.Instance.SendMsgToServer(msg);
    }
    public int GetClientFrame()
    {
        return m_iClientFrame;
    }
    #endregion

    #region event
    private void OnRecieveLogicFrame(EventElement obj)
    {
        OnRecieveCmd(obj.eventParam as SCBattleLogicFrame);
    }
    private void OnRecieveBattleEnd(EventElement obj)
    {
        Distructor();
    }
    private void OnRecieveBattleBegin(EventElement obj)
    {
        BattleBegin();
    }
    private void OnRecieveBeginLoadBattle(EventElement obj)
    {
        BeginLoadBattle();
    }
    #endregion

    #region system function
    private void Distructor()
    {
        CustomTickTask.Instance.UnRegisterFromUpdateList(Update);

    }
    private void OnRecieveCmd(SCBattleLogicFrame msg)
    {
        foreach (var elem in msg.BattleFrameDataList)
        {
            m_LogicFrameQueue.Enqueue(elem);
        }
    }
    private void BeginLoadBattle()
    {
        SceneManager.Instance.LoadScene<SceneOnlineBattle>();
    }
    private void BattleBegin()
    {
        
    }
    private void LogicUpdate(BattleFrameData cmd)
    {
        ++ m_iClientFrame;
        for (int i = 0; i < cmd.CharCommandList.Count; ++i)
        {
            var cmdList = cmd.CharCommandList[i].CommandList;
            var cmdInfoList = m_CmdAnayzerMgr.DecodeCmd(cmd.CharCommandList[i].CharId,cmdList);
            m_CmdHandlerMgr.HandlerCmd(cmdInfoList);
        }
    }
    private void Update()
    {
        ExecLogicFrame();
    }
    private void ExecLogicFrame()
    {
        switch (m_FrameStatus)
        {
            case UseFrameStatus.Wait:
                LogicFrameWait();
                break;
            case UseFrameStatus.Exec:
                LogicFrameExec();
                break;
            case UseFrameStatus.Skip:
                LogicFrameSkip();
                break;
        }
    }

    private void LogicFrameWait()
    {
        if (m_LogicFrameQueue.Count >= m_iMinExeFrameCount)
        {
            m_FrameStatus = UseFrameStatus.Exec;
        }
    }
    private void LogicFrameExec()
    {
        float thisTime = Time.realtimeSinceStartup;
        if (m_fLastTime < 0)
        {
            m_fLastTime = thisTime;
        }
        m_fDuringTime = (1000.0f)*(thisTime - m_fLastTime);
        if (m_fDuringTime < m_iExeLogicFrameSpace)
        {
            return;
        }
        m_fLastTime += (0.001f)*m_iExeLogicFrameSpace;
        Debug.Log(" logic frame time " + thisTime*1000.0f);

        if (m_LogicFrameQueue.Count > m_iMaxExeFrameCount)
        {
            m_FrameStatus = UseFrameStatus.Skip;
        }

        var cmd = m_LogicFrameQueue.Dequeue();
        LogicUpdate(cmd);

        if (m_LogicFrameQueue.Count == 0)
        {
            m_FrameStatus = UseFrameStatus.Wait;
        }
    }
    private void LogicFrameSkip()
    {
        do
        {
            var cmd = m_LogicFrameQueue.Dequeue();
            LogicUpdate(cmd);

        } while (m_LogicFrameQueue.Count > m_iMinExeFrameCount);
        m_FrameStatus = UseFrameStatus.Exec;
    }
    #endregion

}