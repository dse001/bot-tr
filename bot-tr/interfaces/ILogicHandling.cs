using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace bot_tr.interfaces
{
    public interface ILogicHandling
    { 
        Task<Message> SentMessege(ITelegramBotClient botClient, Update update, CancellationToken token, string message);
        Task RememberName(ITelegramBotClient botClient, Update update, CancellationToken token);
        Task<string> GetUserFromDB(ITelegramBotClient botClient, Update update, CancellationToken token);
        Task<string> TryToCheckUserFromDB(ITelegramBotClient botClient, Update update, CancellationToken token, string fromBd);
        Task<string?> RemoveUser(long id);

    }
}
