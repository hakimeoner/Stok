using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Stok_Takibi
{
    class baglanti
    {
        SqlConnection connect = new SqlConnection("Data Source=HASAR-1;Initial Catalog=Stok_Takip;Integrated Security=True");
    
        public SqlConnection con()
        {
            return connect;
        }

        public void open()
        {
            if (con().State.ToString() == "Closed") {
                con().Open();
            }
        }

        public void close()
        {         
                con().Close();
        }

    }
}
