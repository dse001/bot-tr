using Newtonsoft.Json;
using bot_tr.model;
using File = System.IO.File;

namespace bot_tr.handlers
{
    public  class GetSettings
    {
        public static string botToken;
        public static int? adminId;

        public static void GetFromApsetting()
        {
            // It requaried to use ConfigurationBuilder, but not today..
            var baseUrl = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.ToString();
            var jsonAppSetting = File.ReadAllText($"{baseUrl}\\appSetting.json");
            var appSetting = JsonConvert.DeserializeObject<AppSettingsModel>(jsonAppSetting);
             if (appSetting != null)
             {
                 botToken = appSetting.BotTocken;
                 adminId = appSetting.AdminId;
                Console.WriteLine($"OK -- AppSettings are setup");
            }
            else
            {
                Console.WriteLine($"AppSettings are NOT setup!");
            }
        }
    }   
}
