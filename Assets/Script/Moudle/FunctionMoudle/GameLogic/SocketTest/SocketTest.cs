using UnityEngine;
using System.Collections;

public class SocketTest : MonoBehaviour
{
    public string m_strIp;
    public int m_iPort;

	// Use this for initialization
	void Start ()
    {
        TimeManager.Instance.Initialize();
        LogManager.Instance.Initialize(true);
        TickTaskManager.Instance.InitializeTickTaskSystem();
        StageManager.Instance.Initialize();
        //SceneManager.Instance.Initialize();
        MessageManager.Instance.Initialize();
        //WindowManager.Instance.Initialize();
        SystemMsgHandler.Instance.RegisterSystemMsg();
        //AudioPlayer.Instance.Initialize();
    }
	
	// Update is called once per frame
	void Update ()
    {
        TickTaskManager.Instance.Update();
	}
    void OnGUI()
    {
        m_strIp = GUI.TextArea(new Rect(0, 0, 100, 50), m_strIp);
        m_iPort = int.Parse(GUI.TextArea(new Rect(0, 50, 100, 50), m_iPort.ToString()));
        if (GUI.Button(new Rect(0,100,100,50),"Connect"))
        {
            NetWorkManager.Instance.Connect(m_strIp, m_iPort);
        }
        GUI.TextArea(new Rect(0, 150, 100, 50), "IsConnect : " + NetWorkManager.Instance.IsConnected());
        if (GUI.Button(new Rect(0, 200, 100, 50), "disconnect"))
        {
            NetWorkManager.Instance.Disconnect();
        }
    }
}
