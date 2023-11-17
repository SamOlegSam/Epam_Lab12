using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string str = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=D:\Monitor.mdb";
        private OleDbConnection oledbconnection = null;
        private OleDbCommandBuilder oledbbuilder = null;
        private OleDbDataAdapter oledbdataadapter = null;
        private DataSet dataset = null;
        public MainWindow()
        {
            InitializeComponent();
            LoadData();

        }
                

        private void LoadData()
        {
            oledbconnection = new OleDbConnection(str);
            oledbconnection.Open();
            try
            {
                oledbdataadapter = new OleDbDataAdapter("SELECT * FROM Display ORDER BY Модель", oledbconnection);
                oledbbuilder = new OleDbCommandBuilder(oledbdataadapter);
                oledbbuilder.GetInsertCommand();
                oledbbuilder.GetUpdateCommand();
                oledbbuilder.GetDeleteCommand();

                dataset = new DataSet();
                oledbdataadapter.Fill(dataset, "Monitor");

                DataGrid1.ItemsSource = dataset.Tables["Monitor"].DefaultView;
                oledbconnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //--------------Добавления ново записи в базу данных----------------------------------
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text == "")
            {
                TextBox1.Background = Brushes.Red;
            }
            if (TextBox2.Text == "")
            {
                TextBox2.Background = Brushes.Red;
            }
            if (TextBox3.Text == "")
            {
                TextBox3.Background = Brushes.Red;
            }
            if (TextBox4.Text == "")
            {
                TextBox4.Background = Brushes.Red;
            }
            else
            {
                oledbconnection = new OleDbConnection(str);
                oledbconnection.Open();
                OleDbCommand command = new OleDbCommand($"INSERT INTO Display (Модель, Диагональ, Страна, Цена) Values (@a1, @a2, @a3, @a4)", oledbconnection);
                command.Parameters.AddWithValue("@a1", TextBox1.Text);
                command.Parameters.AddWithValue("@a2", TextBox2.Text);
                command.Parameters.AddWithValue("@a3", TextBox3.Text);
                command.Parameters.AddWithValue("@a4", Convert.ToDouble(TextBox4.Text));
                command.ExecuteNonQuery();
                oledbconnection.Close();
                MessageBox.Show("Готово!");

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";

                TextBox1.Text = "";
                TextBox2.Visibility = Visibility.Hidden;
                TextBox3.Visibility = Visibility.Hidden;
                TextBox4.Visibility = Visibility.Hidden;

                Label1.Visibility = Visibility.Hidden;
                Label2.Visibility = Visibility.Hidden;
                Label3.Visibility = Visibility.Hidden;
                Label4.Visibility = Visibility.Hidden;
                Label5.Visibility = Visibility.Hidden;

                Button1.Visibility = Visibility.Hidden;

                LoadData();
            }
        }
        //---------------------Обработка пункта меню ДОБАВИТЬ-----------------------------------------------
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            TextBox5.Visibility = Visibility.Hidden;
            TextBox6.Visibility = Visibility.Hidden;
            TextBox7.Visibility = Visibility.Hidden;
            TextBox8.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;

            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;

            Label1.Content = "Новая запись";
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;

            Button1.Visibility = Visibility.Visible;
        }
        //---------------------Обработка пункта меню РЕДАКТИРОВАТЬ-----------------------------------------------
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            TextBox1.Visibility = Visibility.Hidden;
            TextBox2.Visibility = Visibility.Hidden;
            TextBox3.Visibility = Visibility.Hidden;
            TextBox4.Visibility = Visibility.Hidden;
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;

            TextBox5.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            TextBox7.Visibility = Visibility.Visible;
            TextBox8.Visibility = Visibility.Visible;

            Label1.Content = "Редактировать запись";
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;
            //Label6.Visibility = Visibility.Visible;

            Button3.Visibility = Visibility.Visible;
        }

        //---------------------Обработка пункта меню УДАЛИТЬ-----------------------------------------------
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            TextBox1.Visibility = Visibility.Hidden;
            TextBox2.Visibility = Visibility.Hidden;
            TextBox3.Visibility = Visibility.Hidden;
            TextBox4.Visibility = Visibility.Hidden;
            Button1.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;

            TextBox5.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            TextBox7.Visibility = Visibility.Visible;
            TextBox8.Visibility = Visibility.Visible;

            Label1.Content = "Удалить запись";
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;
            
            Button2.Visibility = Visibility.Visible;
        }

        //---------------------Обработка нажатия кнопки УДАЛИТЬ-----------------------------------------------
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            oledbconnection = new OleDbConnection(str);
            oledbconnection.Open();
            OleDbCommand command = new OleDbCommand($"DELETE FROM Display WHERE Код = @a5", oledbconnection);
            
            command.Parameters.AddWithValue("@a5", Convert.ToInt32(TextBox10.Text));
            command.ExecuteNonQuery();
            oledbconnection.Close();
            MessageBox.Show("Готово!");

            TextBox5.Visibility = Visibility.Hidden;
            TextBox6.Visibility = Visibility.Hidden;
            TextBox7.Visibility = Visibility.Hidden;
            TextBox8.Visibility = Visibility.Hidden;

            Label1.Content = "";
            Label2.Visibility = Visibility.Hidden;
            Label3.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            Label5.Visibility = Visibility.Hidden;

            Button2.Visibility = Visibility.Hidden;

            LoadData();
        }

        //---------------------Обработка нажатия кнопки РЕДАКТИРОВАТЬ-----------------------------------------------
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            oledbconnection = new OleDbConnection(str);
            oledbconnection.Open();
            OleDbCommand command = new OleDbCommand($"UPDATE Display SET Модель = @a1, Диагональ = @a2, Страна = @a3, Цена = @a4 WHERE Код = @a5", oledbconnection);
            command.Parameters.AddWithValue("@a1", TextBox5.Text);
            command.Parameters.AddWithValue("@a2", TextBox6.Text);
            command.Parameters.AddWithValue("@a3", TextBox7.Text);
            command.Parameters.AddWithValue("@a4", Convert.ToDouble(TextBox8.Text));
            command.Parameters.AddWithValue("@a5", Convert.ToInt32(TextBox10.Text));
            command.ExecuteNonQuery();
            oledbconnection.Close();
            MessageBox.Show("Готово!");
                        
            TextBox5.Visibility = Visibility.Hidden;
            TextBox6.Visibility = Visibility.Hidden;
            TextBox7.Visibility = Visibility.Hidden;
            TextBox8.Visibility = Visibility.Hidden;

            Label1.Content = "";
            Label2.Visibility = Visibility.Hidden;
            Label3.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            Label5.Visibility = Visibility.Hidden;

            Button3.Visibility = Visibility.Hidden;

            LoadData();
        }
    }
}
