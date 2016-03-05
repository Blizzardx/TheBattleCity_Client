using UnityEngine;
using System.Collections;

public class BattleLogic : LogicBase<BattleLogic>
{
    public string m_strPreLoadMap = "Map_0";
    public PlayerController m_PlayerController;
    private UIWindowBattle m_BattleWindow;

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
    }

    public void OnStart()
    {
        //open ui
        WindowManager.Instance.OpenWindow(WindowID.Battle);
        m_BattleWindow = WindowManager.Instance.GetWindow(WindowID.Battle) as UIWindowBattle;
        m_BattleWindow.SetActive(true);

        //set call back
        m_BattleWindow.SetInputCallBack(m_PlayerController.GetPlayer().Move);
    }
    public override void EndLogic()
    {
        
    }

}
