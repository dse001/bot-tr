﻿using bot_tr.interfaces;
using bot_tr.model;


namespace bot_tr.hendlers
{

    internal class DbHendler : IDataBase

    {
        public async Task AddToDB(string username, long id, string acountName)
        {
            DbUserContext db = new DbUserContext();
            {

                UserData userData = new UserData()
                {
                    UserName = username,
                    UserId = id,
                    AccountName = acountName
                };

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
