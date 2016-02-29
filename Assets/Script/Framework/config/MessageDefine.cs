using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum MessageID
{
    // 0 - 1000 general pkg
    G_Position          = 0,
    G_Handler           = 1,
    G_RoleInfo          = 2,
    G_DissolutionRoom   = 3,

    // 1000 - 1999 server to client pkg
    STC_RegisterResult      = 1000,
    STC_LoginResult         = 1001,
    STC_CreateRoleResult    = 1002,
    STC_CreateOrEnterRoom   = 1003,
    STC_RoomList            = 1004,
    STC_Hurt                = 1005,
    STC_BeginBattle         = 1006,
    STC_ChangeBulletCompleted  = 1007,
    STC_Fire                = 1008,
    STC_RoomDetail          = 1009,

    // 2000 - 2999 client to server pkg
    CTS_PlayerInfo          = 2000,
    CTS_Register            = 2001,
    CTS_Login               = 2002,
    CTS_CreateRole          = 2003,
    CTS_CreateRoom          = 2004,
    CTS_EnterRoom           = 2005,
    CTS_RoomList            = 2006,
    CTS_ReadyToBattle       = 2007,
    CTS_RequestChangeBullet = 2008,
    CTS_BulletTrajectory    = 2009,
    CTS_ChangePosition      = 2010,

    // 3000 - 3900 custom msg
    C_ConnectToServerDone = 3000,
    C_ConnectToLANServerDone = 3001,
}