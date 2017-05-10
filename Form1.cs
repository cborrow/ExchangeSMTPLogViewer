using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExchangeSMTPLogViewer
{
    public partial class Form1 : Form
    {
        ExchangeMessageList messageList;
        ExchangeMessageDialog messageDialog;
        ExchangeMessageFindDialog messageFindDialog;

        string logFile = string.Empty;

        bool showNoSenders = true;
        bool showNoReceipients = true;

        public Form1()
        {
            InitializeComponent();
            messageList = new ExchangeMessageList();

            exchangeMessageListBox1.DoubleClick += ExchangeMessageListBox1_DoubleClick;
        }

        public void SetOpenFile(string file)
        {
            logFile = file;
            this.Text = "Exchange SMTP Log Viewer [" + logFile + "]";
        }

        public void LoadFile(string file)
        {
            if (!File.Exists(file))
            {
                MessageBox.Show("File not found : " + file);
                return;
            }

            messageList.Clear();
            SetOpenFile(file);

            using(StreamReader sr = new StreamReader(file))
            {
                int lineNum = 0;
                ExchangeMessage msg = new ExchangeMessage();

                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (line.StartsWith("#"))
                        continue;

                    ExchangeEvent ee = ExchangeEventParser.ParseLine(line);

                    lineNum++;

                    if (ee.SessionID != string.Empty && ee.SessionID != null)
                    {
                        if(msg.SessionID == null || msg.SessionID == string.Empty)
                            msg.SessionID = ee.SessionID;
                        else if(msg.SessionID != null && msg.SessionID != ee.SessionID)
                        {
                            messageList.Add(msg);

                            if (messageList.HasMessageWithSession(ee.SessionID))
                                msg = messageList.GetMessageForSession(ee.SessionID);
                            else
                                msg = new ExchangeMessage();
                            msg.SessionID = ee.SessionID;
                        }
                        msg.AddLogEvent(ee);
                    }
                }
                sr.Close();
            }

            exchangeMessageListBox1.Items.AddRange(messageList.ToArray());
        }

        public IEnumerable<ExchangeMessage> CurrentView()
        {
            if (!showNoSenders)
                return messageList.Where(msg => msg.Sender != null && msg.Sender.Contains("@"));
            if (!showNoReceipients)
                return messageList.Where(msg => msg.Receipient != null && msg.Receipient.Contains("@"));
            if (!showNoSenders && !showNoReceipients)
                return messageList.Where(msg => (msg.Sender != null && msg.Sender.Contains("@")) && (msg.Receipient != null && msg.Receipient.Contains("@")));
            return messageList;
        }

        public void UpdateView()
        {
            exchangeMessageListBox1.Items.Clear();
            exchangeMessageListBox1.Items.AddRange(CurrentView().ToArray());
        }

        public void ShowView(IEnumerable<ExchangeMessage> items)
        {
            exchangeMessageListBox1.Items.Clear();
            exchangeMessageListBox1.Items.AddRange(items.ToArray());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                LoadFile(file);
            }
        }

        private void exchangeMessageListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ExchangeMessageListBox1_DoubleClick(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = exchangeMessageListBox1.SelectedItems;

            if(selectedItems.Count > 0)
            {
                ExchangeMessage msg = (ExchangeMessage)selectedItems[0];
                messageDialog = new ExchangeMessageDialog();
                messageDialog.Show(msg);
            }
        }

        private void showItemsWithoutSenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showNoSenders = showItemsWithoutSenderToolStripMenuItem.Checked;
            UpdateView();
        }

        private void showNoReceipentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showNoReceipients = showNoReceipentsToolStripMenuItem.Checked;
            UpdateView();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            messageFindDialog = new ExchangeMessageFindDialog();

            if (messageFindDialog.ShowDialog() == DialogResult.OK)
            {
                IEnumerable<ExchangeMessage> msgList = CurrentView();

                if (messageFindDialog.SenderQuery != null)
                    msgList = msgList.Where(msg => (msg.Sender != null && msg.Sender.Contains(messageFindDialog.SenderQuery)));
                if (messageFindDialog.ReceipientQuery != null)
                    msgList = msgList.Where(msg => (msg.Receipient != null && msg.Receipient.Contains(messageFindDialog.ReceipientQuery)));
                if (messageFindDialog.LogQuery != null)
                    msgList = msgList.Where(msg => msg.LogContains(messageFindDialog.LogQuery));

                ShowView(msgList);
            }
        }
    }
}
