using System;
using Framework.Async;
using Framework.Common;

namespace Framework.Task
{
    public abstract class TaskHandlerBase : IAsyncTask
    {
        protected AsyncState            m_NextState;
        protected ITask                 m_Task;
        protected Exception             m_ErrorException;
        protected object                m_ResultParam;
        private Action<TaskHandlerBase> m_Callback;

        public void ExecTask(ITask task, Action<TaskHandlerBase> finishedCallback)
        {
            m_ErrorException = null;
            m_Task = task;
            m_Callback = finishedCallback;
            AsyncManager.Instance.ExecuteAsyncTask(this);
        }
        public virtual void QuickExecTask(ITask task, Action<TaskHandlerBase> finishedCallback)
        {
            OnQuickExec(task, finishedCallback);
        }
        public void Cancle()
        {
            OnCancle();
        }
        public int GetTaskGroupId()
        {
            return m_Task.GetGroupId();
        }
        public AsyncState BeforeAsyncTask()
        {
            try
            {
                m_NextState = AsyncState.DoAsync;
                OnPrepare();
            }
            catch (Exception e)
            {
                m_ErrorException = e;
                m_NextState = AsyncState.AfterAsync;
            }
            return m_NextState;
        }
        public AsyncState DoAsyncTask()
        {
            try
            {
                m_NextState = AsyncState.AfterAsync;
                OnExec();
            }
            catch (Exception e)
            {
                m_ErrorException = e;
                m_NextState = AsyncState.AfterAsync;
            }
            return m_NextState;
        }
        public AsyncState AfterAsyncTask()
        {
            try
            {
                m_NextState = AsyncState.Done;
                OnEnd();
            }
            catch (Exception e)
            {
                m_ErrorException = e;
                m_NextState = AsyncState.Done;
            }
            var callBack = m_Task.GetCompletedCallBack();
            if (callBack != null)
            {
                callBack(0, m_ResultParam, m_Task.GetTaskParam(), m_ErrorException);
            }
            m_Callback(this);
            return m_NextState;
        }
        public abstract void OnPrepare();
        public abstract void OnExec();
        public abstract void OnEnd();
        protected virtual void OnCancle()
        {
            
        }
        protected virtual void OnQuickExec(ITask task, Action<TaskHandlerBase> finishedCallback)
        {
            m_Task = task;
            m_Callback = finishedCallback;
            BeforeAsyncTask();
            DoAsyncTask();
            AfterAsyncTask();
        }
    }
}
