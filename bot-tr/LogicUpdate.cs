using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
namespace bot_tr
{
    public class LogicUpdate : ILogic

    {

        public  string? userName { get; set; }
        public  long userId { get; set; }
        public  string? accountName { get; set; }
        
        public static bool waitingForConfirmation = false;

        public bool isUserThere
        {
            get; set;
        }
        public string forCheckMessage
        {
            get; set;
        }
        DbHendler dbo = new DbHendler();
        public async Task SentMessege(ITelegramBotClient botClient, Update update, CancellationToken token, string message)
        {
            var chatId = update.Message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, message);
        }

        public  async Task RememberName(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = update.Message.Text;
   
                userId = update.Message.From.Id;
                accountName = update.Message.From.Username;
                
                Console.WriteLine($"Имя пользователя установлено: {userName}, ID: {userId}, {accountName}");
                dbo.AddToDB(userName,userId,accountName);
                FileHandler.SaveUserData(userName, userId, accountName);
            }
        }
        public async Task CheckUser(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            userId = update.Message.From.Id;
            string fromBd = await dbo.CheckWithDB(userId);
            if (fromBd!= string.Empty)
            {
                  userName = fromBd;
            }
        }




        /*    public async override Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
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
            */
    } 
	}
