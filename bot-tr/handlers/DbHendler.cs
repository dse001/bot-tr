using bot_tr.interfaces;
using bot_tr.model;

namespace bot_tr.handlers
{
    public class DbHandler : IDataBase

    {
        private readonly IDataBase db;
        public string outData;

        public async Task AddToDB(UserData userData)

        {
            DbUserContext db = new DbUserContext();
            {
                db.UserDatas.Add(userData);
                db.SaveChanges();
            }
        }

        public async Task<string?> CheckWithDB(long id)
        {
            DbUserContext db = new DbUserContext();
            {
                UserData? checkNotNull = db.UserDatas?.Find(id);
                if (checkNotNull != null)
                {
                    return checkNotNull.UserName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public async Task<string?> RemoveFromDBbyID(long id)
        {
            DbUserContext db = new DbUserContext();
            {
                UserData? checkNotNull = db.UserDatas?.Find(id);
                if (checkNotNull != null)
                {
                    db.UserDatas?.Remove(checkNotNull);
                    db.SaveChanges();
                    return checkNotNull.UserName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public async Task<string?> GetAllData()
        {
            DbUserContext db = new DbUserContext();
            {
                var listAllAdta = db.UserDatas?.ToList();
                if (listAllAdta != null)
                {
                    listAllAdta.ForEach(UserData => StringForOut(UserData.UserName, UserData.UserId, UserData.AccountName));
                    return outData;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void StringForOut(string UserName, long UserId, string AccountName)
        {
            string line = $"{UserName} ,{UserId}, {AccountName}" + "\n";
            outData = outData + line;
        }
        public async Task DelloutDataForReuse()
        {
            outData = string.Empty;
        }
    }
}

