using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DERSLER
{
    class DataAccess
    {
        public SqlConnection baglanti() {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-7GNS0OU\SQLEXPRESS;Initial Catalog=dboTicariOtomasyon;
            Integrated Security=True");
            connection.Open();
            return connection;
        }



    }
}
