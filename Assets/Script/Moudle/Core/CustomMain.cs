using Framework.Common;
using Framework.Event;
using Framework.Network;
using UnityEngine;

public class CustomMain : SystemEventTrigger
{
    public int GetSortId()
    {
        return 0;
    }
    public void Init()
    {
        Debug.Log("register custom main event");
        EventDispatcher.Instance.RegistEvent(EventIdDefine.AppInit, OnAppInit);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.AppQuit, OnAppQuit);
    }

    private void OnAppInit(EventElement obj)
    {
        Initialize();
    }
    private void OnAppQuit(EventElement obj)
    {
        OnAppQuit();
    }
    private void Initialize()
    {
        HandlerManager.Instance.CheckInit();
        ModelManager.Instance.CheckInit();

        // change to scene main
       SceneManager.Instance.LoadScene<SceneMenu>();
    }
    private void OnAppQuit()
    {
        if (NetworkManager.Instance.IsConnect())
        {
            NetworkManager.Instance.Disconnect();
        }
    }

}
