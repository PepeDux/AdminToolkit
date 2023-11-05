using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminToolkit
{
    public class DBConnect
    {
        public static SqlConnection connect;

        public void ConnectDB()
        {
            //Подключение к БД, для изменения адресса подключения поменяй строку ниже
            connect = new SqlConnection("Строка подключения к БД");
            connect.Open();

        }

        public SqlCommand Query(string sqlQuery)
        {
            SqlCommand command = new SqlCommand(sqlQuery, connect);
            return command;
        }

    }
}
