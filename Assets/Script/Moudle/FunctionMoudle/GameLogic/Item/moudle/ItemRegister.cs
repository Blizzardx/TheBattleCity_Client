using UnityEngine;
using System.Collections;

public class ItemRegister
{
    public static void Register()
    {
        ItemManager.Instance.RegisterItem(0, "Item/Item_0_Add_hp_5", Item_0_Add_hp_5.Itemhandler);
        ItemManager.Instance.RegisterItem(1, "Item/Item_1_Add_hp_10", Item_1_Add_hp_10.Itemhandler);
        ItemManager.Instance.RegisterItem(2, "Item/Item_2_Add_hp_20", Item_2_Add_hp_20.Itemhandler);
        ItemManager.Instance.RegisterItem(3, "Item/Item_3_Add_Speed_1.5", Item_3_Add_Speed_15.Itemhandler);
        ItemManager.Instance.RegisterItem(4, "Item/Item_4_Add_Speed_2", Item_4_Add_Speed_20.Itemhandler);
    }
	
}
