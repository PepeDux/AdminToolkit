using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdminToolkit
{
    internal class DeleteClass
    {
        public string queryString; //Переменная отвечает за запросы SQL

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        DBConnect dBConnect = new DBConnect();



        public void Delete(string login)
        {
            dBConnect.ConnectDB(); //Подключение к БД

            if(login != null && login != "")
            {
                if (MessageBox.Show("ТЫ В НАТУРИ ХОЧЕШЬ УДАЛИТЬ ЭТОГО МУДИЛУ ИЗ БД!?", "УДАЛИТЬ?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    using (SqlCommand command = dBConnect.Query($"DELETE FROM Users WHERE Login = '{login}'"))
                    {
                        command.ExecuteNonQuery();//Передача данных
                    }

                    MessageBox.Show("ТУДА ЕГО, ТЫ ДОВОЛЕН!?");
                }
            }
            else
            {
                MessageBox.Show("ПОЛЕ ПУСТОЕ!!!");
            }
        }
    }
}
