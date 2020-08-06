using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Data.SqlServerCe;

namespace SqlServerCompactEditionTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var users = db.T_SYS_USER.ToList();
                    users.ForEach(o => Console.WriteLine($"{o.Id}，{o.Name}，{string.Join("，", o.T_SYS_ROLE.Select(x => x.Name))}"));

                    var roles = db.T_SYS_ROLE.ToList();
                    roles.ForEach(o => Console.WriteLine($"{o.Id}，{o.Name}，{string.Join("，", o.T_SYS_USER.Select(x => x.Name))}"));
                }
                var dt = new DataTable();
                using (var conn = new SqlCeConnection(string.Format("Data Source = {0}", Path.Combine(AppContext.BaseDirectory, @"App_Data\MyDb.sdf"))))
                {
                    conn.Open();
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT * FROM T_SYS_USER";

                        using (var adapter = new SqlCeDataAdapter(comm))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"异常信息：{e.Message}");
            }

            Console.WriteLine("运行结束...");

            Console.ReadKey();
        }
    }
}