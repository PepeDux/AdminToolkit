using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminToolkit;
using System.Windows.Documents;
using System.Windows;

namespace AdminToolkit
{
    internal class UpdateClass
    {
        Random random = new Random();

        public string queryString; //Переменная отвечает за запросы SQL

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        DBConnect dBConnect = new DBConnect();


        public void Update(string login)
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
                    foreach (Product product in DataBank.products)
                    {
                        string SQLActive = product.productName + "Active";
                        string SQLStart = product.productName + "Start";
                        string SQLFinish = product.productName + "Finish";
                        string SQLSubscribe = product.productName + "Subscribe";

                        queryString = $"UPDATE Users SET " + SQLActive + $"='False'," + SQLStart + $"=null," + SQLFinish + $"=null," + SQLSubscribe + $" = '{product.subscribe}' WHERE Login = '{login}'";

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