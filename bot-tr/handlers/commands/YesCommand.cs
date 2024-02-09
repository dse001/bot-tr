using bot_tr.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace bot_tr.handlers
{
    internal class YesCommand : ComandHandler
    {
        public override async Task<bool> Handle(ITelegramBotClient botClient, Update update, ILogicHandling logicUpdate,  bool isFlag)
        {
            // Реализация обработки команды "/start"
            string _ = await logicUpdate.TryToCheckUserFromDB(await logicUpdate.GetUserFromDB(update));

            await logicUpdate.SentMessege(botClient, update, "Отлично! Теперь введите свое имя:");
            isFlag = true;
            return isFlag;
        }
    }
}
