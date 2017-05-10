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
    public partial class ExchangeMessageDialog : Form
    {
        public ExchangeMessageDialog()
        {
            InitializeComponent();
        }

        public void Show(ExchangeMessage msg)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            if(msg != null)
            {
                textBox1.Text = msg.Sender;
                textBox2.Text = msg.Receipient;

                StringBuilder sb = new StringBuilder();

                foreach(ExchangeEvent ee in msg.Events)
                {
                    sb.AppendLine(ee.ToString());
                }

                textBox3.Text = sb.ToString();
            }

            this.Show();
        }
    }
}
