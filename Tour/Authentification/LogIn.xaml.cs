﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Tour.Classes;

namespace Tour.Authentification
{
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                DB db = new DB();

                DataTable table = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM Users" +
                    " WHERE number = '" + Number.Text + "' AND passwrd = '" 
                    + Md5.HashPassword(Pass.Password) + "'", 
                    db.GetConnection());

                SqlCommand cmd = new SqlCommand("SELECT * FROM Managers" +
                    " WHERE loginn = '" + Number.Text + "' AND passwrd = '"
                    + Md5.HashPassword(Pass.Password) + "'",
                    db.GetConnection());

                DataTable tbl = new DataTable();

                SqlDataAdapter adpt = new SqlDataAdapter();

                adpt.SelectCommand = command;
                adpt.Fill(table);

                adapter.SelectCommand = cmd;
                adapter.Fill(tbl);

                db.OpenConnection();

                if (Number.Text.Length > 0)
                {
                    if (Pass.Password.Length > 0)
                    {
                        if (table.Rows.Count > 0 || tbl.Rows.Count > 0)
                        {
                            if (Number.Text == "admin" && Pass.Password == "admin")
                            {
                                this.Hide();
                                Admin.Admin admin = new Admin.Admin();
                                admin.Show();
                            }
                            else if (Number.Text.Contains("mng") && 
                                Pass.Password.Contains("mng"))
                            {
                                Manager manager = new Manager
                                {
                                    Id = Convert.ToInt32(tbl.Rows[0]["id"]),
                                    Fio = tbl.Rows[0]["fio"].ToString(),
                                    Number = tbl.Rows[0]["number"].ToString(),
                                    Email = tbl.Rows[0]["email"].ToString(),
                                    Login = tbl.Rows[0]["loginn"].ToString(),
                                    Password = tbl.Rows[0]["passwrd"].ToString()
                                };

                                this.Hide();
                                Managers.ManagerProfile mng = new Managers.ManagerProfile(manager);
                                mng.Show();
                            }
                            else
                            {
                                Classes.User user = new Classes.User
                                {
                                    Id = Convert.ToInt32(table.Rows[0]["id"]),
                                    Namme = table.Rows[0]["namme"].ToString(),
                                    Surname = table.Rows[0]["surname"].ToString(),
                                    Otch = table.Rows[0]["otch"].ToString(),
                                    Passport = table.Rows[0]["passport"].ToString(),
                                    Visa = table.Rows[0]["visa"].ToString(),
                                    Email = table.Rows[0]["email"].ToString(),
                                    Number = table.Rows[0]["number"].ToString()
                                };

                                this.Hide();
                                User.UserProfile profile = new User.UserProfile(user);
                                profile.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("Введите логин!");
                }

                db.CloseConnection();
            } 
            catch 
            { 
                MessageBox.Show("Ошибка!"); 
            }            
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SignUp up = new SignUp();
            up.Show();
        }

        private void Number_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Number.Clear();
        }

        private void ClosingButton(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
