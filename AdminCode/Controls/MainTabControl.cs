using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminCode.Controls
{
    public class MainTabControl : TabControl
    {
        private const string NAME = "ManTabControl";
        private ContextMenuStrip _contextMenuStrip;
        private MainForm _form;
        public MainTabControl()
        {
            _contextMenuStrip = new TabControlContextMenuStrip();
            Name = NAME;
            ContextMenuStrip = _contextMenuStrip;
            Dock= DockStyle.Fill;

            HandleCreated += (s, e) =>
            {
                _form = FindForm() as MainForm;
            };

            SelectedIndexChanged += (s, e) =>
            {
                _form.CurrentFile = _form.Session.TextFiles[SelectedIndex];
                _form.RichTextBox = (CustomRichTextBox)_form.MainTabControl.TabPages[SelectedIndex].Controls.Find("RtbTextFileContents", true).First();
                _form.Text = $"{_form.CurrentFile.FileName} - AdminCode";
            };

            MouseUp += (s, e) =>
            {
                if(e.Button == MouseButtons.Right) 
                { 
                   for (int i = 0; i < TabCount; i++)
                    {
                        var rect = GetTabRect(i);
                        if(rect.Contains(e.Location))
                        {
                            SelectedIndex = i; 
                            break;
                        }
                    }
                }
            };
        }
    }
}
