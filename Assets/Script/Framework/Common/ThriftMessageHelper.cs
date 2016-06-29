using System;
using System.Collections.Generic;

namespace Framework.Common
{
    public class ThriftMessageHelper
    {

        private static Dictionary<int, System.Type> MSG_DIC = new Dictionary<int, System.Type>();

        private static Dictionary<System.Type, int> TYPE_DIC = new Dictionary<Type, int>();

        internal static Dictionary<int, Type> Get_REQ_ID_MSG()
        {
            return MSG_DIC;
        }
        internal static Dictionary<Type, int> Get_REQ_MSG_ID()
        {
            return TYPE_DIC;
        }
        internal static void  Set_REQ_ID_MSG(Dictionary<int, Type> id_type)
        {
            MSG_DIC = id_type;
        }
        internal static void Set_REQ_MSG_ID(Dictionary<Type, int> type_id)
        {
            TYPE_DIC = type_id;
        }
    }
}
