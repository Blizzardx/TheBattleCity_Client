using UnityEngine;
using System.Collections;

namespace Framework.Common
{
    public partial class EventIdDefine
    {
        public static readonly int Connected = 0;
        public static readonly int ConnectError = 1;
        public static readonly int LoadingSceneProcess = 2;

        // room
        public static readonly int CreateRoom = 3;
        public static readonly int RoomList = 4;
        public static readonly int SyncPlayerInfo = 5;
        public static readonly int EnterRoom = 6;
        public static readonly int SearchRoom = 7;

        // battle
        public static readonly int BeginLoadBattle = 8;
        public static readonly int BattleBegin = 9;
        public static readonly int BattleEnd = 10;
        public static readonly int BattleLogicFrame = 11;
        public static readonly int BattleSelfMove = 12;
        public static readonly int BattleCmdMove = 13;
        public static readonly int BattleSelfStopMove = 14;
        public static readonly int BattleCmdSpeed = 15;
        public static readonly int BattleCmdStopMove = 16;
    }
}