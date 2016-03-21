using UnityEngine;
using System.Collections;
using NetWork.Auto;
using System.Collections.Generic;
using System;

public class BattleLogic : LogicBase<BattleLogic>
{
    public string m_strPreLoadMap = "Map_0";
    private UIWindowBattle m_BattleWindow;
    private Camera m_SceneCamera;
    private CameraFollow m_CameraFollow;
    private List<PlayerController> m_PlayerList;
    private PlayerController m_thisPlayer;
    private int m_iThisPlayerUid;

    public override void StartLogic()
    {
        m_PlayerList = new List<PlayerController>();

        // load map
        MapManager.Instance.LoadMap(m_strPreLoadMap);

        m_SceneCamera = MapManager.Instance.GetCurrentMapInfo().GetSceneCamera();
        m_CameraFollow = m_SceneCamera.GetComponent<CameraFollow>();

        //test code
        List<PlayerInfo> tmpPlayerList = PlayerDataMode.Instance.m_PlayerList;

        for(int i=0;i<tmpPlayerList.Count;++i)
        {
            //load player
            PlayerController PlayerController = new PlayerController();
            PlayerController.CreatePlayer(tmpPlayerList[i],m_CameraFollow);

            //set player postion
            ComponentTool.Attach(MapManager.Instance.GetCurrentMapInfo().GetPlayerPosition()[tmpPlayerList[i].PositionId],
                PlayerController.GetPlayer().transform);

            PlayerController.RegisterEvent();

            if(PlayerDataMode.Instance.playerUid == tmpPlayerList[i].Uid)
            {
                m_thisPlayer = PlayerController;
            }
            m_PlayerList.Add(PlayerController);
        }

        RegisterEvent();
    }

    public void OnStart()
    {
        //open ui
        WindowManager.Instance.OpenWindow(WindowID.Battle);
        m_BattleWindow = WindowManager.Instance.GetWindow(WindowID.Battle) as UIWindowBattle;
        m_BattleWindow.SetActive(false);

        //set call back test code
        m_BattleWindow.SetInputCallBack(m_thisPlayer.OnInputMove);
        m_BattleWindow.SetFireCallBack(OnPlayerFire);

        for(int i=0;i<m_PlayerList.Count;++i)
        {
            RegisterPlayerInfo(m_PlayerList[i].GetUid(), m_PlayerList[i].GetName());
        }
        FrameTickTask.Instance.RegisterToUpdateList(UpdatePlayerPos);
        //send load end
        SendLoadEnd();
    }
    public override void EndLogic()
    {
        for (int i = 0; i < m_PlayerList.Count; ++i)
        {
            m_PlayerList[i].UnRegisterEvent();
        }
        FrameTickTask.Instance.UnRegisterFromUpdateList(UpdatePlayerPos);
        UnRegisterEvent();
        PlayerDataMode.Instance.isCreater = false;
    }
    private void UpdatePlayerPos()
    {
        for (int i = 0; i < m_PlayerList.Count; ++i)
        {
            SetPlayerInfoPos(m_PlayerList[i].GetUid(), m_PlayerList[i].GetPlayer().transform.position);
        }
    }

    #region battle ui
    public void RegisterPlayerInfo(int uid, string name)
    {
        if(null == m_BattleWindow)
        {
            return;
        }
        m_BattleWindow.RegisterPlayerInfo(uid, name);
    }
    public void SetPlayerInfoPos(int uid, Vector3 Pos)
    {
        if (null == m_BattleWindow)
        {
            return;
        }
        Vector3 screenPos = m_SceneCamera.WorldToScreenPoint(Pos);
        m_BattleWindow.SetPlayerInfoPos(uid, screenPos);
    }
    public void SetPlayerInfoHp(int uid, float hp)
    {
        if (null == m_BattleWindow)
        {
            return;
        }
        m_BattleWindow.SetPlayerInfoHp(uid, hp);
    }
    #endregion

    #region battle event
    public void OnPlayerDead(int uid)
    {
        CSBattleEnd client = new CSBattleEnd();
        client.PlayerUid = uid;
        client.IsWin = false;
        NetWorkManager.Instance.SendMsgToServer(client);
    }
    public void OnExitBattle()
    {
        StageManager.Instance.ChangeState(GameStateType.WorldState);   
    }
    #endregion

    private void OnPlayerFire(Vector3 touchPosition)
    {
        //check
        Ray ray = m_SceneCamera.ScreenPointToRay(touchPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            m_thisPlayer.OnInputFire(hitInfo.point);
        }
    }
    private void SendLoadEnd()
    {
        CSBattleLoadEnd client = new CSBattleLoadEnd();
        NetWorkManager.Instance.SendMsgToServer(client);
    }
    private void RegisterEvent()
    {
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_BattleBegin, OnBattleBegin);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_BattleEnd, OnBattleEnd);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_CreateItem, OnCreateItem);
    }
    private void UnRegisterEvent()
    {
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_BattleBegin, OnBattleBegin);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_BattleEnd, OnBattleEnd);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_CreateItem, OnCreateItem);
    }
    private void OnBattleBegin(MessageObject msg)
    {
        if(msg.msgValue is SCBattleBegin)
        {
            SCBattleBegin server = msg.msgValue as SCBattleBegin;
            float cd = (float)(server.Cd) / 1000.0f;
            m_BattleWindow.SetActive(true);

            SendItemGenInfo();
        }
    }
    private void OnBattleEnd(MessageObject msg)
    {
        if (msg.msgValue is SCBattleEnd)
        {
            m_BattleWindow.SetActive(false);
            SCBattleEnd server = msg.msgValue as SCBattleEnd;
            m_BattleWindow.ShowEndPanel(server.IsWin);
        }
    }
    private void OnCreateItem(MessageObject msg)
    {
        if(!(msg.msgValue is SCCreateItem))
        {
            return;
        }

        SCCreateItem server = msg.msgValue as SCCreateItem;
        //try get item position by id
        Vector3 pos = Vector3.zero;
        if(MapManager.Instance.GetCurrentMapInfo().GetItemPositionById(server.PositionId,out pos))
        {
            //trigger to create item
            ItemManager.Instance.AddItem(server.ItemId, pos, server.PositionId);
        }
    }
    private void SendItemGenInfo()
    {
        if(!PlayerDataMode.Instance.isCreater)
        {
            return;
        }

        CSItemGenFundamental client = new CSItemGenFundamental();
        GenItemInfo info = MapManager.Instance.GetCurrentMapInfo().GetGenItemInfo();

        client.GenFundamental = new ItemGenFundamental();
        client.GenFundamental.PositionId = info.m_PosList;
        client.GenFundamental.ItemList = info.m_ItemList;
        client.GenFundamental.MaxCount = info.itemMaxCount;
        client.GenFundamental.GenPreTimeItemCountMin = info.pertimeGenItemCountMin;
        client.GenFundamental.GenPreTimeItemCountMax = info.pertimeGenItemCountMax;
        client.GenFundamental.TriggerDeltaTime = info.deltaTime;
        client.GenFundamental.InitItemCount = info.initItemCount;

        NetWorkManager.Instance.SendMsgToServer(client);
    }
}
