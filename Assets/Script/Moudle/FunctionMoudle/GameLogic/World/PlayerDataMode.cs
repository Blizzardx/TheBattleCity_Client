using NetWork.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PlayerDataMode : Singleton<PlayerDataMode>
{
    public bool isConnected;
    public int playerUid;
    public List<PlayerInfo> m_PlayerList;
}