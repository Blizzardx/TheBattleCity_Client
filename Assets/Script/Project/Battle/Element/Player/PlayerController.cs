using Framework.Common;
using Framework.Event;
using UnityEngine;

public class PlayerController
{
    private PlayerCom m_ObjRoot;
    private PlayerInfo m_PlayerInfo;
    private Vector3 m_vDir;
    private float m_fSpeed;

    public PlayerController(PlayerInfo playerInfo)
    {
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
            EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleSelfChangeSpeed, OnSendSelfChangeSpeed);
            // mark camera
            var camControl = GameObject.FindObjectOfType<CameraFollow>();
            camControl.SetTarget(m_ObjRoot.transform);
        }
        // battle cmd event
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleCmdMove, OnCmdMove);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleCmdSpeed, OnCmdSpeed);
    }
    private void OnSendSelfMove(EventElement obj)
    {
        // set move direction
        BattleCmdInfo_Move cmd = new BattleCmdInfo_Move();
        cmd.dir = (Vector3)obj.eventParam;
        cmd.charId = m_PlayerInfo.Uid;
        BattleManager.SendBattleCmd(cmd);

        // set move speed
        BattleCmdInfo_Speed cmd1 = new BattleCmdInfo_Speed();
        cmd1.Speed = 0.03f;
        cmd1.CharId = m_PlayerInfo.Uid;
        BattleManager.SendBattleCmd(cmd1);
    }
    private void OnSendSelfChangeSpeed(EventElement obj)
    {
        BattleCmdInfo_Speed cmd = new BattleCmdInfo_Speed();
        cmd.Speed = (float) obj.eventParam;
        cmd.CharId = m_PlayerInfo.Uid;
        BattleManager.SendBattleCmd(cmd);
    }
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
        m_vDir = cmd.dir;
    }
    private void OnCmdSpeed(EventElement obj)
    {
        BattleCmdInfo_Speed cmd = obj.eventParam as BattleCmdInfo_Speed;
        if (ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetSelf().Uid != cmd.CharId)
        {
            return;
        }
        m_fSpeed = cmd.Speed;
    }
    public void OnCmdUpdate()
    {
        if (m_vDir != Vector3.zero)
        {
            m_ObjRoot.transform.forward = m_vDir;
        }
        if (m_fSpeed > 0.0f)
        {
            m_ObjRoot.transform.position += m_vDir * m_fSpeed;
        }
    }
}