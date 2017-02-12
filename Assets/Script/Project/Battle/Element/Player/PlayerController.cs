using System.Runtime.InteropServices;
using Common.Component;
using Framework.Common;
using Framework.Event;
using UnityEngine;

public class PlayerCmd
{
    public PlayerCmd()
    {
        m_bDir = false;
        m_bSpeed = false;
    }

    private Vector3 m_vDir;
    public bool     m_bDir;
    public Vector3 Dir
    {
        get
        {
            return m_vDir;
        }
        set
        {
            m_bDir = true;
            m_vDir = value;
        }
    }

    private float m_fSpeed;
    public bool m_bSpeed;
    public float Speed
    {
        get
        {
            return m_fSpeed;
        }
        set
        {
            m_bDir = true;
            m_fSpeed = value;
        }
    }
}
public class PlayerController
{
    private PlayerCom m_ObjRoot;
    private PlayerInfo m_PlayerInfo;
    private TemplateQueue<PlayerCmd> m_CmdQueue;
    private float m_fSpeed;
    private bool m_bIsMoving;
    
    #region system

    public int GetUid()
    {
        return m_PlayerInfo.Uid;
    }
    public PlayerController(PlayerInfo playerInfo)
    {
        m_CmdQueue = new TemplateQueue<PlayerCmd>();
        m_CmdQueue.Initialize(false);

        m_PlayerInfo = playerInfo;
        var posList = GameObject.FindObjectsOfType<PlayerPosCom>();
        for (int i = 0; i < posList.Length; ++i)
        {
            if (posList[i].m_Id == playerInfo.positionId)
            {
                m_ObjRoot = posList[i].GetComponentInChildren<PlayerCom>();
                if (null == m_ObjRoot)
                {
                    Debug.LogError("can't load player com at pos " + posList[i].name);
                }
                break;
            }
        }
        
        // self control event
        if (ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetSelf().Uid == playerInfo.Uid)
        {
            EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleSelfMove, OnSendSelfMove);
            EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleSelfStopMove, OnSendSelfStopMove);
            // mark camera
            var camControl = GameObject.FindObjectOfType<CameraFollow>();
            camControl.SetTarget(m_ObjRoot.transform);
        }
        // battle cmd event
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleCmdMove, OnCmdMove);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleCmdSpeed, OnCmdSpeed);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleCmdStopMove, OnCmdStopMove);

        m_fSpeed = 0.03f;
    }
    public void OnCmdUpdate()
    {
        var playercmd = m_CmdQueue.Dequeue();
        while (playercmd != null)
        {
            ExecCmd(playercmd);
            playercmd = m_CmdQueue.Dequeue();
        }
        PhyicsCheck();
        AfterCmdUpdate();
    }
    private void PhyicsCheck()
    {
        
    }
    private void AfterCmdUpdate()
    {
        if (m_bIsMoving)
        {
            PlayerCmd playerCmd = new PlayerCmd();
            playerCmd.Dir = m_ObjRoot.transform.forward;
            m_CmdQueue.Enqueue(playerCmd);
        }
    }
    #endregion

    #region send
    private void OnSendSelfMove(EventElement obj)
    {
        // set move direction
        BattleCmdInfo_Move cmd = new BattleCmdInfo_Move();
        cmd.dir = (Vector3)obj.eventParam;
        cmd.charId = m_PlayerInfo.Uid;
        BattleManager.SendBattleCmd(cmd);
    }
    private void OnSendSelfStopMove(EventElement obj)
    {
        BattleCmdInfo_StopMove cmd = new BattleCmdInfo_StopMove();
        cmd.charId = m_PlayerInfo.Uid;
        BattleManager.SendBattleCmd(cmd);
    }
    #endregion

    #region recieve
    private void OnCmdMove(EventElement obj)
    {
        BattleCmdInfo_Move cmd = obj.eventParam as BattleCmdInfo_Move;
        if (ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetSelf().Uid != cmd.charId)
        {
            return;
        }
        var tmp = cmd.dir.y;
        cmd.dir.y = cmd.dir.z;
        cmd.dir.z = tmp;
        PlayerCmd playerCmd = new PlayerCmd();
        playerCmd.Dir = cmd.dir;
        bool isAlradyHavCmd = false;
        int size = m_CmdQueue.GetCount();
        for (int i = 0; i < size; ++i)
        {
            var incmd = m_CmdQueue.Dequeue();
            if (incmd.m_bDir)
            {
                isAlradyHavCmd = true;
                incmd.Dir = playerCmd.Dir;
            }
            m_CmdQueue.Enqueue(incmd);
        }
        if (!isAlradyHavCmd)
        {
            m_CmdQueue.Enqueue(playerCmd);
            m_bIsMoving = true;
        }
    }
    private void OnCmdSpeed(EventElement obj)
    {
        BattleCmdInfo_Speed cmd = obj.eventParam as BattleCmdInfo_Speed;
        if (ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetSelf().Uid != cmd.CharId)
        {
            return;
        }
        PlayerCmd playerCmd = new PlayerCmd();
        playerCmd.Speed = cmd.Speed;
        m_CmdQueue.Enqueue(playerCmd);
        m_bIsMoving = true;
    }
    private void OnCmdStopMove(EventElement obj)
    {
        BattleCmdInfo_StopMove cmd = obj.eventParam as BattleCmdInfo_StopMove;
        if (ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetSelf().Uid != cmd.charId)
        {
            return;
        }
        m_bIsMoving = false;
    }
    #endregion

    #region cmd
    private void ExecCmd(PlayerCmd playercmd)
    {
        if (playercmd.m_bSpeed)
        {
            ExecCmd_Speed(playercmd.Speed);
        }
        if (playercmd.m_bDir)
        {
            ExecCmd_Move(playercmd.Dir);
        }
    }
    private void ExecCmd_Move(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            m_ObjRoot.transform.forward = dir;
        }
        if (m_fSpeed > 0.0f)
        {
            m_ObjRoot.transform.position += m_ObjRoot.transform.forward * m_fSpeed;
        }
    }
    private void ExecCmd_Speed(float speed)
    {
        m_fSpeed = speed;
    }

    #endregion
}