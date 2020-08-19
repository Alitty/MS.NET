using System;
using ServiceStack.OrmLite;

namespace ServiceStackOrmLite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var conn = new OrmLiteConnectionFactory("Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.9.65)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = ORCL)));USER ID=DRV_EXAM_2018;PASSWORD=Jaya_drvexam_2018;", OracleDialect.Provider).CreateDbConnection())
            {
                conn.Open();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = "SELECT XM FROM T_SYS_USER";
                    using (var reader = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}