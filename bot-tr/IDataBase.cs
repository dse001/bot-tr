using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot_tr
{
    internal interface IDataBase
    {
        Task AddToDB(string username, long id, string acountName);
        Task<string?> CheckWithDB(long id);
    } 
}
