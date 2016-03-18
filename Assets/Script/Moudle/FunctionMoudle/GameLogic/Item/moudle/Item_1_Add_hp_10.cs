using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Item_1_Add_hp_10 : ItemBase
{
    public static void Itemhandler(object param)
    {
        if (param is Player)
        {
            Player player = param as Player;
            player.AddHp(10);

            //trigger to update ui
            MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_UPDATE_PLAYER_UI, null));
        }
    }
}