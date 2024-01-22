using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace bot_tr
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
