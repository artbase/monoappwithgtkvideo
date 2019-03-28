﻿using System;
using Gdk;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    public Rectangle GetDisplaySize()
    {
        // Use xrandr to get size of screen located at offset (0,0).
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.FileName = "xrandr";
        p.Start();
        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        var match = System.Text.RegularExpressions.Regex.Match(output, @"(\d+)x(\d+)\+0\+0");
        var w = match.Groups[1].Value;
        var h = match.Groups[2].Value;
        Rectangle r = new Rectangle();
        r.Width = int.Parse(w);
        r.Height = int.Parse(h);
        return r;
    }
}
