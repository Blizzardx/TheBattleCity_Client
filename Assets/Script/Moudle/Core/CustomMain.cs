using Common.Tool;
using Framework.Network;
using UnityEngine;

public class CustomMain:Singleton<CustomMain>
{
    public void Initialize()
    {
        HandlerManager.Instance.CheckInit();
        ModelManager.Instance.CheckInit();

        // change to scene main
       SceneManager.Instance.LoadScene<SceneMenu>();
    }
    public void OnAppQuit()
    {
        if (NetworkManager.Instance.IsConnect())
        {
            NetworkManager.Instance.Disconnect();
        }
    }
}
