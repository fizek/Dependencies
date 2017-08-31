﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;

namespace Dependencies
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Process command line args
            if (e.Args.Length > 0)
            {
                mainWindow.OpenNewDependencyWindow(e.Args[0]);
                
            }
        }

        public static void AddToRecentDocuments(String Filename)
        {
            // Create custom task
            JumpTask item = new JumpTask();
            item.Title = System.IO.Path.GetFileName(Filename);
            item.Description = Filename;
            item.ApplicationPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            item.Arguments = Filename;
            item.CustomCategory = "Tasks";
            

            // Add document to recent category
            JumpList RecentsDocs = JumpList.GetJumpList(Application.Current);
            RecentsDocs.JumpItems.Add(item);
            JumpList.AddToRecentCategory(item);
            RecentsDocs.Apply();

            // Store a copy in application settings, ring buffer style
            Dependencies.Properties.Settings.Default.RecentFiles[Dependencies.Properties.Settings.Default.RecentFilesIndex] = Filename;
            Dependencies.Properties.Settings.Default.RecentFilesIndex = (byte) ((Dependencies.Properties.Settings.Default.RecentFilesIndex + 1) % Dependencies.Properties.Settings.Default.RecentFiles.Count);
        }
    }
}