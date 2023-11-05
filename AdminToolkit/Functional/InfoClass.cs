using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Windows;

namespace AdminToolkit
{
    internal class InfoClass
    {
        Random random = new Random();

        public string queryString; //Переменная отвечает за запросы SQL

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        DBConnect dBConnect = new DBConnect();

        //$"ISNULL(CuttingActive, 'False') AS CuttingActive, ISNULL(CuttingStart, 0) AS CuttingStart, ISNULL(CuttingFinish, 0) AS CuttingFinish, ISNULL(CuttingSubscribe, 0) AS CuttingSubscribe, " +
        //$"";

        public string Info(string login)
        {
            dBConnect.ConnectDB();

            string info = null;

            //string infoCuttingActive = " ";
            //long infoCuttingStart = 0;
            //long infoCuttingFinish = 0;
            //string infoCuttingSubscribe = " ";

            string queryString = $"SELECT ID, Login, Password, Source, ISNULL(PastIP, 'Не активен') AS PastIP, ISNULL(CurrentIP, 'Не активен') AS CurrentIP, RegistrationDate FROM Users WHERE Login = '{login}'"; 


            using (SqlCommand command = dBConnect.Query(queryString))
            {
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) //Если есть данные
                {
                    while (reader.Read())
                    {
                        info += "ID: " + reader["ID"].ToString() + "\r\n" +
                                "Логин: " + reader["Login"].ToString() + "\r\n" +
                                "Пароль: " + reader["Password"].ToString() + "\r\n" +
                                "Ключ: " + reader["Source"].ToString() + "\r\n" +
                                "Последний IP: " + reader["PastIP"].ToString() + "\r\n" +
                                "Текущий IP: " + reader["CurrentIP"].ToString() + "\r\n" +
                                "Дата регистрации: " + reader["RegistrationDate"].ToString() + "\r\n" +
                                " " + "\r\n"; 
                    }
                }

                reader.Close();                  
            }



            foreach (Product product in DataBank.products)
            {
                string SQLActive = product.productName + "Active";
                string SQLStart = product.productName + "Start";
                string SQLFinish = product.productName + "Finish";
                string SQLSubscribe = product.productName + "Subscribe";

                queryString = $"SELECT ID, " + SQLActive + $"," + SQLStart + $"," + SQLFinish + $"," + SQLSubscribe + $" FROM Users WHERE Login = '{login}'";

                using (SqlCommand command = dBConnect.Query(queryString))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) //Если есть данные
                    {
                        while (reader.Read())
                        {
                            if(Convert.ToInt16(reader[SQLSubscribe]) > 0)
                            {
                                info += $"Активность продукта {product.productName} : " + reader[SQLActive].ToString() + "\r\n" +
                                        $"Дата начала подписки на {product.productName} : " + reader[SQLStart].ToString() + "\r\n" +
                                        $"Дата окончания подписки на {product.productName} : " + reader[SQLFinish].ToString() + "\r\n" +
                                        $"Подписка на {product.productName} на срок: " + reader[SQLSubscribe].ToString() + "\r\n" +
                                        " " + "\r\n";
                            }
                        }
                    }

                    reader.Close();
                }

            }

            return info;
        }
    }
}
