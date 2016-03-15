using UnityEngine;
using System.Collections;
using NetWork.Auto;
using System.Collections.Generic;

public class BattleLogic : LogicBase<BattleLogic>
{
    public string m_strPreLoadMap = "Map_0";
    private UIWindowBattle m_BattleWindow;
    private Camera m_SceneCamera;
    private List<PlayerController> m_PlayerList;
    private PlayerController m_thisPlayer;
    private int m_iThisPlayerUid;

    public override void StartLogic()
    {
        m_PlayerList = new List<PlayerController>();

        // load map
        MapManager.Instance.LoadMap(m_strPreLoadMap);

        //test code
        List<PlayerInfo> tmpPlayerList = PlayerDataMode.Instance.m_PlayerList;

        for(int i=0;i<tmpPlayerList.Count;++i)
        {
            //load player
            PlayerController PlayerController = new PlayerController();
            PlayerController.CreatePlayer("Tank_0", tmpPlayerList[i].Uid);

            //set player postion
            ComponentTool.Attach(MapManager.Instance.GetCurrentMapInfo().GetPlayerPosition()[tmpPlayerList[i].PositionId],
                PlayerController.GetPlayer().transform);

            PlayerController.RegisterEvent();

            if(PlayerDataMode.Instance.playerUid == tmpPlayerList[i].Uid)
            {
                m_thisPlayer = PlayerController;
            }
        }
        

        m_SceneCamera = MapManager.Instance.GetCurrentMapInfo().GetSceneCamera();

    }

    public void OnStart()
    {
        //open ui
        WindowManager.Instance.OpenWindow(WindowID.Battle);
        m_BattleWindow = WindowManager.Instance.GetWindow(WindowID.Battle) as UIWindowBattle;
        m_BattleWindow.SetActive(true);

        //set call back test code
        m_BattleWindow.SetInputCallBack(m_thisPlayer.OnInputMove);
        m_BattleWindow.SetFireCallBack(OnPlayerFire);
    }
    public override void EndLogic()
    {
        for (int i = 0; i < m_PlayerList.Count; ++i)
        {
            m_PlayerList[i].UnRegisterEvent();
        }
    }
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
}
