using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulLabel
{
    class RequestLog
    {
        public void Log(string sessionID, string desc, string createdtime, string GTIN, string APICall, string Barcode)
        {
            try {
                string connectionString = ConfigurationManager.AppSettings["str"];
                string insertCommandText = "INSERT INTO [tbl_requestLogs] (sessionID,eventDesc,createdTime,GTIN,APICall,Barcode) VALUES (@Value1,@Value2,@Value3,@Value4,@Value5,@Value6)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Insert new record
                    using (SqlCommand insertCommand = new SqlCommand(insertCommandText, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Value1", sessionID);
                        insertCommand.Parameters.AddWithValue("@Value2", desc);
                        insertCommand.Parameters.AddWithValue("@Value3", createdtime);
                        insertCommand.Parameters.AddWithValue("@Value4", GTIN);
                        insertCommand.Parameters.AddWithValue("@Value5", APICall);
                        insertCommand.Parameters.AddWithValue("@Value6", Barcode);
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {

            }
          

         
        }
    }
}
