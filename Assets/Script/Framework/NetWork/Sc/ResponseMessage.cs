using NetWork.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Thrift.Protocol;

namespace NetWork
{
    public class ResponseMessage
    {
        public HttpStatusCode StatusCode { get; set; }

        public Header Header { get; set; }

        public TBase Message { get; set; }

        public MEventList EventList { get; set; }

        public int MessageId { get; set; }

        public Exception Ex { get; set; }
    }
}
