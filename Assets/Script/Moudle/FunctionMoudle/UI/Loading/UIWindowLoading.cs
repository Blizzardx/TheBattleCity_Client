using UnityEngine;
using System.Collections.Generic;

public class UIWindowLoading : WindowBase
{
    private UISlider m_Slider;

    public override void OnInit()
    {
        base.OnInit();
        m_Slider = FindChildComponent<UISlider>("ProgressBar");

    }
    public override void OnOpen(object param)
    {
        base.OnOpen(param);
        UITickTask.Instance.RegisterToUpdateList(Update);
    }
    public override void OnClose()
    {
        base.OnClose();
        UITickTask.Instance.UnRegisterFromUpdateList(Update);
    }
    private void Update()
    {
        //m_Slider.value = SceneManager.Instance.GetLoadingSceneProcess();        
    }
}

public class ListTestItem : UIListItemBase
{
    
}
