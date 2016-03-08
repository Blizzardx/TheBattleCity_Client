using UnityEngine;
using System.Collections;

public class CustomMain : Singleton<CustomMain>
{
    public void Initialize()
    {
        StageManager.Instance.ChangeState(GameStateType.BattleState);

        //connet to server test code
        NetWorkManager.Instance.Connect("127.0.0.1", 8000);

    }
    public void Quit()
    {
         
    }
}
