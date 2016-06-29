﻿using System;
using System.Collections.Generic;
using NetWork.Auto;
using Thrift.Protocol;
using UnityEngine;

namespace Framework.Common
{
    
        public partial class ThriftMessageHelper
        {
            private static Dictionary<int, System.Type> REQ_ID_MSG = new Dictionary<int, System.Type>();

            private static Dictionary<System.Type, int> REQ_MSG_ID = new Dictionary<System.Type, int>();

            static ThriftMessageHelper()
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

            public static TBase GetResponseMessage(int messageId)
            {
                if (!REQ_ID_MSG.ContainsKey(messageId))
                {
                    return null;
                }
                System.Type reqType = REQ_ID_MSG[messageId];
                string respClassName = reqType.FullName.Substring(0, reqType.FullName.Length - 7) + "Response";

                try
                {
                    return System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(respClassName, false) as TBase;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
                return null;
            }

            public static bool CanSupportMessageId(int messageId)
            {
                return REQ_ID_MSG.ContainsKey(messageId);
            }

            public static bool CanSupportMessage(TBase message)
            {
                return REQ_MSG_ID.ContainsKey(message.GetType());
            }

            public static int GetMessageId(TBase message)
            {
                if (CanSupportMessage(message))
                {
                    return REQ_MSG_ID[message.GetType()];
                }

                return -1;
            }

            public static Dictionary<int, System.Type> Get_REQ_ID_MSG()
            {
                return REQ_ID_MSG;
            }
            public static Dictionary<System.Type, int> Get_REQ_MSG_ID()
            {
                return REQ_MSG_ID;
            }
        }

}
