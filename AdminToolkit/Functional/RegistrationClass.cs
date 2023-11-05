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
using System.Net;

namespace AdminToolkit
{
    internal class RegistrationClass
    {
        Random random = new Random();

        static string host = Dns.GetHostName();
        static IPAddress[] address = Dns.GetHostAddresses(host);

        public string queryString; //Переменная отвечает за запросы SQL

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        DBConnect dBConnect = new DBConnect();



        public string Registration(string login, string password, string source)
        {
            dBConnect.ConnectDB(); //Подключение к БД

            if(source == null || source == "")
            {
                source = SourceGenerator();
            }


            if ((login != null && login != "") && (password != null && password != ""))
            {
                queryString = $"SELECT Login FROM Users WHERE Login = '{login}'";

                using (SqlCommand command = dBConnect.Query(queryString))
                {
                    SqlCommand selectCommand = new SqlCommand(queryString, DBConnect.connect);
                    adapter.SelectCommand = selectCommand;
                    adapter.Fill(table);

                    if (table.Rows.Count >= 1)
                    {
                        MessageBox.Show("Такой логин уже есть 😪");

                        table = new DataTable();

                        return null;
                    }
                    else
                    {
                        queryString = $"INSERT INTO Users (Login, RegistrationIP, Password, Source, RegistrationDate) values('{login}', '{address[0]}', '{password}', '{source}', '{DateTime.Now.ToString("yyyy-MM-dd")}')";

                        using (SqlCommand command2 = dBConnect.Query(queryString))
                        {
                            command2.ExecuteNonQuery();//Передача данных
                        }

                        foreach (Product product in DataBank.products)
                        {
                            string SQLActive = product.productName + "Active"; //Наименование поля активности продукта в БД
                            string SQLSubscribe = product.productName + "Subscribe"; //Наименование поля подписки продукта в БД

                            queryString = $"UPDATE Users SET " + SQLActive + $"='False', " + SQLSubscribe + $"='{product.subscribe}' WHERE Login='{login}'";

                            using (SqlCommand command3 = dBConnect.Query(queryString))
                            {
                                command3.ExecuteNonQuery(); //Передача данных
                            }
                        }

                        MessageBox.Show(login + " Успешно зарегистрирован");

                        return "Логин: " + login + "\r\n" + "Пароль: " + password + "\r\n" + "Ключ: " + source + "\r\n";
                    }
                }
            }
            else
            {
                MessageBox.Show("Неверные данные");
                return null;
            }
        }



        public string SourceGenerator()
        {
            string source = null;
            string sourceChar = "ABCDEFGHIKLMNPQRSTUVWXYZ12345678901234567890";


            for (int i = 0; i < 4; i++)
            {
                source += sourceChar[random.Next(0, sourceChar.Length)]; // 1 уровень ID
            }

            source += "-";

            for (int i = 0; i < 4; i++)
            {
                source += sourceChar[random.Next(0, sourceChar.Length)]; // 2 уровень ID
            }

            source += "-";

            for (int i = 0; i < 4; i++)
            {
                source += sourceChar[random.Next(0, sourceChar.Length)]; // 3 уровень ID
            }

            source += "-";

            for (int i = 0; i < 4; i++)
            {
                source += sourceChar[random.Next(0, sourceChar.Length)]; // 4 уровень ID
            }

            return source;
        }
    }
}
