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
    private string m_strCurrentBullet = "Bullet_0";
    //
    private CameraFollow m_CameraFollow;
    private Vector3 m_vLastMoveDir;
    private Vector3 m_vLastPos;
    private bool m_bNeedUpdate;


    public void CreatePlayer(PlayerInfo baseInfo,CameraFollow sceneCamera)
    {
        m_CameraFollow = sceneCamera;
        m_PlayerBaseInfo = baseInfo;
        int uid = baseInfo.Uid;
        string name = baseInfo.MeshName;
        //name = "Tank_";

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
        m_Player.SetCallBack(OnDestroy,OnMoveDirChange);
        m_Player.SetOnDeadCallBack(OnDead);

        //set value
        m_Player.SetHp(baseInfo.Hp);
        m_Player.SetMaxHp(baseInfo.Hp);

        m_playerControllerMap.Add(obj.GetInstanceID(), this);

        //set camera follow
        SetCameraFollow();
    }

    private void SetCameraFollow()
    {
        if (m_CameraFollow != null && m_iPlayerUid == PlayerDataMode.Instance.playerUid)
        {
            m_CameraFollow.SetTarget(m_Player.transform);
        }
    }
    public void ChangePlayer(string meshName)
    {
        m_PlayerBaseInfo.MeshName = meshName;

        //hp 
        int hp = m_Player.GetHp();
        float speed = m_Player.GetSpeed();

        //stop first
        OnInputMove(Vector2.zero);

        Vector3 pos = m_Player.transform.position;
        
        //clear player old instance
        GameObject.Destroy(m_Player.gameObject);

        //create new instance
        CreatePlayer(m_PlayerBaseInfo,m_CameraFollow);

        //reset hp
        m_Player.SetHp(hp);
        //reset speed
        m_Player.SetSpeed(speed);
        //reset move handler
        m_Player.transform.position = pos;
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
            m_vLastMoveDir = newDir;
            m_vLastPos = m_Player.transform.position;
            m_bNeedUpdate = true;
            m_Player.DoMove(newDir, m_Player.transform.position);            
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
    internal void SetBullet(string name)
    {
        m_strCurrentBullet = name;
    }
    internal string GetCurrentBullet(string name)
    {
        return m_strCurrentBullet;
    }
    public void OnInputFire(Vector3 pos)
    {
        if (!m_Player.TryFire())
        {
            return;
        }
        m_Player.BeginFireCd();
        List<Vector3> firePosList = m_Player.GetFirePos(pos);

        //send message to fire
        CSFire fire = new CSFire();
        fire.PlayerUid = m_iPlayerUid;
        fire.FireInfoList = new List<FireInfo>(firePosList.Count);
        foreach (var elem in firePosList)
        {
            FireInfo fireInfo = new FireInfo();
            fireInfo.BulletName = m_strCurrentBullet;
            fireInfo.CurrentPosition = new ThriftVector3();
            fireInfo.CurrentPosition.SetVector3(elem);
            fireInfo.FireDirection = new ThriftVector3();
            pos.y = 0.0f;
            fireInfo.FireDirection.SetVector3(pos);
            
            //add to list
            fire.FireInfoList.Add(fireInfo);
        }

        NetWorkManager.Instance.SendMsgToServer(fire);
    }
    public void RegisterEvent()
    {
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_HANDLER, OnPlayerMove);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_FIRE, OnPlayerFire);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_Hurt, OnPlayerHurt);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_UsedItem, OnPlayerUsedItem);
        MessageManager.Instance.RegistMessage(ClientCustomMessageDefine.C_UPDATE_PLAYER_UI, OnUpdatePlaeyrUI);
        SyncFrameTickTask.Instance.RegisterToUpdateList(SyncPlayerMove);
    }
    public void UnRegisterEvent()
    {
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_HANDLER, OnPlayerMove);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_FIRE, OnPlayerFire);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_Hurt, OnPlayerHurt);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_UsedItem, OnPlayerUsedItem);
        MessageManager.Instance.UnregistMessage(ClientCustomMessageDefine.C_UPDATE_PLAYER_UI, OnUpdatePlaeyrUI);
        SyncFrameTickTask.Instance.UnRegisterFromUpdateList(SyncPlayerMove);
    }
    private void OnDestroy(int instanceId)
    {
        m_playerControllerMap.Remove(instanceId);
        //m_Player = null;
    }
    private void OnMoveDirChange(Vector3 newDir)
    {
        m_vLastMoveDir = newDir;
        m_vLastPos = m_Player.transform.position;
        m_bNeedUpdate = true;
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

        m_Player.ReadyToFollow(handler.MoveDirection.GetVector3(),handler.CurrentPosition.GetVector3());
        //m_Player.DoMove(handler.MoveDirection.GetVector3(), handler.CurrentPosition.GetVector3());

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
        if (handler.FireInfoList.Count == 0)
        {
            return;
        }
        m_Player.Fire(handler.FireInfoList[0].FireDirection.GetVector3());        
        
        foreach (var elem in handler.FireInfoList)
        {
            Vector3 dir = (elem.FireDirection.GetVector3() - elem.CurrentPosition.GetVector3());
            dir.y = 0.0f;
            dir.Normalize();
            //create bullet
            BulletManager.Instance.CreateBullet(m_iPlayerUid, elem.BulletName, elem.CurrentPosition.GetVector3(),dir);
        }
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
    private void SyncPlayerMove()
    {
        if (!m_bNeedUpdate)
        {
            return;
        }

        m_bNeedUpdate = false;
        CSHandler handler = new CSHandler();
        handler.PlayerUid = m_iPlayerUid;
        handler.MoveDirection = new ThriftVector3();
        handler.MoveDirection.SetVector3(m_vLastMoveDir);
        handler.CurrentPosition = new ThriftVector3();
        handler.CurrentPosition.SetVector3(m_vLastPos);

        NetWorkManager.Instance.SendMsgToServer(handler);        
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
