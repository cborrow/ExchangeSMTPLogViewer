using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ExchangeSMTPLogViewer
{
    public class ExchangeMessageList : ICollection<ExchangeMessage>
    {
        List<ExchangeMessage> messages;

        public ExchangeMessage this[int index]
        {
            get { return messages[index];  }
        }

        public ExchangeMessageList()
        {
            messages = new List<ExchangeMessage>();
        }

        public int Count
        {
            get { return messages.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool HasMessageWithSession(string sessionId)
        {
            foreach(ExchangeMessage msg in messages)
            {
                if (msg.SessionID == sessionId)
                    return true;
            }
            return false;
        }

        public ExchangeMessage GetMessageForSession(string sessionId)
        {
            foreach(ExchangeMessage msg in messages)
            {
                if (msg.SessionID == sessionId)
                    return msg;
            }

            ExchangeMessage newMsg = new ExchangeMessage();
            newMsg.SessionID = sessionId;
            return newMsg;
        }

        public void GetMessageForSession(string sessionId, ref ExchangeMessage hMsg)
        {
            foreach(ExchangeMessage msg in messages)
            {
                if (msg.SessionID == sessionId)
                {
                    hMsg = msg;
                    return;
                }
            }

            ExchangeMessage newMsg = new ExchangeMessage();
            newMsg.SessionID = sessionId;
            hMsg = newMsg;
        }

        public void Add(ExchangeMessage item)
        {
            for(int i = 0; i < messages.Count; i++)
            {
                if (messages[i].SessionID == item.SessionID)
                {
                    messages[i] = item;
                    return;
                }
            }
            messages.Add(item);
        }

        public void AddRange(IEnumerable<ExchangeMessage> msgList)
        {
            foreach(ExchangeMessage msg in msgList)
            {
                messages.Add(msg);
            }
        }

        public void Clear()
        {
            messages.Clear();
        }

        public bool Contains(ExchangeMessage item)
        {
            return messages.Contains(item);
        }

        public void CopyTo(ExchangeMessage[] array, int arrayIndex)
        {
            messages.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ExchangeMessage> GetEnumerator()
        {
            return (IEnumerator<ExchangeMessage>)messages.GetEnumerator();
        }

        public bool Remove(ExchangeMessage item)
        {
            return messages.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return messages.GetEnumerator();
        }
    }
}
