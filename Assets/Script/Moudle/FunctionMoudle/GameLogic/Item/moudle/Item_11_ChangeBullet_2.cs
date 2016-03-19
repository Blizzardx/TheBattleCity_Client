using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Item_11_ChangeBullet_2 : ItemBase
{
    public static void Itemhandler(object param)
    {
        if (param is Player)
        {
            Player player = param as Player;
            PlayerController control = PlayerController.GetPlayerController(player.gameObject.GetInstanceID());
            if (null != control)
            {
                control.SetBullet("Bullet_2");
            }
        }
    }
}