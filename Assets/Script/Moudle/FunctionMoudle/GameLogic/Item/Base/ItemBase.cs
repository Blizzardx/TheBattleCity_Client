using UnityEngine;
using System.Collections;

public class ItemBase : MonoBehaviour
{
    public int m_iId;
    public int m_iPosId;
    void OnTriggerEnter(Collider other)
    {
        Debuger.Log("item trigger " + other.name);

        Player player = other.GetComponent<Player>();
        if (null != player)
        {
            //trigger hit
            PlayerController control = PlayerController.GetPlayerController(other.gameObject.GetInstanceID());
            if (null != control )
            {
                //trigger to use item
                control.TriggerUseItem(m_iId,m_iPosId);

                //remove item
                ItemManager.Instance.RemoveItem(m_iId, m_iPosId);
            }
        }
    }
}
