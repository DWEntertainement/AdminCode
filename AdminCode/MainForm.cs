using AdminCode.Controls;
using AdminCode.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminCode
{
    public partial class MainForm : Form
    {
        public RichTextBox RichTextBox;
        public TextFile CurrentFile;
        public TabControl MainTabControl;
        public Session Session;

        public MainForm()
        {
            InitializeComponent();
            

            var menuStrip = new MainMenuStrip();
            MainTabControl = new MainTabControl();

            Controls.AddRange(new Control[] { MainTabControl, menuStrip });

            Initializefile();
        }

        private async void Initializefile()
        {
            Session = await Session.Load();
            

            if (Session.TextFiles.Count == 0)
            {
                var file = new TextFile("Sans titre 1");
                

                MainTabControl.TabPages.Add(file.SafeFileName);

                var tabPage = MainTabControl.TabPages[0];
                var rtb = new CustomRichTextBox();
                tabPage.Controls.Add(rtb);
                rtb.Select();

                Session.TextFiles.Add(file);

                CurrentFile = file;
                RichTextBox = rtb;
            }
            else
            {
                var activeIndex = Session.ActiveIndex;
                foreach (var file in Session.TextFiles)
                {
                    if (File.Exists(file.FileName) || File.Exists(file.BackupFileName))
                    {
                        var rtb = new CustomRichTextBox();
                        var tabPageCount = MainTabControl.TabPages.Count;

                        MainTabControl.TabPages.Add(file.SafeFileName);
                        MainTabControl.TabPages[tabPageCount].Controls.Add(rtb);

                        rtb.Text = file.Contents;
                    }
                }
                CurrentFile = Session.TextFiles[activeIndex];
                RichTextBox = (CustomRichTextBox)MainTabControl.TabPages[activeIndex].Controls.Find("RtbTextFileContents", true).First();
                RichTextBox.Select();

                MainTabControl.SelectedIndex = activeIndex;
                Text = $"{CurrentFile.FileName} - AdminCode";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Session.ActiveIndex = MainTabControl.SelectedIndex;
            Session.Save();

            foreach (var file in Session.TextFiles)
            {
                var fileIndex = Session.TextFiles.IndexOf(file);
                var rtb = MainTabControl.TabPages[fileIndex].Controls.Find("RtbTextFileContents", true).First();

                if(file.FileName.StartsWith("Sans titre"))
                {
                    file.Contents = rtb.Text;
                    Session.BackupFile(file);
                }
            }
        }
    }
}
