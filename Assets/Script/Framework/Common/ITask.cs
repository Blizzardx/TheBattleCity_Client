using System;

namespace Framework.Common
{
    public interface ITask
    {
        int GetTaskType();
        object GetTaskParam();
        object GetResult();
        // id - param - result - error exception
        Action<int, object, object, Exception> GetCompletedCallBack();
        int GetGroupId();
    }
}
