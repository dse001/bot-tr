using Newtonsoft.Json;
using File = System.IO.File;

using bot_tr.model;
namespace bot_tr.hendlers
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
        public static void SaveUserData(string userName, long userId, string accountName)
        {
            var userData = new UserData { UserName = userName, UserId = userId, AccountName = accountName };
            var jsonData = JsonConvert.SerializeObject(userData);

            File.WriteAllText("userdata.json", jsonData);
            Console.WriteLine($"Данные пользователя сохранены: {userName}, ID: {userId}, username {accountName}");
        }

        public static void RemoveUserData(long userIDforDel)
        {

            if (File.Exists("userdata.json"))
            {
                var jsonData = File.ReadAllText("userdata.json");

                if (!string.IsNullOrEmpty(jsonData))
                {
                    var usersDataList = JsonConvert.DeserializeObject<UserDataList>(jsonData);

                    if (usersDataList != null)
                    {
                        var userToRemove = usersDataList.UsersDataList.FirstOrDefault(u => u.UserId == userIDforDel);

                        if (userToRemove != null)
                        {
                            usersDataList.UsersDataList.Remove(userToRemove);

                            jsonData = JsonConvert.SerializeObject(usersDataList);
                            File.WriteAllText("userdata.json", jsonData);

                            Console.WriteLine($"Данные пользователя с ID {userIDforDel} удалены.");
                            return;
                        }
                    }

                }
            }
        }
        public static void CheckInJson(long userIDforCheck, bool isUserThere)
        {

            if (File.Exists("userdata.json"))
            {


                var jsonData = File.ReadAllText("userdata.json");
                var userData = JsonConvert.DeserializeObject<UserData>(jsonData);

                if (userData != null)
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        var userDataList = JsonConvert.DeserializeObject<List<UserData>>(jsonData);

                        if (userDataList != null)
                        {
                            var userToRemove = userDataList.FirstOrDefault(u => u.UserId == userIDforCheck);

                            if (userToRemove != null)
                            {
                                ;
                                isUserThere = true;
                            }
                        }

                    }
            }
        }

    }

}
