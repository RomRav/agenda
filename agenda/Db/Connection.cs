using Microsoft.Data.SqlClient;

namespace agenda.Db
{
        public class Connexion
        {
            public Connexion()
            {
            }
            public static SqlConnection open()
            {
                SqlConnection conn = new SqlConnection("server=localhost;database=agenda; Trusted_Connection = True");
                
                try
                {
                    Console.WriteLine("Connection to MySQL...");
                    conn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return conn;
            }

            public static void close(SqlConnection conn)
            {
                conn.Close();
            }
        }
  
}
