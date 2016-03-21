using UnityEngine;
using System.Collections;
public enum GameStateType
{
    //logic
    none,
    LoginState,
    BattleState,
    ReConnectState,
    WorldState,
    
}
public class WindowID
{
    public const int Loading = 0;
    public const int Login = 1;
    public const int Alert = 2;
    public const int AssetUpdate = 3;
    public const int Battle = 4;
    public const int Wait = 5;
    public const int RoomList = 6;
    public const int CreateRoom = 7;
    public const int EnterRoom = 8;
    public const int WaitBattle = 9;
    public const int SelectMode = 10;
    public const int Welcome = 11;
    public const int SearchRoom = 12;
}

public enum WindowLayer
{
    Window,
    Tip ,
    Max,
}
public class Definer
{
    public static void RegisterWindow()
    {
        WindowManager.Instance.RegisterWindow(WindowID.Alert, "Tip/UIWindow_Alert", WindowLayer.Tip, typeof(UIWindowAlert));
        WindowManager.Instance.RegisterWindow(WindowID.Loading, "Loading/UIWindow_Loading", WindowLayer.Window, typeof(UIWindowLoading));
        WindowManager.Instance.RegisterWindow(WindowID.Battle, "Battle/UIWindow_Battle", WindowLayer.Window, typeof(UIWindowBattle));
        WindowManager.Instance.RegisterWindow(WindowID.Wait, "Loading/UIWindow_Wait", WindowLayer.Window, typeof(WindowBase));
        WindowManager.Instance.RegisterWindow(WindowID.RoomList, "Room/UIWindow_Room", WindowLayer.Window, typeof(UIWindowRoomList));
        WindowManager.Instance.RegisterWindow(WindowID.CreateRoom, "Room/UIWindow_CreateRoom", WindowLayer.Window, typeof(UIWindowCreateRoom));
        WindowManager.Instance.RegisterWindow(WindowID.EnterRoom, "Room/UIWindow_EnterRoom", WindowLayer.Window, typeof(UIWindowEnterRoom));
        WindowManager.Instance.RegisterWindow(WindowID.WaitBattle, "Room/UIWindow_WaitBattle", WindowLayer.Window, typeof(UIWindowWaitBattle));
        WindowManager.Instance.RegisterWindow(WindowID.Welcome, "MainMenu/Window_Welcom", WindowLayer.Window, typeof(UIWindowWelcome));
        WindowManager.Instance.RegisterWindow(WindowID.SelectMode, "MainMenu/Window_SelectMode", WindowLayer.Window, typeof(UIWindowSelectMode));
        WindowManager.Instance.RegisterWindow(WindowID.SearchRoom, "Room/UIWindow_SearchRoom", WindowLayer.Window, typeof(UIWindowSearchRoom));
    }
    public static void RegisterStage()
    {
        //logic scene
        StageManager.Instance.RegisterStage(GameStateType.LoginState, "Login", typeof(LoginStage));
        StageManager.Instance.RegisterStage(GameStateType.ReConnectState, "World", typeof(ReconnectStage));
        StageManager.Instance.RegisterStage(GameStateType.BattleState, "Battle", typeof(BattleStage));
        StageManager.Instance.RegisterStage(GameStateType.WorldState, "World", typeof(WorldStage));

        //node game scene
    }
    public static  void DoCollection()
    {
        if (StageManager.Instance.GetCurrentGameStage() == GameStateType.LoginState)
        {
            return;
            // do nothing
        }
        
        //release logic 
        
        
    }
}