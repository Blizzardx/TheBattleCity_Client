using System;
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