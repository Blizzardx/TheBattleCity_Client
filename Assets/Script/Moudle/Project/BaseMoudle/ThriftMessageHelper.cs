using System.Collections.Generic;
using NetWork.Auto;
using UnityEngine;

namespace Framework.Common
{
    public class ThriftMessageHelper_Constructor : SystemEventTrigger
    {
        public int GetSortId()
        {
            return 0;
        }
        public void Init()
        {
            Debug.Log("register thrift message");
            ThriftMessageHelper.RegisterMessage();
        }
    }
    public partial class ThriftMessageHelper
    {
        public static void RegisterMessage()
        {
            REQ_ID_MSG.Add(MessageIdConstants.CS_PING, typeof(CSPingMsg));
            REQ_ID_MSG.Add(MessageIdConstants.SC_PONG, typeof(SCPongMsg));
            REQ_ID_MSG.Add(MessageIdConstants.CS_RoomList, typeof(CSRoomList));
            REQ_ID_MSG.Add(MessageIdConstants.SC_RoomList, typeof(SCRoomList));
            REQ_ID_MSG.Add(MessageIdConstants.CS_SearchRoom, typeof(CSSearchRoom));
            REQ_ID_MSG.Add(MessageIdConstants.SC_SearchRoom, typeof(SCSearchRoom));
            REQ_ID_MSG.Add(MessageIdConstants.CS_CreateRoom, typeof(CSCreateRoom));
            REQ_ID_MSG.Add(MessageIdConstants.SC_CreateRoom, typeof(SCCreateRoom));
            REQ_ID_MSG.Add(MessageIdConstants.CS_EnterRoom, typeof(CSEnterRoom));
            REQ_ID_MSG.Add(MessageIdConstants.SC_EnterRoom, typeof(SCEnterRoom));
            REQ_ID_MSG.Add(MessageIdConstants.SC_SyncPlayerInfo, typeof(SCSyncPlayerInfo));
            REQ_ID_MSG.Add(MessageIdConstants.CS_BattleLoadEnd, typeof(CSBattleLoadEnd));
            REQ_ID_MSG.Add(MessageIdConstants.SC_BeginLoadBattle, typeof(SCBeginLoadBattle));
            REQ_ID_MSG.Add(MessageIdConstants.SC_BattleBegin, typeof(SCBattleBegin));
            REQ_ID_MSG.Add(MessageIdConstants.SC_BattleEnd, typeof(SCBattleEnd));
            REQ_ID_MSG.Add(MessageIdConstants.CS_BattleEnd, typeof(CSBattleEnd));
            REQ_ID_MSG.Add(MessageIdConstants.CS_BattleLogicFrame, typeof(CSBattleLogicFrame));
            REQ_ID_MSG.Add(MessageIdConstants.SC_BattleLogicFrame, typeof(SCBattleLogicFrame));


            foreach (KeyValuePair<int, System.Type> kv in REQ_ID_MSG)
            {
                REQ_MSG_ID.Add(kv.Value, kv.Key);
            }
        }
    }
}
