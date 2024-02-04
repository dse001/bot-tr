using Microsoft.EntityFrameworkCore;
using bot_tr.model;
namespace bot_tr.handlers
{
    internal class DbUserContext : DbContext
    {
        public DbSet<UserData> UserDatas => Set<UserData>();
        public DbUserContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=bottr_main.db");
        }
    }

}
