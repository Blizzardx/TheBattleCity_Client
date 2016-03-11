﻿using UnityEngine;
using System.Collections;
using NetWork.Auto;

public class PlayerController
{
    private int m_iPlayerUid;
    private Player m_Player;
    
    public void CreatePlayer(string name)
    {
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
            // send message to move
            CSHandler handler = new CSHandler();
            handler.PlayerUid = m_iPlayerUid;
            handler.MoveDirection = new ThriftVector3();
            handler.MoveDirection.SetVector3(newDir);
            handler.CurrentPosition = new ThriftVector3();
            handler.CurrentPosition.SetVector3(m_Player.transform.position);

            NetWorkManager.Instance.SendMsgToServer(handler);
        }
    }
    public void OnInputFire(Vector3 pos)
    {

    }
    private void OnDestroy()
    {
        m_Player = null;
    }
    public void RegisterEvent()
    {
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_HANDLER, OnPlayerMove);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_FIRE, OnPlayerFire);
    }
    public void UnRegisterEvent()
    {
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_HANDLER, OnPlayerMove);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_FIRE, OnPlayerFire);
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
    }
    private void OnPlayerFire(MessageObject obj)
    {

    }
}
