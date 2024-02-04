using bot_tr.interfaces;
using bot_tr.model;


namespace bot_tr.hendlers
{

    public class DbHendler : IDataBase

    {
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
    }
}

