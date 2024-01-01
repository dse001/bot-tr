using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Newtonsoft.Json;
using File = System.IO.File;
using Telegram.Bot.Exceptions;


namespace bot_tr
{
    public class BotHandler
    {
        public static string userName;
        public static long userId;
        public static string? accountName;

        public static bool waitingForConfirmation = false;

        //public static string dataFilePath = "userdata.json";
        public readonly ITelegramBotClient botClient;
       

        public BotHandler(string botToken)
        {
            botClient = new TelegramBotClient(botToken);
            botClient.StartReceiving(HandleUpdate, HandlePollingErrorAsync);
        }

        public virtual async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Message?.Text != null)
            {

                //тут надо что то засунуть что запустит override HandleUpdate, из LogicUpdate
            }
        }
        public static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

         public static void SaveUserData()
        {
            var userData = new UserData { UserName = userName, UserId = userId, AccountName = accountName };
            var jsonData = JsonConvert.SerializeObject(userData);

            File.WriteAllText("userdata.json", jsonData);
            Console.WriteLine($"Данные пользователя сохранены: {userName}, ID: {userId}, username {accountName}");
        }
    }
}
