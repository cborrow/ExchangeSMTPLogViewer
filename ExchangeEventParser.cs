using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeSMTPLogViewer
{
    public class ExchangeEventParser
    {
        public static ExchangeEvent ParseLine(string line)
        {
            ExchangeEvent ee = new ExchangeEvent();
            string[] data = line.Split(',');

            if(data.Length == 9)
            {
                ee.DateTime = DateTime.Parse(data[0]);
                ee.ConnectorID = data[1];
                ee.SessionID = data[2];
                ee.Sequence = int.Parse(data[3]);
                ee.LocalEndPoint = data[4];
                ee.RemoteEndPoint = data[5];
                ee.Event = data[6];
                ee.Data = data[7];
            }

            return ee;
        }
    }
}
