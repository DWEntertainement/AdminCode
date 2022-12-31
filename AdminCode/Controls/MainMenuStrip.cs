using AdminCode.Objects;
using AdminCode.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminCode.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";
        private MainForm _form;
        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;
        public MainMenuStrip() 
        {
            Name = NAME;
            Dock = DockStyle.Top;
            _openFileDialog = new OpenFileDialog();
            _saveFileDialog= new SaveFileDialog();

            FileDropDownMenu();
            EditDropDownMenu();
            AdminCodeDownMenu();
            ExtensionDownMenu();

            HandleCreated += (s, e) =>
            {
                _form = FindForm() as MainForm;
            };
        }

        public void FileDropDownMenu()
        {
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newFile = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var openFile = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var saveFile = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var saveAsFile = new ToolStripMenuItem("Enregistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.S);
            var quit = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            newFile.Click += (s, e) =>
            {
                var tabControl = _form.MainTabControl;
                var tabPagesCount = tabControl.TabPages.Count;
                var fileName = $"Sans titre {tabPagesCount + 1}";
                var file = new TextFile(fileName);
                var rtb = new CustomRichTextBox();

                tabControl.TabPages.Add(file.SafeFileName);
                _form.Session.TextFiles.Add(file);
                tabControl.TabPages[tabPagesCount].Controls.Add(rtb);
                tabControl.SelectedTab = tabControl.TabPages[tabPagesCount];

                
                _form.CurrentFile = file;
                _form.RichTextBox = rtb;
            };

            openFile.Click += async (s, e) =>
            {
                if(_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var tabControl = _form.MainTabControl;
                    var tabPagesCount = tabControl.TabPages.Count;

                    var file = new TextFile(_openFileDialog.FileName);

                    var rtb = new CustomRichTextBox();

                    _form.Text = $"{file.FileName} - AdminCode";
                    using(StreamReader reader = new StreamReader(file.FileName))
                    {
                        file.Contents = await reader.ReadToEndAsync();
                    }

                    rtb.Text = file.Contents;

                    tabControl.TabPages.Add(file.SafeFileName);
                    tabControl.TabPages[tabPagesCount].Controls.Add(rtb);

                    _form.Session.TextFiles.Add(file);
                    _form.RichTextBox = rtb;
                    _form.CurrentFile = file;
                    tabControl.SelectedTab = tabControl.TabPages[tabPagesCount];
                }
            };

            saveFile.Click += async (s, e) =>
            {
                var currentFile = _form.CurrentFile;
                var currentRtbText = _form.RichTextBox.Text;

                if(currentFile.Contents != currentRtbText)
                {
                    if(File.Exists(currentFile.FileName))
                    {
                        using (StreamWriter writer = File.CreateText(currentFile.FileName))
                        {
                            await writer.WriteAsync(currentFile.Contents);
                        }
                        currentFile.Contents = currentRtbText;
                        _form.Text = currentFile.Contents;
                        _form.MainTabControl.SelectedTab.Text = currentFile.SafeFileName;
                    }
                    else
                    {
                        saveAsFile.PerformClick();
                    }
                }
            };

            saveAsFile.Click += async (s, e) =>
            {
                if(_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var newFileName = _saveFileDialog.FileName;
                    var alreadyExists = false;

                    foreach(var file in _form.Session.TextFiles)
                    {
                        if(file.FileName == newFileName)
                        {
                            MessageBox.Show("Ce fichier est déjà ouvert dans AdminCode", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        var file = new TextFile(newFileName) { Contents = _form.RichTextBox.Text };
                        var oldFile = _form.Session.TextFiles.Where(x => x.FileName == _form.CurrentFile.FileName).First();
                        
                        _form.Session.TextFiles.Replace(oldFile, file);

                        using (StreamWriter writer = File.CreateText(file.FileName))
                        {
                            await writer.WriteAsync(file.Contents);
                        }
                        _form.MainTabControl.SelectedTab.Text = file.SafeFileName;
                        _form.Text = file.FileName;
                        _form.CurrentFile = file;
                    }

                    
                }
            };

            quit.Click += (s, e) =>
            {
                Application.Exit();
            };

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newFile, openFile, saveFile, saveAsFile, quit });

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDownMenu() 
        {
            var editDropDownMenu = new ToolStripMenuItem("Edition");

            var cancel = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var restore = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);

            cancel.Click += (s, e) => { if (_form.RichTextBox.CanUndo) _form.RichTextBox.Undo(); };
            restore.Click += (s, e) => { if (_form.RichTextBox.CanRedo) _form.RichTextBox.Redo(); };

            editDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { cancel, restore });

            Items.Add(editDropDownMenu);
        }

        public void AdminCodeDownMenu()
        {
            var admincodeDropDownMenu = new ToolStripMenuItem("Admin Code");

            var console = new ToolStripMenuItem("Console", null, null, Keys.Control | Keys.T);
            var debug = new ToolStripMenuItem("debbogue", null, null, Keys.Control | Keys.P);

            console.Click += (s, e) => { };

            debug.Click += (s, e) => { };

            admincodeDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { console, debug });

            Items.Add(admincodeDropDownMenu);
        }

        public void ExtensionDownMenu()
        {
            var extensionDropDownMenu = new ToolStripMenuItem("Extension");

            var nugetMenu = new ToolStripMenuItem("Nuget Net Core", null, null, Keys.Control | Keys.X);

            nugetMenu.Click += (s, e) => { };

            extensionDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { nugetMenu });

            Items.Add(extensionDropDownMenu);
        }
    }
}
