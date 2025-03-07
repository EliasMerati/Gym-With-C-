﻿using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DataLayer;
using Gym.Utilitys;
using Gym.Windows;
using Microsoft.Win32;
using Telerik.Windows;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Reflection;

namespace Gym
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string V = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OFPaZYasFtsxNoDemsFOXbvf7SIcnyAkFX/4u37NTfx7g+0IqLXw6QIPolr1PvCSZz8Z5wjBNakeCVozGGOiuCOQDy60XNqfbgrOjxgQ5y/u54K4g7R/xuWmpdx5OMAbUbcy3WbhPCbJJYTI5Hg8C/gsbHSnC2EeOCuyA9ImrNyjsUHkLEh9y4WoRw7lRIc1x+dli8jSJxt9C+NYVUIqK7MEeCmmVyFEGN8mNnqZp4vTe98kxAr4dWSmhcQahHGuFBhKQLlVOdlJ/OT+WPX1zS2UmnkTrxun+FWpCC5bLDlwhlslxtyaN9pV3sRLO6KXM88ZkefRrH21DdR+4j79HA7VLTAsebI79t9nMgmXJ5hB1JKcJMUAgWpxT7C7JUGcWCPIG10NuCd9XQ7H4ykQ4Ve6J2LuNo9SbvP6jPwdfQJB6fJBnKg4mtNuLMlQ4pnXDc+wJmqgw25NfHpFmrZYACZOtLEJoPtMWxxwDzZEYYfT";

        public MainWindow()
        {
            InitializeComponent();
            Stimulsoft.Base.StiLicense.Key = V;
            Timer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (Gym_DBEntities db = new Gym_DBEntities())
            {
                var d = db.vw_shahrieh.ToList();

                foreach (var people in d)
                {

                    SmsSender sms = new SmsSender();
                    if (people.ShahriehOUT == DateTime.Now.date())
                    {
                        sms.SendMessage(people.PeopleMobile, "ورزشکار گرامی ، تاریخ یک ماهه ی شهریه ی شما تمام شده است. لطفا جهت تمدید شهریه به باشگاه مراجعه کنید... با تشکر");
                    }


                }
            }
            LblVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            LblDate.Content = DateTime.Now.date();
            LblName.Content = Public.user;
        }
        private void Timer()
        {
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Tick += new EventHandler(timer_Tick);
            tmr.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            tmr.Start();
        }

        private void timer_Tick(object sender, EventArgs e) => Lblhour.Content = DateTime.Now.ToString("HH:mm:ss");
        private void Image_MouseDown(object sender, MouseButtonEventArgs e) => Environment.Exit(0);

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e) => WindowState = WindowState.Minimized;

        private void Image_MouseDown_3(object sender, MouseButtonEventArgs e) => DragMove();

        private void users_click(object sender, Telerik.Windows.RadRoutedEventArgs e) => new WinShowUsers().ShowDialog();

        private void gym_click(object sender, RadRoutedEventArgs e) => new WinAddGym().ShowDialog();

        private void Admin_click(object sender, RadRoutedEventArgs e) => new WinAddLogin().ShowDialog();

        private void pass_click(object sender, RadRoutedEventArgs e) => new WinChangePass().ShowDialog();

        private void login_click(object sender, RadRoutedEventArgs e) => new WinAddLogin().ShowDialog();

        private void Window_Activated(object sender, EventArgs e)
        {
            using (Gym_DBEntities db = new Gym_DBEntities())
            {
                var d = db.vw_shahrieh.ToList();

                foreach (var people in d)
                {
                    SmsSender sms = new SmsSender();
                    if (people.ShahriehOUT == DateTime.Now.date())
                    {
                        sms.SendMessage(people.PeopleMobile, "ورزشکار گرامی ، تاریخ یک ماهه ی شهریه ی شما به اتمام رسیده است. لطفا جهت شارژ مجدد به باشگاه مراجعه کنید... با تشکر");
                    }
                }
            }
        }

        private void sms_click(object sender, RadRoutedEventArgs e) => new WinSms().ShowDialog();

        private void Button_Click(object sender, RoutedEventArgs e) => new WinExcel().ShowDialog();

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            Backup_Restore b = new Backup_Restore();
            b.BackUpMyDB();
        }

        private void btnRestor_Click(object sender, RoutedEventArgs e)
        {
            Backup_Restore b = new Backup_Restore();
            b.ReStorMyDB();
        }

        private void Ma_click(object sender, RadRoutedEventArgs e) => new Us().Show();

        private void Calc_Click(object sender, RoutedEventArgs e) => System.Diagnostics.Process.Start("calc.exe");

        private void Webcam_Click(object sender, RoutedEventArgs e)
        {
            new WinWebCam().ShowDialog();
        }
    }
}
