using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExchangeSMTPLogViewer
{
    public partial class ExchangeMessageFindDialog : Form
    {
        public string SenderQuery
        {
            get { return textBox1.Text; }
        }

        public string ReceipientQuery
        {
            get { return textBox2.Text; }
        }

        public string LogQuery
        {
            get { return textBox3.Text; }
        }

        public ExchangeMessageFindDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
