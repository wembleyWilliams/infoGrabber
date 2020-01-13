using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace infoGrabber
{
    public class Database
    {
        SqlConnection sqlConnection;
        public Database()
        {
              sqlConnection = new SqlConnection(@"Data Source=(local)\IOPAWSEYWEMBLEY;Initial Catalog=itelAssets;Integrated Security=True;");
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from PC", sqlConnection);
                
        }

        public void insertPC()
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO PC(pcSerial,pcVendor,pcModel,pcWindowsVersion,pcName) " + 
                "VALUES()", sqlConnection);
        }

        public void insertMonitor()
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Monitor", sqlConnection);
        }
    }
}
