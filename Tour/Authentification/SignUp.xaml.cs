﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using Tour.Classes;

namespace Tour.Authentification
{
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        public Boolean CheckLogin()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT * FROM Users" +
                " WHERE number = '" + Number.Text + "'", db.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.OpenConnection();

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Данный телефон уже зарегистрирован!");
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
            Authentification.LogIn log = new Authentification.LogIn();
            log.Show();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                Regex R = new Regex("\\s+");
                Regex E = new Regex("@");
                Match MEe = E.Match(Email.Text);
                Match MS = R.Match(Surname.Text);
                Match MN = R.Match(Name.Text);
                Match MO = R.Match(Otch.Text);
                Match MP = R.Match(Passport.Text);
                Match MV = R.Match(Visa.Text);
                Match ME = R.Match(Email.Text);
                Match MNu = R.Match(Number.Text);
                Match MPs = R.Match(Pass.Password);
                
                if (Surname.Text == "" || Name.Text == "" || Otch.Text == ""
                    || Email.Text == "" || Passport.Text == "" ||
                    Number.Text == "" || Pass.Password == "" ||
                    Visa.Text == "")
                {
                    MessageBox.Show("Поля не могут быть пустыми!");
                    return;
                }

                for (int i = 0; i < Surname.Text.Length; i++)
                {                    
                    if (MS.Success)
                    {
                        MessageBox.Show("Поле Фамилия не может содержать пробелы!");
                        return;
                    }
                }

                for (int i = 0; i < Name.Text.Length; i++)
                {
                    if (MN.Success)
                    {
                        MessageBox.Show("Поле Имя не может содержать пробелы!");
                        return; 
                    }
                }

                for (int i = 0; i < Otch.Text.Length; i++)
                {
                    if (MO.Success)
                    {
                        MessageBox.Show("Поле Отчество не может содержать пробелы!");
                        return;
                    }
                }

                for (int i = 0; i < Passport.Text.Length; i++)
                {
                    if (MP.Success)
                    {
                        MessageBox.Show("Поле Паспортные данные не может содержать пробелы!");
                        return; 
                    }
                }

                for (int i = 0; i < Visa.Text.Length; i++)
                {
                    if (MV.Success)
                    {
                        MessageBox.Show("Поле Виза не может содержать пробелы!");
                        return;
                    }
                }

                for (int i = 0; i < Email.Text.Length; i++)
                {
                    if (ME.Success || !MEe.Success)
                    {
                        MessageBox.Show("Поле Email не может содержать пробелы и должно содержать @!");
                        return;
                    }
                }

                for (int i = 0; i < Number.Text.Length; i++)
                {
                    if (MNu.Success)
                    {
                        MessageBox.Show("Поле Телефон не может содержать пробелы!");
                        return;
                    }
                }

                for (int i = 0; i < Pass.Password.Length; i++)
                {
                    if (MPs.Success)
                    {
                        MessageBox.Show("Поле Пароль не может содержать пробелы!");
                        return;
                    }
                }

                if (CheckLogin()) return;

                DB db = new DB();

                SqlCommand command = new SqlCommand("INSERT INTO Users " +
                    "(namme, surname, " +
                    "otch, passport, visa, email, number, passwrd) VALUES " +
                    "('" + Name.Text + "', '" + Surname.Text + "', '"
                    + Otch.Text + "', '"
                    + Passport.Text + "', '" + Visa.Text + "', '"
                    + Email.Text + "', '" + Number.Text + "', '"
                    + Md5.HashPassword(Pass.Password) + "')", db.GetConnection());

                db.OpenConnection();

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Вы были успешно зарегистрированы!");
                    this.Hide();
                    LogIn log = new LogIn();
                    log.Show();
                }
                else
                {
                    MessageBox.Show("Вы не были зарегистрированы!");
                    return;
                }

                db.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Данные введены неверно! Попробуйте еще раз.");
            }                   
        }

        private void ClosingButton(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
