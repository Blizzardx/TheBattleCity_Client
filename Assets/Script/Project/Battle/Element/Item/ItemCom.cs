using UnityEngine;
using System.Collections;

public class ItemCom : MonoBehaviour
{

    private static int index = 0;
    public int m_Id;

    void Awake()
    {
        m_Id = ++index;
    }
}
