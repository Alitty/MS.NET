using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OracleDotNetCoreTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new MyDbContext())
            {
                var result = db.Preasigns.Where(o => o.PreExamDate == DateTime.Today.AddMonths(-1)).ToList();
                Console.WriteLine(result.Count);
                result.ForEach(entity => Console.WriteLine($"考生：{entity.Name}（{entity.IdCardNumber}）"));
            }

            Console.ReadKey();
        }
    }

    public class MyDbContext : DbContext
    {
        public DbSet<Preasign> Preasigns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseOracle(connectionString: "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.9.90)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = ORCL)));USER ID=DRV_EXAM_2020;PASSWORD=Jaya_drvexam_2020;");
    }

    [Table("T_TEST_PREASIGN")]
    public class Preasign
    {
        /// <summary>
        /// 序号【主键唯一标识，关联考生分组表序号，保证每天同一科目每个考生只能生成一个编号，重复下载自动覆盖】
        /// </summary>
        [Column("XH")]
        public string Id { get; set; }

        /// <summary>
        /// 管理部门【关联管理部门表序号】
        /// </summary>
        [Column("GLBM")]
        public string OrgId { get; set; }

        /// <summary>
        /// 考场序号【关联考场备案表序号】
        /// </summary>
        [Column("KCXH")]
        public string ExamRoomId { get; set; }

        /// <summary>
        /// 考试线路【关联考试线路表序号】
        /// </summary>
        [Column("KSXL")]
        public string ExamLineId { get; set; }

        /// <summary>
        /// 考试场次
        /// </summary>
        [Column("KSCC")]
        public string ExamSessionId { get; set; }

        /// <summary>
        /// 考试车型
        /// </summary>
        [Column("KSCX")]
        public string ExamCarType { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        [Column("LSH")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// 身份证明号码
        /// </summary>
        [Column("SFZMHM")]
        public string IdCardNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column("XM")]
        public string Name { get; set; }

        /// <summary>
        /// 性别【1=男，2=女】
        /// </summary>
        [Column("XB")]
        public string Sex { get; set; }

        /// <summary>
        /// 考试原因【A=初次申领，B=增驾申请，C=复考，F=满分学习，默认值=A】
        /// </summary>
        [Column("KSYY")]
        public string ExamCause { get; set; }

        /// <summary>
        /// 考试科目【1=科目一，2=科目二，3=科目三，4=科目三安全文明驾驶】
        /// </summary>
        [Column("KSKM")]
        public string ExamSubject { get; set; }

        /// <summary>
        /// 学习时间【单位：分钟（min），默认值=0】
        /// </summary>
        [Column("XXSJ")]
        public int? StudyDuration { get; set; }

        /// <summary>
        /// 约考日期
        /// </summary>
        [Column("YKRQ")]
        public DateTime? PreExamDate { get; set; }

        /// <summary>
        /// 考试员1
        /// </summary>
        [Column("KSY1")]
        public string Examiner { get; set; }

        /// <summary>
        /// 考试日期
        /// </summary>
        [Column("KSRQ")]
        public DateTime? ExamDate { get; set; }

        /// <summary>
        /// 状态【0=停用，1=正常，默认值=0】
        /// </summary>
        [Column("ZT")]
        public string Status { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        [Column("JYW")]
        public string Signed { get; set; }
    }
}