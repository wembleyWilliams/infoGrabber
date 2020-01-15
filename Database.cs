using System;
using System.Data.SqlClient;

namespace infoGrabber
{
    public class Database
    {
        SqlConnection sqlConnection;
        public Database()
        {
            try
            {
                sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
                sqlConnection.Open();
            }
            catch(SqlException e)
            {
                Console.WriteLine("Sql error" + e.Message);
            }

                
        }

        public void insertPC(PCData data)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO PC(pcSerial,pcVendor,pcModel,pcWindowsVersion,pcName,domain,assetName) " +
                    "VALUES('" + data.serialNumberPC + "','" + data.vendorPC + "','" + data.modelPC + "','" + data.version + "','" + data.systemName + "','" + data.domain + 
                    "','" + data.assetName + "')"
                    , sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Monitor(monitorModel, monitorSerial, monitorVendor) " +
                    "VALUES('" + "Generic Model" + "','" + "Generic Serial" + "','" + "Generic Vendor" + "')"
                    , sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Sql error from insertPC method" + e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public void insertMonitor(MonitorData data)
        {
            try
            {
                sqlConnection.Open();
                
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Monitor(monitorModel, monitorSerial, monitorVendor) " +
                    "VALUES('" + data.modelM + "','" + data.serialNumberM + "','" + data.vendorM + "')"
                    , sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Monitor(monitorModel, monitorSerial, monitorVendor) " +
                    "VALUES('" + "Generic Model" + "','" + "Generic Serial" + "','" + "Generic Vendor" + "')"
                    , sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Sql error from insertMonitor method" + e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool verifyPC(string serial)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM PC WHERE pcSerial ='" + serial + "'", sqlConnection);
                if (sqlCommand.ExecuteReader().HasRows)
                    return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Sql error" + e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return false;
        }

        public bool verifyMonitor(string serial)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Monitor WHERE monitorSerial ='" + serial + "'", sqlConnection);
                if (sqlCommand.ExecuteReader().HasRows)
                    return true;

                
            }
            catch (SqlException e)
            {
                Console.WriteLine("Sql error" + e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return false;
        }
  
    }
}
