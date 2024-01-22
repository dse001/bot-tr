using bot_tr;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace bot_tr
{

    internal class DbHendler : IDataBase
    //var UserData rezult;
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

        public async Task<string?> CheckWithDB( long id)
        {
            DbUserContext db = new DbUserContext();
            {

                //var rezult = db.UserDatas.ToList().Where(x => x.UserId == id).FirstOrDefault<UserData>();
               // string? checkNotNull = db.UserDatas.ToList().Where(x => x.UserId == id).FirstOrDefault<UserData>().UserName.ToString();
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
//using (ApplicationContext db = new ApplicationContext())
//{
//    // создаем два объекта User
//    User tom = new User { Name = "Tom", Age = 33 };
//    User alice = new User { Name = "Alice", Age = 26 };

//    // добавляем их в бд
//    db.Users.Add(tom);
//    db.Users.Add(alice);
//    db.SaveChanges();
//    Console.WriteLine("Объекты успешно сохранены");
 
//    // получаем объекты из бд и выводим на консоль
//    var users = db.Users.ToList();
//    Console.WriteLine("Список объектов:");
//    foreach (User u in users)
//    {
//        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
//    }
}    

}

