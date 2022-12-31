﻿using AdminCode.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminCode
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var menuStrip = new MainMenuStrip();
            var mainTabControl = new MainTabControl();

            mainTabControl.TabPages.Add("Onglet 1");

            Controls.AddRange(new Control[] {menuStrip, mainTabControl});
        }
    }
}
