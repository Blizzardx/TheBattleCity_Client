using UnityEngine;
using System.Collections;

public class Item_0_Add_hp_5 : ItemBase
{
    public static void Itemhandler(object param)
    {
        if(param is Player)
        {
            Player player = param as Player;
            player.AddHp(5);

            //trigger to update ui
            MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_UPDATE_PLAYER_UI, null));
        }
    }
}
