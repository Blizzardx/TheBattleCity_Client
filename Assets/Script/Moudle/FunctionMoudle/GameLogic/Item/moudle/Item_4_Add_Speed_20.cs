using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Item_4_Add_Speed_20 : ItemBase
{
    public static void Itemhandler(object param)
    {
        if (param is Player)
        {
            Player player = param as Player;
            player.SetSpeed(2.0f);
        }
    }
}