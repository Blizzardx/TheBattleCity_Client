using UnityEngine;
using System.Collections;

public class EventIdDefine
{
    public static readonly int Connected = 0;
    public static readonly int ConnectError = 1;
    public static readonly int LoadingSceneProcess = 2;

    // room
    public static readonly int CreateRoom = 3;
    public static readonly int RoomList = 4;
    public static readonly int SyncPlayerInfo = 5;
    public static readonly int EnterRoom = 6;
    public static readonly int BeginLoadBattle = 7;
    public static readonly int BattleBegin = 8;
    public static readonly int BattleEnd = 9;
    public static readonly int SearchRoom = 10;
}
