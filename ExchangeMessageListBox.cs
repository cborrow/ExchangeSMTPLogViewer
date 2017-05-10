using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ExchangeSMTPLogViewer
{
    public class ExchangeMessageListBox : ListBox
    {
        public ExchangeMessageListBox() : base()
        {

        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= this.Items.Count)
                return;

            ExchangeMessage msg = (ExchangeMessage)this.Items[e.Index];

            e.DrawBackground();
            e.DrawFocusRectangle();

            Color foreColor = Color.Black;

            if (e.State == DrawItemState.Selected)
                foreColor = Color.White;

            TextRenderer.DrawText(e.Graphics, "From : " + msg.Sender, this.Font, new Rectangle(e.Bounds.Location, new Size(250, e.Bounds.Height)), this.ForeColor,
                Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);

            TextRenderer.DrawText(e.Graphics, "To : " + msg.Receipient, this.Font, new Rectangle(e.Bounds.X + 255, e.Bounds.Y, 250, e.Bounds.Height), this.ForeColor,
                Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);

            if(msg.Delivered)
            {
                TextRenderer.DrawText(e.Graphics, "Message Queued for Delivery", this.Font, new Rectangle(e.Bounds.X + 510, e.Bounds.Y, 250, e.Bounds.Height),
                    this.ForeColor, Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }

            base.OnDrawItem(e);
        }
    }
}
