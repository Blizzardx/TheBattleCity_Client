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
        ItemManager.Instance.RegisterItem(5, "Item/Item_5_ChangeMesh_1", Item_5_ChangeMesh_1.Itemhandler);
        ItemManager.Instance.RegisterItem(6, "Item/Item_6_ChangeMesh_2", Item_6_ChangeMesh_2.Itemhandler);
        ItemManager.Instance.RegisterItem(7, "Item/Item_7_ChangeMesh_3", Item_7_ChangeMesh_3.Itemhandler);
        ItemManager.Instance.RegisterItem(8, "Item/Item_8_ChangeMesh_4", Item_8_ChangeMesh_4.Itemhandler);
        ItemManager.Instance.RegisterItem(9, "Item/Item_9_ChangeMesh_5", Item_9_ChangeMesh_5.Itemhandler);
        ItemManager.Instance.RegisterItem(10, "Item/Item_10_ChangeBullet_1", Item_10_ChangeBullet_1.Itemhandler);
        ItemManager.Instance.RegisterItem(11, "Item/Item_11_ChangeBullet_2", Item_11_ChangeBullet_2.Itemhandler);
    }
	
}
