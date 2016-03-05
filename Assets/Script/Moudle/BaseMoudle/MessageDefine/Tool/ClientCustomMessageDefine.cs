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
    public const int C_HIT_LIFE         = 300002;
    public const int C_HIT_TERRAIN      = 300003;
    public const int C_PLAY_ACTION      = 300004;
    public const int C_ACCEPT_MISSION   = 300005;
    public const int C_ACTION_FININSH       = 300006;
    public const int C_ACTION_START         = 300007;
    public const int C_GAMELOGIC_SCENE_TRIGGER = 300008;
    public const int C_MISSION_COUNTER_CHANGE = 300009;
    public const int C_ENABLE_BLOCK = 300010;
    public const int C_DISABLE_BLOCK = 300011;
    public const int C_MESSAGE_EVENT_LIST = 300012;
    public const int C_CHANGE_TO_NODE_GAME  = 300013;
    public const int C_CHANGE_TO_WORLD_GAME = 300014;
    public const int C_CHANGE_SCENE = 300015;
    public const int C_PLAY_STORY = 300016;

}
