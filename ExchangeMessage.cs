using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeSMTPLogViewer
{
    public class ExchangeMessage
    {
        string sender;
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        string receipient;
        public string Receipient
        {
            get { return receipient; }
            set { receipient = value; }
        }

        string localEndPoint;
        public string LocalEndPoint
        {
            get { return localEndPoint; }
            set { localEndPoint = value; }
        }

        string remoteEndPoint;
        public string RemoteEndPoint
        {
            get { return remoteEndPoint; }
            set { remoteEndPoint = value; }
        }

        string sessionId;
        public string SessionID
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        List<ExchangeEvent> events;
        public List<ExchangeEvent> Events
        {
            get { return events; }
        }

        public string ExchangEventLogs
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(ExchangeEvent ee in events)
                {
                    sb.AppendLine(ee.ToString());
                }

                return sb.ToString();
            }
        }

        public ExchangeMessage()
        {
            events = new List<ExchangeEvent>();
        }

        public void AddLogEvent(ExchangeEvent ee) {
            if(this.events.Count == 0)
            {
                this.LocalEndPoint = ee.LocalEndPoint;
                this.RemoteEndPoint = ee.RemoteEndPoint;
                this.SessionID = ee.SessionID;
            }
            if(this.Sender == null)
            {
                if (ee.Data.Contains("MAIL FROM"))
                    this.Sender = ee.Data.Split(':')[1];
            }
            if(this.Receipient == null)
            {
                if (ee.Data.Contains("RCPT TO"))
                    this.Receipient = ee.Data.Split(':')[1];
            }
            this.events.Add(ee);
        }

        public bool LogContains(string query)
        {
            foreach(ExchangeEvent ee in events)
            {
                if (ee.Data.Contains(query))
                    return true;
            }
            return false;
        }

        public string SetLengthStr(string org, int length)
        {
            if(org == null)
            {
                org = "";
                for (int i = 0; i < length; i++)
                    org += " ";
            }
            else if(org.Length < length)
            {
                for (int i = 0; i < (length - org.Length); i++)
                    org += " ";
            }
            return org;
        }

        public override string ToString()
        {
            if(this.Events.Count > 0 && this.Events[0].DateTime != null)
                return string.Format("From: {0} To: {1} {2}", SetLengthStr(this.Sender, 100), SetLengthStr(this.Receipient, 100), this.events[0].DateTime);
            return string.Format("From: {0}, To: {1}", SetLengthStr(this.Sender, 100), SetLengthStr(this.Receipient, 100));
        }
    }
}
