using bot_tr.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace bot_tr.handlers
{
    public class StartCommand : ComandHandler
    {
        public override async Task<bool> Handle(ITelegramBotClient botClient, Update update, ILogicHandling logicUpdate, bool isFlag)
        {
            // Реализация обработки команды "/start"
            string _ = await logicUpdate.TryToCheckUserFromDB(await logicUpdate.GetUserFromDB(update));

            if (_ == null)
            {
                await logicUpdate.SentMessege(botClient, update, "Привет! Пожалуйста, введите '/yes' для подтверждения регистрации на нашем чудесном мероприятии и введите своё имя, с вами свяжутся" +
                        "если хочешь удалить упоминание о себе нажми /no");
                return false;
            }
            else
            {
                await logicUpdate.SentMessege(botClient, update, $"Привет {_} если хочешь удалить упоминание о себе нажми /no");
                return false;
            }
        }
    }
}

