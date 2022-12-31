using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminCode.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string MENU_NAME = "MainMenuStrip";
        public MainMenuStrip() 
        {
            Name = MENU_NAME;
            Dock = DockStyle.Top;

            FileDropDownMenu();
            EditDropDownMenu();
            AdminCodeDownMenu();
            ExtensionDownMenu();
        }

        public void FileDropDownMenu()
        {
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newMenu = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var openMenu = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var saveMenu = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var saveAsMenu = new ToolStripMenuItem("Enregistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.N);
            var quitMenu = new ToolStripMenuItem("Quitter", null, null, Keys.Control | Keys.F4);

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newMenu, openMenu, saveMenu, saveAsMenu, quitMenu });

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDownMenu() 
        {
            var editDropDownMenu = new ToolStripMenuItem("Edition");

            var cancelMenu = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var restoreMenu = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);
            

            editDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { cancelMenu, restoreMenu });

            Items.Add(editDropDownMenu);
        }

        public void AdminCodeDownMenu()
        {
            var admincodeDropDownMenu = new ToolStripMenuItem("Admin Code");

            var consolMenu = new ToolStripMenuItem("Console", null, null, Keys.Control | Keys.T);
            var debugMenu = new ToolStripMenuItem("debbogue", null, null, Keys.Control | Keys.P);


            admincodeDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { consolMenu, debugMenu });

            Items.Add(admincodeDropDownMenu);
        }

        public void ExtensionDownMenu()
        {
            var extensionDropDownMenu = new ToolStripMenuItem("Extension");

            var nugetMenu = new ToolStripMenuItem("Nuget Net Core", null, null, Keys.Control | Keys.X);
            


            extensionDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { nugetMenu });

            Items.Add(extensionDropDownMenu);
        }
    }
}
