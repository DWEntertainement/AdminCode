using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AdminCode.Controls
{
    public class CustomRichTextBox : RichTextBox
    {
        private const string NAME = "RtbTextFileContents";
        public CustomRichTextBox()
        {
            Name = NAME;
            AcceptsTab = true;
            Font = new Font("Arial", 12.0f, FontStyle.Regular);
            Dock = DockStyle.Fill;
            BorderStyle= BorderStyle.None;
            ContextMenuStrip = new RichTextBoxContextMenuStrip(this);
        }
    }
}
