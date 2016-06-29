using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Event
{
    public class EventElement
    {
        public EventElement(int id, object param)
        {
            eventId = id;
            eventParam = param;
        }

        public int eventId;
        public object eventParam;
    }
}