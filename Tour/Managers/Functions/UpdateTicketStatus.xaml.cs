﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Tour.Managers.Functions
{
    public partial class UpdateTicketStatus : Window
    {
        public UpdateTicketStatus()
        {
            InitializeComponent();

            try
            {
                Status.Items.Add("Подано");
                Status.Items.Add("В процессе обработки");
                Status.Items.Add("Обработано");
                Status.Items.Add("Завершено");

                DB db = new DB();

                SqlCommand command = new SqlCommand("SELECT id FROM ticket", db.GetConnection());

                db.OpenConnection();

                Ticket.Items.Clear();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Ticket.Items.Add(reader[0].ToString());
                }
                reader.Close();

                db.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void UpdateTicketStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();

                SqlCommand command = new SqlCommand("UPDATE ticket SET statuss = '" + Status.SelectedItem + "' WHERE id = " + Ticket.SelectedItem, db.GetConnection());

                db.OpenConnection();

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Статус был изменен!");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Во время операции произошла ошибка!");
                    return;
                }

                db.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void ClosingButton(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
