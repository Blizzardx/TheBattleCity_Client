using UnityEngine;
using System.Collections;

public class CustomMain : Singleton<CustomMain>
{
    public void Initialize()
    {
        //register item
        ItemRegister.Register();

        StageManager.Instance.ChangeState(GameStateType.LoginState);

    }
    public void Quit()
    {
         
    }
}
