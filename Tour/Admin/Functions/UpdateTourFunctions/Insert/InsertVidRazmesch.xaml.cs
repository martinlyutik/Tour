﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tour.Admin.Functions.UpdateTourFunctions.Insert
{
    public partial class InsertVidRazmesch : Window
    {
        public InsertVidRazmesch()
        {
            InitializeComponent();
        }

        private void InsertVidRazm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex R = new Regex("\\s+");
                Match MC = R.Match(VidRazm.Text);
                Regex r = new Regex(@"\d");
                Match MN = r.Match(VidRazm.Text);

                if (VidRazm.Text == "")
                {
                    MessageBox.Show("Поле не может быть пустым!");
                    return;
                }

                for (int i = 0; i < VidRazm.Text.Length; i++)
                {
                    if (MC.Success)
                    {
                        MessageBox.Show("Поле не может содержать пробелы!");
                        return;
                    }

                    if (MN.Success)
                    {
                        MessageBox.Show("Поле не может содержать цифры!");
                        return;
                    }
                }

                DB db = new DB();

                SqlCommand command = new SqlCommand("INSERT INTO Vid (namme) VALUES ('" + VidRazm.Text + "')", db.GetConnection());

                db.OpenConnection();

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Добавление прошло успешно!");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Добавление провалено!");
                    return;
                }

                db.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}
