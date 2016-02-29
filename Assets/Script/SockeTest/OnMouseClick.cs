using UnityEngine;
using System.Collections;

public class OnMouseClick : MonoBehaviour 
{
    public enum BtnStatus
    {
        Int16,
        Int32,
        Int64,
        Float,
        Double,
        String,
        Byte,
        Connect,
        Disconnect,
        CustomPkg,
        Exit,
    }
    public BtnStatus m_BtnStatus;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void OnClick()
    {
        switch (m_BtnStatus)
        {
            
            case BtnStatus.Connect:
                {
                    SocketMain.m_Instance.Connect();
                }
                break;
            case BtnStatus.Disconnect:
                {
                    SocketMain.m_Instance.Disconnect();
                }
                break;
            case BtnStatus.Exit:
                {
                    Application.Quit();
                }
                break;
            case BtnStatus.CustomPkg:
                {
                    SocketMain.m_Instance.SendCustomPkg();
                }
                break;
        }
    } 
}
