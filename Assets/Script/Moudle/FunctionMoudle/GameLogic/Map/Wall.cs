using System;
using UnityEngine;
using System.Collections.Generic;

public enum WallType
{
    Indestructible,
    Destructible,
}
public class Wall : MonoBehaviour
{
    [SerializeField]
    private WallType        m_Type;


    public WallType GetWallType()
    {
        return m_Type;
    }
    public void DestroyWall()
    {
        Destroy(gameObject);
    }
	// Use this for initialization
	void Start () {
	
	} 
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HandlerBullet(Bullet elem)
    {
        if (GetWallType() == WallType.Indestructible)
        {
            // destroy this
            elem.CollectionBullet();
        }
        else if (GetWallType() == WallType.Destructible)
        {
            // destroy this
            elem.CollectionBullet();

            //destroy wall
            DestroyWall();
        }
    }
}
