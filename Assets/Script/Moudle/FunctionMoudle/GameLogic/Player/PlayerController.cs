using UnityEngine;
using System.Collections.Generic;
using NetWork.Auto;
using System;

public class PlayerController
{
    //test code
    private static Dictionary<int, PlayerController> m_playerControllerMap = new Dictionary<int, PlayerController>();

    private int m_iPlayerUid;
    private Player m_Player;
    private PlayerInfo m_PlayerBaseInfo;

    //
    private const float m_fMoveCd = 0.125f;
    private float m_fCurrentMovecd;
    private Vector3 m_LastOption;
    private bool m_bNeedSendLastOption;

    public void CreatePlayer(PlayerInfo baseInfo)
    {
        m_PlayerBaseInfo = baseInfo;
        int uid = baseInfo.Uid;
        string name = baseInfo.MeshName;
        name = "Tank_0";

        m_iPlayerUid = uid;
        // try load player
        var obj = ResourceManager.Instance.LoadBuildInResource<GameObject>(name, AssetType.Tank);
        if (null == obj)
        {
            Debuger.LogWarning("can't laod tank " + name);
            return;
        }
        obj = GameObject.Instantiate(obj);
        ComponentTool.Attach(null, obj.transform);

        m_Player = obj.GetComponent<Player>();
        if (null == m_Player)
        {
            Debuger.LogWarning("player instance is not attach player script " + name);
            GameObject.Destroy(obj);
            return;
        }

        m_Player.SetOnDestroyCallBack(OnDestroy);
        m_Player.SetOnDeadCallBack(OnDead);

        //set value
        m_Player.SetHp(baseInfo.Hp);
        m_Player.SetMaxHp(baseInfo.Hp);
        //set move cd
        m_fCurrentMovecd = 0.0f;

        m_playerControllerMap.Add(obj.GetInstanceID(), this);
    }
    public Player GetPlayer()
    {
        return m_Player;
    }
    public void OnInputMove(Vector2 dir)
    {
        Vector3 newDir = Vector3.zero;
        if(m_Player.TryMove(dir,ref newDir))
        {
            if(m_fCurrentMovecd <= 0.0f)
            {
                // send message to move
                CSHandler handler = new CSHandler();
                handler.PlayerUid = m_iPlayerUid;
                handler.MoveDirection = new ThriftVector3();
                handler.MoveDirection.SetVector3(newDir);
                handler.CurrentPosition = new ThriftVector3();
                handler.CurrentPosition.SetVector3(m_Player.transform.position);

                NetWorkManager.Instance.SendMsgToServer(handler);
                DisableInput();
            }
            else
            {
                //record last option
                m_LastOption = newDir;
                m_bNeedSendLastOption = newDir == Vector3.zero;
            }
        }
    }
    internal string GetName()
    {
        return m_PlayerBaseInfo.Name;
    }
    internal int GetUid()
    {
        return m_iPlayerUid;
    }
    public void OnInputFire(Vector3 pos)
    {
        if (!m_Player.TryFire())
        {
            return;
        }
        m_Player.BeginFireCd();

        //send message to fire
        CSFire fire = new CSFire();
        fire.BulletName = "Bullet_0";
        fire.CurrentPosition = new ThriftVector3();
        fire.CurrentPosition.SetVector3(m_Player.GetFirePos(pos));
        fire.FireDirection = new ThriftVector3();
        fire.FireDirection.SetVector3(pos);
        fire.PlayerUid = m_iPlayerUid;

        NetWorkManager.Instance.SendMsgToServer(fire);
    }
    public void RegisterEvent()
    {
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_HANDLER, OnPlayerMove);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_FIRE, OnPlayerFire);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_Hurt, OnPlayerHurt);
    }
    public void UnRegisterEvent()
    {
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_HANDLER, OnPlayerMove);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_FIRE, OnPlayerFire);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_Hurt, OnPlayerHurt);
    }
    private void OnDestroy(int instanceId)
    {
        m_playerControllerMap.Remove(instanceId);
        m_Player = null;
    }
    private void OnDead()
    {
        BattleLogic.Instance.OnPlayerDead(m_iPlayerUid);
    }
    private void OnPlayerMove(MessageObject obj)
    {
        if(! (obj.msgValue is SCHandler))
        {
            return;
        }

        SCHandler handler = obj.msgValue as SCHandler;
        if(m_iPlayerUid != handler.PlayerUid)
        {
            return;
        }

        m_Player.DoMove(handler.MoveDirection.GetVector3(),handler.CurrentPosition.GetVector3());
        
        SendLastMoveOption();
    }
    private void OnPlayerFire(MessageObject obj)
    {
        if (!(obj.msgValue is SCFire))
        {
            return;
        }

        SCFire handler = obj.msgValue as SCFire;
        if (m_iPlayerUid != handler.PlayerUid)
        {
            return;
        }

        m_Player.Fire(handler.FireDirection.GetVector3());
        //create bullet
        BulletManager.Instance.CreateBullet(m_iPlayerUid, handler.BulletName, handler.CurrentPosition.GetVector3(),
           (handler.FireDirection.GetVector3() - handler.CurrentPosition.GetVector3()).normalized);
    }
    private void OnPlayerHurt(MessageObject obj)
    {
        if (!(obj.msgValue is SCHurt))
        {
            return;
        }

        SCHurt server = obj.msgValue as SCHurt;
        if (m_iPlayerUid != server.PlayerUid)
        {
            return;
        }

        m_Player.SubHp(server.HurtValue);
        //refresh ui
        BattleLogic.Instance.SetPlayerInfoHp(m_iPlayerUid, m_Player.GetHpValue());
    }
    private void SendLastMoveOption()
    {
        if(m_bNeedSendLastOption)
        {
            // send message to move
            CSHandler handler = new CSHandler();
            handler.PlayerUid = m_iPlayerUid;
            handler.MoveDirection = new ThriftVector3();
            handler.MoveDirection.SetVector3(m_LastOption);
            handler.CurrentPosition = new ThriftVector3();
            handler.CurrentPosition.SetVector3(m_Player.transform.position);

            NetWorkManager.Instance.SendMsgToServer(handler);
            m_bNeedSendLastOption = false;
            DisableInput();
        }
    }
    private void DisableInput()
    {
        m_fCurrentMovecd = m_fMoveCd;
    }
    public void TriggerHurt(int value)
    {
        // check
        if(m_iPlayerUid != PlayerDataMode.Instance.playerUid)
        {
            return;
        }
        CSHurt client = new CSHurt();
        client.PlayerUid = m_iPlayerUid;
        client.HurtValue = value;

        NetWorkManager.Instance.SendMsgToServer(client);
    }
    //test code
    public static PlayerController GetPlayerController(int instanceId)
    {
        PlayerController res = null;
        if(m_playerControllerMap.TryGetValue(instanceId,out res))
        {
            return res;
        }
        return null;
    }
    public void UpdateMoveCd()
    {
        m_fCurrentMovecd -= Time.deltaTime;
    }
}
