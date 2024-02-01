using Telegram.Bot;
using Telegram.Bot.Types;
using bot_tr.interfaces;
using bot_tr.model;

namespace bot_tr.hendlers
{
    public class LogicUpdate : ILogicHandling

    {
        internal string? userName;
        internal long userId;

       DbHendler dbo = new DbHendler();
        public async Task<Message> SentMessege(ITelegramBotClient botClient, Update update, CancellationToken token, string message)
        {
            return _ = await botClient.SendTextMessageAsync(update!.Message!.Chat.Id, message);        
        }

        public async Task RememberName(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (string.IsNullOrEmpty(userName))
            {
                UserData userData = new UserData()
                {
                    UserName = update!.Message!.Text,
                    UserId = update!.Message!.From!.Id,
                    AccountName = update.Message.From.Username
                };
                await dbo.AddToDB(userData);
            }
        }

        public async Task<string?> GetUserFromDB(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            userId = update!.Message!.From!.Id;
            
           return await dbo.CheckWithDB(userId);
  
        }

        public async Task<string?> TryToCheckUserFromDB(ITelegramBotClient botClient, Update update, CancellationToken token, string fromBd)
        {

            return fromBd != string.Empty ? (userName = fromBd) : (userName = null);
        }

        public async Task<string?> RemoveUser(long id)
        {
            return userName = await dbo.RemoveFromDBbyID(id!);
        }
    }
}
