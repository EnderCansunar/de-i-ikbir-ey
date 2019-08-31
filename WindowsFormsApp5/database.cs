using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace WindowsFormsApp5
{
    class Database
    {
        public static string connstr = ConfigurationManager.ConnectionStrings["MySql"].ToString();

        public MySqlConnection baglanti = new MySqlConnection(connstr);

        public string baglanti_kontrol()
        {
            try
            {
                baglanti.Open();
                return "true";


            }
            catch(MySqlException ex)
            {
                return ex.Message;
            }
        }

        
    }
}
