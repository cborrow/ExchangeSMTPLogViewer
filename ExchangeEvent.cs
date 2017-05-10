using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeSMTPLogViewer
{
    public struct ExchangeEvent
    {
        public DateTime DateTime;
        public string ConnectorID;
        public string SessionID;
        public int Sequence;
        public string LocalEndPoint;
        public string RemoteEndPoint;
        public string Event;
        public string Data;

        public string EventType()
        {
            if (this.Event == "<")
                return "Client";
            else if (this.Event == ">")
                return "Server";
            else if (this.Event == "*")
                return "Info";
            else
                return "Other";
        }

        public override string ToString()
        {
            return string.Format("{0} {1}: {2}", DateTime.ToString(), EventType(), Data);
        }
    }
}
