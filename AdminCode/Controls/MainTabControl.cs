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
        private const string TAB_CONTROL_NAME = "ManTabControl";
        public MainTabControl()
        {
            Name= TAB_CONTROL_NAME;
            Dock= DockStyle.Fill;
        }
    }
}
