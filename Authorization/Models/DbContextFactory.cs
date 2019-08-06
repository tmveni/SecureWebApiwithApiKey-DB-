using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Models
{
    public class DbContextFactory
    {
        public static Dictionary<string, string> ConnectionStrings { get; private set; }

        public static void SetConnectionString(Dictionary<string, string> connStrs)
        {
            ConnectionStrings = connStrs;
        }

        public static SampleDBContext CreateCisMainDbContext()
        {
            string connStr = ConnectionStrings["DefaultConnection"];
            //"Server=DESKTOP-7F6CFAD\\SQLEXPRESS;Database=Sample;UID=sa;PWD=sb@1234;";
            var optionBuilder = new DbContextOptionsBuilder<SampleDBContext>();
            optionBuilder.UseSqlServer(connStr);
            var context = new SampleDBContext(optionBuilder.Options);
            return context;
        }
    }
}
