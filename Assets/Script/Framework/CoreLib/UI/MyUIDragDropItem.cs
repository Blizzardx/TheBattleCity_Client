using Common.Component;
using UnityEngine;
using System.Collections;
using System;
using Common.Tool;

public class MyUIDragDropItem : UIDragDropItem
{
    private Action<bool> pressAction = null;
    private Action<MyUIDragDropItem> dragStartAction = null;
    private Action<Vector3> dragAction = null;
    private Action<MyUIDragDropItem> dragEndAction = null;
    private bool isPressed;

    public void RegisterPressAction(Action<bool> _action)
    {
        pressAction = _action;
    }

    public void RegisterDragStartAction(Action<MyUIDragDropItem> _action)
    {
        dragStartAction = _action;
    }

    public void RegisterDragAction(Action<Vector3> _action)
    {
        dragAction = _action;
    }

    public void RegisterDragEndAction(Action<MyUIDragDropItem> _action)
    {
        dragEndAction = _action;
    }

    protected void OnPressFun(bool _isPress)
    {
        isPressed = _isPress;
        if (!isPressed && pressAction != null)
        {
            pressAction(_isPress);
        }
        if (isPressed && pressAction != null)
        {
            Timer _timer = TimerCollection.GetInstance().Create(PressTimerFun, true, null);
            _timer.Start(pressAndHoldDelay);
        }
    }

    private void PressTimerFun()
    {
        if (isPressed && pressAction != null)
        {
            pressAction(isPressed);
        }
    }

    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();
        mTouchID = UICamera.currentTouchID;
        mParent = mTrans.parent;
        mRoot = NGUITools.FindInParents<UIRoot>(mParent);
        if (dragStartAction != null)
        {
            dragStartAction(this);
        }
    }

    protected override void OnDragDropMove(Vector3 delta)
    {
        base.OnDragDropMove(delta);
        if (dragAction != null)
        {
            dragAction(delta);
        }
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        mTouchID = int.MinValue;
        if (dragEndAction != null)
        {
            dragEndAction(this);
        }
    }
}
