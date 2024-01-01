using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace bot_tr
{
	public class LogicUpdate : BotHandler

    {
		public LogicUpdate(string botToken) : base(botToken)
		{
		}

		public async override Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
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
}