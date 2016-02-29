using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour 
{
    public  string m_sCameraTag = "MainCamera";
    private Camera m_SceneCamera;

	// Use this for initialization
	void Start () 
    {
        m_SceneCamera = GameObject.FindGameObjectWithTag(m_sCameraTag).GetComponent<Camera>();
        if( null == m_SceneCamera )
        {
            DebugLog.LogError("can't find scene camera on bill board");
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //face to camera
        transform.forward = m_SceneCamera.transform.forward;
	}
}
