namespace Framework.Common
{
    public partial class ClientCustomMessageDefine
    {

        public static bool IsClientCustomMessage(int message)
        {
            return message >= 300000 && message <= 400000;
        }

        //300000 - 400000
        public const int C_SOCKET_CLOSE             = 300000;
        public const int C_SOCKET_TIMEOUT           = 300001;
        public const int C_SOCKET_SEND_FAILED       = 300002;
        public const int C_SOCKET_CONNECT_ERROR     = 300003;
        public const int C_SOCKET_CONNECTED         = 300004;


    }
}
