using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;

namespace AdminToolkit
{
    internal class ExtendClass
    {
        Random random = new Random();

        public string queryString; //Переменная отвечает за запросы SQL

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        DBConnect dBConnect = new DBConnect();
        

        public void Extend(string login)
        {
            dBConnect.ConnectDB(); //Подключение к БД

            queryString = $"SELECT Login FROM Users WHERE Login = '{login}'";

            using (SqlCommand command = dBConnect.Query(queryString))
            {
                SqlCommand selectCommand = new SqlCommand(queryString, DBConnect.connect);
                adapter.SelectCommand = selectCommand;
                adapter.Fill(table);

                if (table.Rows.Count >= 1)
                {
                    foreach(Product product in DataBank.products)
                    {
                        string SQLFinish = product.productName + "Finish"; //Наименование поля конца активности продукта в БД
                        string SQLSubscribe = product.productName + "Subscribe"; //Наименование поля подписки продукта в БД

                        queryString = $"UPDATE Users SET " + SQLFinish + $" += '{((DateTimeOffset)new DateTime(1970, 1, 1).AddMonths(product.subscribe)).ToUnixTimeSeconds()}', " + SQLSubscribe + $" += '{product.subscribe}' WHERE Login = '{login}'";

                        using (SqlCommand command2 = dBConnect.Query(queryString))
                        {
                            command2.ExecuteNonQuery();//Передача данных
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Такого логина нет 😪");

                    table = new DataTable();
                }
            }
        }
    }
}
