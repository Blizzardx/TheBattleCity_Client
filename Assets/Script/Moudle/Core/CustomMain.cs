using Common.Tool;
using UnityEngine;

public class CustomMain:Singleton<CustomMain>
{
    public void Initialize()
    {
        // change to scene main
       SceneManager.Instance.LoadScene<SceneMenu>();
    }
    public void OnAppQuit()
    {
        
    }
}
