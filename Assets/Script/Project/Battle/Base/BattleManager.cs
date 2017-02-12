using System.Collections.Generic;
using Framework.Common;
using Framework.Event;
using Framework.Network;
using Framework.Tick;
using NetWork.Auto;
using UnityEngine;

public class BattleManager
{
    private List<PlayerController>          m_PlayerList;
    private Queue<BattleFrameData>          m_LogicFrameQueue = new Queue<BattleFrameData>();
    private BattleCmdAnalysisManager        m_CmdAnayzerMgr = new BattleCmdAnalysisManager();
    private BattleCmdHandlerManager         m_CmdHandlerMgr = new BattleCmdHandlerManager();
    private int                             m_iClientFrame = 0;
    private static BattleManager            m_Instance;

    #region public interface
    public BattleManager()
    {
        m_Instance = this;
    }
    public void Initialize()
    {
        m_CmdAnayzerMgr.Initialize();
        m_CmdHandlerMgr.Initialize();
        CustomTickTask.Instance.RegisterToUpdateList(Update);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BeginLoadBattle, OnRecieveBeginLoadBattle);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleBegin,OnRecieveBattleBegin);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleEnd, OnRecieveBattleEnd);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleLogicFrame, OnRecieveLogicFrame);
    }
    public void InitBattleScene()
    {
        m_PlayerList = new List<PlayerController>();
        var list = ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetPlayerInfoList();
        for (int i = 0; i < list.Count; ++i)
        {
            m_PlayerList.Add(new PlayerController(list[i]));
        }
        m_PlayerList.Sort(SortByUid);
    }
    private int SortByUid(PlayerController x, PlayerController y)
    {
        if(x.GetUid() > y.GetUid())
        {
            return 1;
        }
        if (x.GetUid() < y.GetUid())
        {
            return -1;
        }
        return 0;
    }
    public static void SendBattleCmd(BattleCmdInfo cmd)
    {
        m_Instance.SendCmd(cmd);
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
        msg.CommandData = realCmd;
        NetworkManager.Instance.SendMsgToServer(msg);
    }
    public static int GetClientFrame()
    {
        return m_Instance.m_iClientFrame;
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
            //Debug.Log(" frame " + elem.FrameIndex + " : count " + elem.CharCommandList.Count);
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
    private void BattleFrameUpdate()
    {
        for (int i = 0; i < m_PlayerList.Count; ++i)
        {
            m_PlayerList[i].OnCmdUpdate();
        }
    }
    #endregion

    #region frame control
    private const int m_iMinExeFrameCount = 4;
    private const int m_iMaxExeFrameCount = 8;
    private const float m_iExeLogicFrameSpace = 0.02f;
    private float m_fLastExecFrameTime;

    private void Update()
    {
        ExecLogicFrame();
    }
    private void ExecLogicFrame()
    {
        if (m_LogicFrameQueue.Count < m_iMinExeFrameCount)
        {
            Debug.LogWarning("Waitting " + m_LogicFrameQueue.Count);
            return;
        }
        if (m_LogicFrameQueue.Count > m_iMaxExeFrameCount)
        {
            Debug.LogWarning("Skiping " + m_LogicFrameQueue.Count);
            while (m_LogicFrameQueue.Count >= m_iMaxExeFrameCount)
            {
                LogicUpdate(m_LogicFrameQueue.Dequeue());
            }
            m_fLastExecFrameTime = Time.realtimeSinceStartup;
            return;
        }
        if (Time.realtimeSinceStartup - m_fLastExecFrameTime >= m_iExeLogicFrameSpace)
        {
            m_fLastExecFrameTime = Time.realtimeSinceStartup;
            Debug.LogWarning("Normal " + m_LogicFrameQueue.Count);
            LogicUpdate(m_LogicFrameQueue.Dequeue());
        }
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
        BattleFrameUpdate();
    }
    #endregion
}