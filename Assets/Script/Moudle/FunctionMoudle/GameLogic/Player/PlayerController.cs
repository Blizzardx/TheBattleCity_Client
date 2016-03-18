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
            CSHandler handler = new CSHandler();
            handler.PlayerUid = m_iPlayerUid;
            handler.MoveDirection = new ThriftVector3();
            handler.MoveDirection.SetVector3(newDir);
            handler.CurrentPosition = new ThriftVector3();
            handler.CurrentPosition.SetVector3(m_Player.transform.position);

            NetWorkManager.Instance.SendMsgToServer(handler);

            m_Player.DoMove(handler.MoveDirection.GetVector3(), m_Player.transform.position);            
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
        pos.y = 0.0f;
        fire.FireDirection.SetVector3(pos);
        fire.PlayerUid = m_iPlayerUid;

        NetWorkManager.Instance.SendMsgToServer(fire);

    }
    public void RegisterEvent()
    {
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_HANDLER, OnPlayerMove);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_FIRE, OnPlayerFire);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_Hurt, OnPlayerHurt);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_UsedItem, OnPlayerUsedItem);
        MessageManager.Instance.RegistMessage(ClientCustomMessageDefine.C_UPDATE_PLAYER_UI, OnUpdatePlaeyrUI);
    }
    public void UnRegisterEvent()
    {
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_HANDLER, OnPlayerMove);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_FIRE, OnPlayerFire);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_Hurt, OnPlayerHurt);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_UsedItem, OnPlayerUsedItem);
        MessageManager.Instance.UnregistMessage(ClientCustomMessageDefine.C_UPDATE_PLAYER_UI, OnUpdatePlaeyrUI);
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
        if(PlayerDataMode.Instance.playerUid == m_iPlayerUid)
        {
            return;
        }
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
    private void OnPlayerUsedItem(MessageObject obj)
    {
        if(!(obj.msgValue is SCUsedItem))
        {
            return;
        }

        SCUsedItem server = obj.msgValue as SCUsedItem;

        //try remvoe item
        ItemManager.Instance.RemoveItem(server.ItemId, server.PositionId);

        //
        if(m_iPlayerUid == server.PlayerUid)
        {
            //try use item
            ItemManager.Instance.UseItem(server.ItemId, m_Player);
        }
    }

    private void OnUpdatePlaeyrUI(MessageObject obj)
    {
        //refresh ui
        BattleLogic.Instance.SetPlayerInfoHp(m_iPlayerUid, m_Player.GetHpValue());
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
    internal void TriggerUseItem(int playerUid,int itemId, int posId)
    {
        CSUseItem client = new CSUseItem();
        client.ItemId = itemId;
        client.PositionId = posId;
        client.PlayerUid = playerUid;
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
}
