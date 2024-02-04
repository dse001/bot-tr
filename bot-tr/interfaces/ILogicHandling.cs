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
        Task<Message> SentMessege(ITelegramBotClient botClient, Update update,  string message);
        Task RememberName(Update update);
        Task<string> GetUserFromDB(Update update);
        Task<string> TryToCheckUserFromDB(string fromBd);
        Task<string?> RemoveUser(long id);

    }
}
