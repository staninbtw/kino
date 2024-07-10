using Kinoteatr.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kinoteatr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinoteatrContext db = new KinoteatrContext();
        public MainWindow()
        {
            InitializeComponent();
            loadmesta();
        }
        void loadmesta()
        {
            var mesta = db.Mesta.Where(d => d.Typeid == 2).ToList();
            list_mesta.ItemsSource = mesta;
            var vipmesta = db.Mesta.Where(d => d.Typeid == 1).ToList();
            list_vipmesta.ItemsSource = vipmesta;
        }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var mesta = db.Mesta.Where(d => d.Typeid == 2 && d.Number % 2 != 0).ToList();
            list_mesta.ItemsSource = mesta;
            var vipmesta = db.Mesta.Where(d => d.Typeid == 1 && d.Number % 2 != 0).ToList();
            list_vipmesta.ItemsSource = vipmesta;
        }

        private void RadioButton2_Click(object sender, RoutedEventArgs e)
        {
            loadmesta();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (list_vipmesta.SelectedItem != null)
            {
                kupitvip();
            }
            else
            {
                kupitdefault();
            }
        }
        void vernutdefault()
        {
            int id = list_mesta.SelectedIndex + 15;
            var zabron = db.Mesta.FirstOrDefault(a => a.Id == id);
            if (zabron != null)
            {
                var typemest = db.Typemests.FirstOrDefault(a => a.Price == 2500);
                if (typemest != null)
                {
                    var usemesta = db.Zabronmesta.FirstOrDefault(a => a.Mestaid == id);
                    if (usemesta != null)
                    {
                        int add = Convert.ToInt32(typemest.Price) - Convert.ToInt32(typemest.Price * 0.1);
                        int adsa = add + Convert.ToInt32(balance_tb.Text);
                        MessageBox.Show($"Вам возваращено: {add}");
                        balance_tb.Text = adsa.ToString();
                        db.Zabronmesta.Remove(usemesta);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Брони нет");
                    }
                }
            }
        }
        void vernutvip()
        {
            int id = list_vipmesta.SelectedIndex + 3;
            var zabron = db.Mesta.FirstOrDefault(a => a.Id == id);
            if (zabron != null)
            {
                var typemest = db.Typemests.FirstOrDefault(a => a.Price == 5000);
                if (typemest != null)
                {
                    var usemesta = db.Zabronmesta.FirstOrDefault(a => a.Mestaid == id);
                    if (usemesta != null)
                    {
                        int add = Convert.ToInt32(typemest.Price) - Convert.ToInt32(typemest.Price * 0.1);
                        int adsa = add + Convert.ToInt32(balance_tb.Text);
                        MessageBox.Show($"Вам возваращено: {add}");
                        balance_tb.Text = adsa.ToString();
                        db.Zabronmesta.Remove(usemesta);
                        db.SaveChanges();
                    }
                    else
                    {

                        MessageBox.Show("Брони нет");
                    }
                }
            }
        }
        void kupitvip()
        {
            int id = list_vipmesta.SelectedIndex + 3;
            var zabron = db.Mesta.FirstOrDefault(a => a.Id == id);
            if (zabron != null)
            {
                var typemest = db.Typemests.FirstOrDefault(a => a.Price == 5000);
                if (typemest != null)
                {
                    Zabronmestum zabronmestum = new Zabronmestum() { Mestaid = id };
                    var usemesta = db.Zabronmesta.FirstOrDefault(a => a.Mestaid == id);
                    if (usemesta != null)
                    {
                        MessageBox.Show("Место уже забронированно!!!");
                    }
                    else
                    {
                        int add = Convert.ToInt32(balance_tb.Text) - Convert.ToInt32(typemest.Price);
                        if (add >= 0)
                        {
                            MessageBox.Show("Место успешно забронировано за вами!");
                            balance_tb.Text = add.ToString();
                            db.Zabronmesta.Add(zabronmestum);
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Недостаточно средств!!!");
                        }

                    }
                }
            }
        }
        void kupitdefault()
        {
            int id = list_mesta.SelectedIndex + 15;
            var zabron = db.Mesta.FirstOrDefault(a => a.Id == id);
            if (zabron != null)
            {
                var typemest = db.Typemests.FirstOrDefault(a => a.Price == 2500);
                if (typemest != null)
                {
                    Zabronmestum zabronmestum = new Zabronmestum() { Mestaid = id };
                    var usemesta = db.Zabronmesta.FirstOrDefault(a => a.Mestaid == id);
                    if (usemesta != null)
                    {
                        MessageBox.Show("Место уже забронированно!!!");
                    }
                    else
                    {
                        int add = Convert.ToInt32(balance_tb.Text) - Convert.ToInt32(typemest.Price);
                        if (add >= 0)
                        {
                            MessageBox.Show("Место успешно забронировано за вами!");
                            balance_tb.Text = add.ToString();
                            db.Zabronmesta.Add(zabronmestum);
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Недостаточно средств!!!");
                        }

                    }
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (list_vipmesta.SelectedItem != null)
            {
                addbron();
            }
            else
            {
                int id = list_mesta.SelectedIndex + 15;
                var zabron = db.Mesta.FirstOrDefault(a => a.Id == id);
                if (zabron != null)
                {
                    Zabronmestum zabronmestum = new Zabronmestum() { Mestaid = id };
                    var usemesta = db.Zabronmesta.FirstOrDefault(a => a.Mestaid == id);
                    if (usemesta != null)
                    {
                        MessageBox.Show("Место уже забронированно!!!");
                    }
                    else
                    {
                        MessageBox.Show("Место успешно забронировано за вами!");
                        db.Zabronmesta.Add(zabronmestum);
                        db.SaveChanges();
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (list_vipmesta.SelectedItem != null)
            {
                vernutvip();
            }
            else 
            {
                vernutdefault();
            }
        }
        void addbron()
        {
            int id = list_vipmesta.SelectedIndex + 3;
            var zabron = db.Mesta.FirstOrDefault(a => a.Id == id);
            if (zabron != null)
            {
                Zabronmestum zabronmestum = new Zabronmestum() { Mestaid = id };
                var usemesta = db.Zabronmesta.FirstOrDefault(a => a.Mestaid == id);
                if (usemesta != null)
                {
                    MessageBox.Show("Место уже забронированно!!!");
                }
                else
                {
                    MessageBox.Show("Место успешно забронировано за вами!");
                    db.Zabronmesta.Add(zabronmestum);
                    db.SaveChanges();
                }
            }
        }
        void snyatbron()
        {
            int id = list_vipmesta.SelectedIndex + 3;
            var zabron = db.Mesta.FirstOrDefault(a => a.Id == id);
            if (zabron != null)
            {
                var usemesta = db.Zabronmesta.FirstOrDefault(a => a.Mestaid == id);
                if (usemesta != null)
                {
                    MessageBox.Show("Бронь снята");
                    db.Zabronmesta.Remove(usemesta);
                    db.SaveChanges();

                }
                else
                {

                    MessageBox.Show("Брони нет");
                }
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (list_vipmesta.SelectedItem != null)
            {
                snyatbron();
            }
            else
            {
                int id = list_mesta.SelectedIndex + 15;
                var zabron = db.Mesta.FirstOrDefault(a => a.Id == id);
                if (zabron != null)
                {
                    var usemesta = db.Zabronmesta.FirstOrDefault(a => a.Mestaid == id);
                    if (usemesta != null)
                    {
                        MessageBox.Show("Бронь снята");
                        db.Zabronmesta.Remove(usemesta);
                        db.SaveChanges();

                    }
                    else
                    {
                        MessageBox.Show("Брони нет");
                    }
                }
            }
       
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            list_mesta.SelectedItem = null;
            list_vipmesta.SelectedItem = null;
        }
    }   
}
