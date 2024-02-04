using bot_tr.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot_tr.interfaces
{
    public interface IDataBase
    {
        Task AddToDB(UserData userData);
        Task<string?> CheckWithDB(long id);
        Task<string?> RemoveFromDBbyID(long id);
        Task<string?> GetAllData();
        Task DelloutDataForReuse();
    }
}
