using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace bot_tr
{
    internal interface ILogic
    {
        Task SentMessege(ITelegramBotClient botClient, Update update, CancellationToken token, string message);
        Task RememberName(ITelegramBotClient botClient, Update update, CancellationToken token);
        Task CheckUser(ITelegramBotClient botClient, Update update, CancellationToken token);
    }
}
