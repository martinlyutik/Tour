﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Tour.Authentification;

namespace Tour.Admin.Functions
{
    public partial class CreateManager : Window
    {
        public CreateManager()
        {
            InitializeComponent();
        }

        public Boolean CheckLogin()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT * FROM Users" +
                " WHERE number = '" + Login.Text + "'", db.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.OpenConnection();

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Данный Логин уже занят!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void CreateManagerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex R = new Regex("\\s+");
                Regex E = new Regex("@");
                Match MEe = E.Match(Email.Text);
                Match MS = R.Match(Fio.Text);
                Match ME = R.Match(Email.Text);
                Match MNu = R.Match(Number.Text);
                Match MPs = R.Match(Password.Password);
                Match ML = R.Match(Login.Text);

                for (int i = 0; i < Fio.Text.Length; i++)
                {
                    if (MS.Success)
                    {
                        MessageBox.Show("Поле Ф.И.О. не может быть пустым!");
                        return;
                    }
                }

                for (int i = 0; i < Email.Text.Length; i++)
                {
                    if (ME.Success || !MEe.Success)
                    {
                        MessageBox.Show("Поле Email не может быть пустым и должно содержать @!");
                        return;
                    }
                }

                for (int i = 0; i < Number.Text.Length; i++)
                {
                    if (MNu.Success)
                    {
                        MessageBox.Show("Поле Телефон не может быть пустым!");
                        return;
                    }
                }

                for (int i = 0; i < Password.Password.Length; i++)
                {
                    if (MPs.Success)
                    {
                        MessageBox.Show("Поле Пароль не может быть пустым!");
                        return;
                    }
                }

                for (int i = 0; i < Login.Text.Length; i++)
                {
                    if (ML.Success)
                    {
                        MessageBox.Show("Поле Логин не может быть пустым!");
                        return;
                    }
                }

                if (CheckLogin()) return;

                DB db = new DB();

                SqlCommand command = new SqlCommand("INSERT INTO Managers " +
                    "(fio, number, " +
                    "email, loginn, passwrd) VALUES " +
                    "('" + Fio.Text + "', '" + Number.Text + "', '"
                    + Email.Text + "', '" + Login.Text + "', '"
                    + Classes.Md5.HashPassword(Password.Password) + "')", db.GetConnection());

                db.OpenConnection();

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Менеджер создан успешно!");
                    this.Hide();
                }

                db.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Данные введены неверно! Попробуйте еще раз.");
            }
        }
    }
}
