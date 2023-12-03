using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace bot_tr
{
    internal class FileHandler
    {
        private readonly string dataFilePath;

        public static void LoadUserData(out string userName, out long userId, out string accountName)
        {
            userName = null;
            accountName = null;
            userId = 0;

            if (File.Exists("userdata.json"))
            {
                var jsonData = File.ReadAllText("userdata.json");
                var userData = JsonConvert.DeserializeObject<UserData>(jsonData);

                if (userData != null)
                {
                    userName = userData.UserName;
                    userId = userData.UserId;
                    accountName = userData.AccountName;
                    Console.WriteLine($"Загружены данные пользователя: {userName}, ID: {userId},{accountName}");
                }
            }
        }
    }
}
