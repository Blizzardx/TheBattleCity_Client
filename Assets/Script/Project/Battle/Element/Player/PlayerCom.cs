using UnityEngine;
using System.Collections;

public class PlayerCom : MonoBehaviour
{
    public Vector3 Size;

    void Awake()
    {
        Size = GetComponent<BoxCollider>().size;
        Size *= 0.5f;
    }
}
