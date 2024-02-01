﻿using System;
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
using bot_tr.interfaces;
using bot_tr.model;

namespace bot_tr.hendlers
{
    public class BotHandler
    {

        public bool? isFlag
        {
            get; set;
        }

        public readonly ITelegramBotClient botClient;
        public BotHandler(string botToken)
        {
            botClient = new TelegramBotClient(botToken);
            botClient.StartReceiving(HandleUpdate, HandlePollingErrorAsync);
        }
        LogicUpdate logicUpdate = new LogicUpdate();
        public async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (isFlag != null)
            {
                await logicUpdate.RememberName(botClient, update, token);
                await logicUpdate.SentMessege(botClient, update, token, $"Приятно познакомиться, {update!.Message!.Text}! Твой ID: {update!.Message!.From!.Id} {update.Message.From.Username} мы вас найдем, вы записаны, за вами выехали..");
                isFlag = null;
                return;
            }
            if (update.Message?.Text != null)
            {

                switch (update.Message?.Text)
                {
                    case string currentMessage when currentMessage == "/start":
                        await logicUpdate.TryToCheckUserFromDB(botClient, update, token, await logicUpdate.GetUserFromDB(botClient, update, token));
                        if (logicUpdate.userName == null)
                        {
                            await logicUpdate.SentMessege(botClient, update, token, "Привет! Пожалуйста, введите '/yes' для подтверждения регистрации на нашем чудесном мероприятии и введите своё имя, с вами свяжутся" +
                                    "если хочешь удалить упоминание о себе нажми /no");
                        }
                        else
                        {
                            await logicUpdate.SentMessege(botClient, update, token, $"Привет {logicUpdate.userName} если хочешь удалить упоминание о себе нажми /no");
                        }
                        break;

                    case string currentMessage when currentMessage == "/yes":
                        await logicUpdate.SentMessege(botClient, update, token, "Отлично! Теперь введите свое имя:");
                        isFlag = true;
                        return;

                    case string currentMessage when currentMessage == "/no":
                        await logicUpdate.TryToCheckUserFromDB(botClient, update, token, await logicUpdate.GetUserFromDB(botClient, update, token));
                        string delededUserName = await logicUpdate.RemoveUser(update!.Message!.From!.Id);
                        await logicUpdate.SentMessege(botClient, update, token, $"{delededUserName} ну как хочешь, но мы тебя запомним");
                        break;

                    case string currentMessage when currentMessage != "/no" || currentMessage != "/yes" || currentMessage != "/start":
                        await logicUpdate.TryToCheckUserFromDB(botClient, update, token, await logicUpdate.GetUserFromDB(botClient, update, token));
                        if (logicUpdate.userName == null)
                        {
                            await logicUpdate.SentMessege(botClient, update, token, $" тебя мы не знаем , можешь записаться на наше чудесное мероприятие  нажав /yes");
                        }
                        else
                        {
                            await logicUpdate.SentMessege(botClient, update, token, $"Привет {logicUpdate.userName} если хочешь удалить упоминание о себе нажми /no");
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

    }
}
