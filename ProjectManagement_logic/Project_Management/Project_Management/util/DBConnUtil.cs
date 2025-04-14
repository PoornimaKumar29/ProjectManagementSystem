using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.IO;

namespace Project_Management.util
{
    public static class DBConnUtil
    {
        //public static SqlConnection GetConnection(string filename)
        //{
        //    string connectionString = DBPropertyUtil.GetConnectionString(filename);
        //    return new SqlConnection(connectionString);
        //}
        //---without external connectio.txt file
        public static SqlConnection GetConnection()
        {
            string connectionString = "Server=POORNIMA\\SQLSERVER2022;Database=project_management;Trusted_Connection=True;TrustServerCertificate=True;";
            return new SqlConnection(connectionString);
        }
    }
}
