using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Item_3_Add_Speed_15 : ItemBase
{
    public static void Itemhandler(object param)
    {
        if (param is Player)
        {
            Player player = param as Player;
            player.SetSpeed(1.5f);
        }
    }
}