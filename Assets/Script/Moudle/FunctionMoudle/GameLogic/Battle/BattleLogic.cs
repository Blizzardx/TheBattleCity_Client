using UnityEngine;
using System.Collections;

public class BattleLogic : LogicBase<BattleLogic>
{
    public string m_strPreLoadMap = "Map_0";
    public PlayerController m_PlayerController;
    private UIWindowBattle m_BattleWindow;
    private Camera m_SceneCamera;

    public override void StartLogic()
    {
        // load map
        MapManager.Instance.LoadMap(m_strPreLoadMap);

        //load player
        m_PlayerController = new PlayerController();
        m_PlayerController.CreatePlayer("Tank_0");

        //set player postion
        ComponentTool.Attach(MapManager.Instance.GetCurrentMapInfo().GetPlayerPosition()[0],
            m_PlayerController.GetPlayer().transform);

        m_SceneCamera = MapManager.Instance.GetCurrentMapInfo().GetSceneCamera();

        m_PlayerController.RegisterEvent();
    }

    public void OnStart()
    {
        //open ui
        WindowManager.Instance.OpenWindow(WindowID.Battle);
        m_BattleWindow = WindowManager.Instance.GetWindow(WindowID.Battle) as UIWindowBattle;
        m_BattleWindow.SetActive(true);

        //set call back
        m_BattleWindow.SetInputCallBack(m_PlayerController.OnInputMove);
        m_BattleWindow.SetFireCallBack(OnPlayerFire);
    }
    public override void EndLogic()
    {
        m_PlayerController.UnRegisterEvent();
    }

    private void OnPlayerFire(Vector3 touchPosition)
    {
        //check
        Ray ray = m_SceneCamera.ScreenPointToRay(touchPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            m_PlayerController.OnInputFire(hitInfo.point);
        }
    }
}
