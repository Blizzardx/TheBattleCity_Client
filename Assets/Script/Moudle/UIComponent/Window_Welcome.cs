using UnityEngine;
using System.Collections;

public class Window_Welcome : WindowBase
{
    private Button m_ButtonTouchToPlay;
    private Animator m_AnimatorControl;

    public override void OnOpen(System.Action OnFinshedCallBack, object paramter)
    {
        base.OnOpen(OnFinshedCallBack, paramter);
        m_ButtonTouchToPlay = FindChildComponent<Button>("Label_TouchToEnter");
        m_AnimatorControl = FindChildComponent<Animator>();
        m_ButtonTouchToPlay.AddCallBack(OnTouchPlay);

        //add udpate to update store
        WindowManager.GetInstance.RegisterToUpdateStore(Update);

        //set background music
        AudioManager.GetInstance.PlayBackgroundSound(BackgroundAudioDefine.AudioBackgroundSoundType.LogIn);

        OnFinshedCallBack();
    }
    public override void OnClose()
    {
        base.OnClose();

        //remove from udpate to update store
        WindowManager.GetInstance.UnRegisterFromUpdateStore(Update);
    }
    private void OnTouchPlay()
    {
        // play animation       
        m_AnimatorControl.Play("Welcom_Close");

    }
    private void Update()
    {
        if(m_AnimatorControl.GetCurrentAnimatorStateInfo(0).IsName("Welcom_Close"))
        {
            if( m_AnimatorControl.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                //trigger to close
                m_AnimatorControl.speed = 0.0f;
                WindowManager.GetInstance.OpenWindow(WindowType.SelectMode);
            }
        }
    }
}
