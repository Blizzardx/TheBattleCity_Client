using UnityEngine;
using System.Collections;

public class ClientCustomMessageDefine
{

    public static bool IsClientCustomMessage(int message)
    {
        return message >= 300000 && message <= 400000;
    }

    //300000 - 400000
    public const int C_SOCKET_CLOSE     = 300000;
    public const int C_SOCKET_TIMEOUT   = 300001;
    public const int C_SOCKET_CONNECTED = 300002;
    public const int C_SOCKET_CONNECTEERROR = 300003;
    public const int C_HIT_LIFE = 300004;
    public const int C_HIT_TERRAIN = 300005;
    public const int C_CHANGE_SCENE = 300006;
    public const int C_ENABLE_BLOCK = 300007;
    public const int C_DISABLE_BLOCK = 300008;

}
