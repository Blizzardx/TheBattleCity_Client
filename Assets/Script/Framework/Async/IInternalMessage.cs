using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Async
{
    /// <summary>
    /// 系统内部消息接口
    /// </summary>
    public interface IInternalMessage
    {
        int GetMessageId();
    }
}
