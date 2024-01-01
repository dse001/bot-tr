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

        public async static Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Message?.Text != null)
            {
                var chatId = update.Message.Chat.Id;
                if (!waitingForConfirmation && (update.Message?.Text != "/yes"))
                {
                    await botClient.SendTextMessageAsync(chatId, "Привет! Пожалуйста, введите '/yes' для подтверждения регистрации на нашем чудесном мероприятии и введите своё имя, с вами свяжутся");
                }
                var userMessage = update.Message.Text.ToLower();
                switch (userMessage)
                {
                    case "/start":
                        // Игнорируем обработку сообщения "/start"
                        return;

                    case "/yes":
                        if (!waitingForConfirmation)
                        {
                            await botClient.SendTextMessageAsync(chatId, "Отлично! Теперь введите свое имя:");
                            waitingForConfirmation = true;
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(chatId, "Не ожидал такого ответа.");
                        }
                        break;

                    default:
                        if (string.IsNullOrEmpty(userName) && waitingForConfirmation)
                        {
                            userName = update.Message.Text;
                            userId = update.Message.From.Id;
                            accountName = update.Message.From.Username;
                            Console.WriteLine($"Имя пользователя установлено: {userName}, ID: {userId}, {accountName}");
                            SaveUserData();
                            await botClient.SendTextMessageAsync(chatId, $"Приятно познакомиться, {userName}! Твой ID: {userId} {accountName} мы вас найдем, вы записаны, за вами выехали..");
                        }
                        break;
                }
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
