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
using System.Text.RegularExpressions;
using System.Threading;


namespace bot_tr
{
    public class BotHandler 
    {
        public static string userName;
        public static long userId;
        public static string? accountName;
        /*
    public bool isUserThere
    {
        get;set;
    }
    public string forCheckMessage
    {
        get; set;
    }
    */
        public static bool waitingForConfirmation { get; set; }

        //public static string dataFilePath = "userdata.json";
        public readonly ITelegramBotClient botClient;
        

     /*   public virtual async Task SentMessege(ITelegramBotClient botClient, Update update, string message)
        {
           
        }

        public virtual async Task RememberName(ITelegramBotClient botClient, Update update, CancellationToken token)
        { }
        public virtual async Task CheckUser(ITelegramBotClient botClient, Update update, CancellationToken token)
        { }
     */
        public BotHandler(string botToken)
        {
            botClient = new TelegramBotClient(botToken);
            botClient.StartReceiving(HandleUpdate, HandlePollingErrorAsync);
        }

        LogicUpdate logicUpdate = new LogicUpdate();

        public async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            if (update.Message?.Text != null)
            {
                waitingForConfirmation = false;
                switch (update.Message?.Text)
                {
                    case string currentMessage when currentMessage == "/start":
                            await logicUpdate.SentMessege(botClient, update, token, "Привет! Пожалуйста, введите '/yes' для подтверждения регистрации на нашем чудесном мероприятии и введите своё имя, с вами свяжутся" +
                                "если хочешь удалить упоминание о себе нажми /no");
                        break;
                       
                    case string currentMessage when currentMessage == "/yes" || (waitingForConfirmation = false):
                        await logicUpdate.SentMessege(botClient, update,token, "Отлично! Теперь введите свое имя:");
                        waitingForConfirmation = true;
                        break;
                        


                    case string currentMessage when currentMessage == "/no":
                        FileHandler.RemoveUserData(userId);
                        await logicUpdate.SentMessege(botClient, update,token, $"ну как хочешь, но мы тебя запомним");
                        break;
                    
                    case string currentMessage when currentMessage != null || (waitingForConfirmation = true):

                        await logicUpdate.RememberName(botClient, update, token);
                        waitingForConfirmation = false;
                        await logicUpdate.SentMessege(botClient, update, token, $"Приятно познакомиться, {logicUpdate.userName}! Твой ID: {logicUpdate.userId} {logicUpdate.accountName} мы вас найдем, вы записаны, за вами выехали..");
                        break;

                    case string currentMessage when (currentMessage != "/no") || (currentMessage != "/yes") || (currentMessage != "/start"):
                        await logicUpdate.SentMessege(botClient, update,token ," test");
                        await logicUpdate.CheckUser(botClient, update, token);
                        break;

                }
                return;
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
        
    }
}
