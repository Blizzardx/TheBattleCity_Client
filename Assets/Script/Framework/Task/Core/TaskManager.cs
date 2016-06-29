using System;
using System.Collections.Generic;
using Common.Tool;
using Framework.Common;
using Framework.Queue;
using UnityEngine;

namespace Framework.Task
{
    public enum QueueType
    {
        AllQueue,
        BlockQueue,
        UnblockQueue,
    }

    public class TaskManager : Singleton<TaskManager>
    {
        private Dictionary<int, Type>               m_HandlerMap;
        private TaskHandlerBase                     m_TaskHandlerInBlockQueue;
        private LinkedList<TaskHandlerBase>         m_TaskHandlerInUnblockQueue;
        private int m_ProcesserMaxCount = 4;

        #region public interface
        public TaskManager()
        {
            m_HandlerMap            = new Dictionary<int, Type>();
            m_TaskHandlerInUnblockQueue = new LinkedList<TaskHandlerBase>();
            AutoRegister();
        }
        public void ExecTask(ITask task,bool isInBlockQueue = true)
        {
            if (isInBlockQueue)
            {
                TaskQueue_Block.Instance.Enqueue(task);
            }
            else
            {
                TaskQueue_UnBlock.Instance.Enqueue(task);
            }
        }
        public void Update()
        {
            if (null == m_TaskHandlerInBlockQueue)
            {
                ITask task = TaskQueue_Block.Instance.Dequeue();
                if (null != task)
                {
                    // exec task
                    ExecBlockTask(task);
                }
            }
            if (m_TaskHandlerInUnblockQueue.Count < m_ProcesserMaxCount)
            {
                ITask task = TaskQueue_UnBlock.Instance.Dequeue();
                if (null != task)
                {
                    // exec task
                    ExecUnblockTask(task);
                }
            }
        }
        public void QuickFinishedAllTask()
        {
            while (true)
            {
                var task = TaskQueue_Block.Instance.Dequeue();
                if (null == task)
                {
                    break;
                }
                QuickExecTask(task);
            }
            while (true)
            {
                var task = TaskQueue_UnBlock.Instance.Dequeue();
                if (null == task)
                {
                    break;
                }
                QuickExecTask(task);
            }
        }
        public void CancleTaskByGroupId(int groupId, QueueType type = QueueType.AllQueue)
        {
            if (type == QueueType.AllQueue || type == QueueType.BlockQueue)
            {
                CancleTaskInBlockTaskQueueByGroupId(groupId);
            }
            if (type == QueueType.AllQueue || type == QueueType.UnblockQueue)
            {
                CancleTaskInUnblockTaskQueueByGroupId(groupId);
            }
        }
        #endregion

        #region system function
        private void CancleTaskInBlockTaskQueueByGroupId(int groupId)
        {
            if (m_TaskHandlerInBlockQueue != null && m_TaskHandlerInBlockQueue.GetTaskGroupId() == groupId)
            {
                m_TaskHandlerInBlockQueue.Cancle();
                // clear handler 
                m_TaskHandlerInBlockQueue = null;
            }
            int size = TaskQueue_Block.Instance.GetCount();
            for (int i = 0; i < size; ++i)
            {
                var elem = TaskQueue_Block.Instance.Dequeue();
                if (elem.GetGroupId() != groupId)
                {
                    TaskQueue_Block.Instance.Enqueue(elem);
                }
            }
        }
        private void CancleTaskInUnblockTaskQueueByGroupId(int groupId)
        {
            List<TaskHandlerBase> removingList = new List<TaskHandlerBase>();
            foreach (var handler in m_TaskHandlerInUnblockQueue)
            {
                if (handler != null && handler.GetTaskGroupId() == groupId)
                {
                    handler.Cancle();
                    removingList.Add(handler);
                }
            }
            foreach (var handler in removingList)
            {
                // clear from handler list
                m_TaskHandlerInUnblockQueue.Remove(handler);
            }

            int size = TaskQueue_UnBlock.Instance.GetCount();
            for (int i = 0; i < size; ++i)
            {
                var elem = TaskQueue_UnBlock.Instance.Dequeue();
                if (elem.GetGroupId() != groupId)
                {
                    TaskQueue_UnBlock.Instance.Enqueue(elem);
                }
            }
        }
        private void ExecBlockTask(ITask element)
        {
            //instance handler
            m_TaskHandlerInBlockQueue = GetHandler(element);
            if (null == m_TaskHandlerInBlockQueue)
            {
                return;
            }
            // begin task
            m_TaskHandlerInBlockQueue.ExecTask(element,OnTadkInBlocklistIsDone);
        }
        private void ExecUnblockTask(ITask element)
        {
            //instance handler
            var handler = GetHandler(element);
            if (null == handler)
            {
                return;
            }
            // add to list
            m_TaskHandlerInUnblockQueue.AddFirst(handler);
            // begin task
            m_TaskHandlerInBlockQueue.ExecTask(element, OnTadkInUnblocklistIsDone);

        }
        private void QuickExecTask(ITask element)
        {
            //instance handler
            var handler = GetHandler(element);
            if (null == handler)
            {
                return;
            }
            handler.QuickExecTask(element, (tmpHandler) => { });
        }
        private void OnTadkInBlocklistIsDone(TaskHandlerBase handlerBase)
        {
            // done & free
            m_TaskHandlerInBlockQueue = null;
        }
        private void OnTadkInUnblocklistIsDone(TaskHandlerBase handlerBase)
        {
            // done & free
            m_TaskHandlerInUnblockQueue.Remove(handlerBase);
        }
        private TaskHandlerBase GetHandler(ITask element)
        {
            // get task handler
            int id = element.GetTaskType();
            Type handler = null;
            if (!m_HandlerMap.TryGetValue(id, out handler))
            {
                Debug.LogError("can't exec tadk id " + id + " named: " + element.GetType());
                return null;
            }
            //instance handler
            return Activator.CreateInstance(handler) as TaskHandlerBase;
        }
        private void AutoRegister()
        {
            // do sth
            var list = ReflectionManager.Instance.GetTypeByBase(typeof (TaskHandlerBase));
            // decode type by name
            for (int i = 0; i < list.Count; ++i)
            {
                int id = DecodeClassnameToId(list[i].Name);
                if (id != -1 && !m_HandlerMap.ContainsKey(id))
                {
                    m_HandlerMap.Add(id, list[i]);
                }
                else
                {
                    Debug.Log("Already exist taskhandler with id " + id);
                }
            }
        }
        private int DecodeClassnameToId(string name)
        {
            int id = -1;
            var list = name.Split('_');
            if (list.Length > 1)
            {
                if (int.TryParse(list[1], out id))
                {
                    return id;
                }
            }
            Debug.LogError("can't decode taskhandler by class name " + name);
            return id;
        }
        #endregion
    }
}
