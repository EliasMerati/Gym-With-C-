﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DataLayer;
using System.Transactions;

namespace Gym.Windows
{
    /// <summary>
    /// Interaction logic for WinAddPeyment.xaml
    /// </summary>
    public partial class WinAddPeyment : Window
    {
        public WinAddPeyment()
        {
            InitializeComponent();
            Timer();
        }

        public int id { get; set; }
        Gym_DBEntities db = new Gym_DBEntities();
        public int cash { get; set; }
        private void timer_Tick(object sender, EventArgs e)//// دستورات ساعت
        {
            LblDate.Content = DateTime.Now.date() + " _ " + DateTime.Now.ToString("HH:mm:ss");
        }
        private void Timer()
        {
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Tick += new EventHandler(timer_Tick);
            tmr.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            tmr.Start();
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LblDate.Content = DateTime.Now.date() + " _ " + DateTime.Now.ToString("HH:mm:ss");
            Cmbtype.SelectedIndex = 0;
            Cmbtype2.SelectedIndex = 0;
        }

        private void Btncash_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    var f = db.People.Where(l => l.PeopleID == id).ToList();
                    if (f[0].PeopleDeptor > 0 && Cmbtype2.Text == "درج در بدهی ها")
                    {
                        db.updatedeptor2(id, int.Parse(TxtPey.Text.Trim()));
                        db.SaveChanges();
                    }
                    else if (f[0].PeopleDeptor == 0 && Cmbtype2.Text == "درج در بدهی ها")
                    {
                        db.updatedeptor1(id, int.Parse(TxtPey.Text.Trim()));
                        db.SaveChanges();
                    }
                    else if (f[0].PeopleCreditor < 0 && Cmbtype2.Text == "درج در بدهی ها")
                    {
                        db.updatedeptor3(id, int.Parse(TxtPey.Text.Trim()));
                        db.SaveChanges();
                    }
                    else if (f[0].PeopleDeptor > 0 && Cmbtype2.Text == "نقدی" && f[0].PeopleCreditor == 0)
                    {
                        db.updatedeptor4(id, int.Parse(TxtPey.Text.Trim()));
                        db.SaveChanges();
                    }
                    else if (Cmbtype2.Text == "نقدی" && f[0].PeopleCreditor > 0)
                    {
                        db.Updatedeptor5(id, int.Parse(TxtPey.Text.Trim()));
                        db.SaveChanges();
                    }
                    else if (Cmbtype2.Text == "تسویه حساب")
                    {
                        db.Updatedeptor6(id);
                        db.SaveChanges();
                    }
                    
                    db.InsertPay(id, int.Parse(TxtPey.Text.Trim()), DateTime.Now.date() + " _ " + DateTime.Now.ToString("HH:mm:ss"), Cmbtype.Text, Cmbtype2.Text);
                    db.SaveChanges();

                    
                    ts.Complete();
                    MessageBox.Show("عملیات با موفقیت انجام شد");

                }
                catch 
                {
                    MessageBox.Show("در ارتباط با پایگاه داده مشکلی بوجود آمده است ، لطفا مجددا تلاش کنید");
                }
            }

        }
    }
}
