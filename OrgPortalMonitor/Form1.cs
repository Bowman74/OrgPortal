﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrgPortalMonitor
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private Installer _installer;
    private bool _reallyClose;

    private void Form1_Load(object sender, EventArgs e)
    {
      this.ShowInTaskbar = false;
      this.Visible = false;

      this.notifyIcon1.Visible = true;
      var menu = this.notifyIcon1.ContextMenu = new ContextMenu();
      menu.MenuItems.Add(new MenuItem { Text = "Open log window", Name = "OpenItem", DefaultItem = true });
      menu.MenuItems.Add(new MenuItem { Text = "Refresh app list", Name = "RefreshAppList" });
      menu.MenuItems.Add(new MenuItem { Text = "Get developer license", Name = "GetDevLicense" });
      menu.MenuItems.Add(new MenuItem { Text = "Exit", Name = "ExitItem" });
      menu.MenuItems[0].Click += (o, a) => DisplayForm();
      menu.MenuItems[1].Click += (o, a) => RefreshAppList();
      menu.MenuItems[2].Click += (o, a) => GetDevLicense();
      menu.MenuItems[3].Click += (o, a) =>
        {
          _reallyClose = true;
          this.Close();
        };

      _installer = new Installer(this.notifyIcon1, this.textBox1, this.fileSystemWatcher1);
      _installer.StartFileWatcher();
    }

    private void RefreshAppList()
    {
      _installer.GetInstalledPackages();
    }

    private void GetDevLicense()
    {
      _installer.GetDevLicense();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!_reallyClose)
      {
        this.ShowInTaskbar = false;
        this.Visible = false;
        e.Cancel = true;
      }
    }

    private void notifyIcon1_DoubleClick(object sender, EventArgs e)
    {
      DisplayForm();
    }

    private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
    {
      DisplayForm();
    }

    private void DisplayForm()
    {
      this.ShowInTaskbar = true;
      this.WindowState = FormWindowState.Normal;
      this.Visible = true;
      this.BringToFront();
    }
  }
}
