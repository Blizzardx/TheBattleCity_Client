using UnityEngine;
using System.Collections;

public class WallCom : MonoBehaviour
{
    private static int index = 0;
    public int m_Id;

    void Awake()
    {
        m_Id = ++ index;
    }
	// Use this for initialization
	void Start ()
    {
	
	}
}
