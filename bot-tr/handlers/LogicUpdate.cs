using Telegram.Bot;
using Telegram.Bot.Types;
using bot_tr.interfaces;
using bot_tr.model;
using Moq;

namespace bot_tr.handlers
{
    public class LogicUpdate : ILogicHandling

    {
        internal string? userName;
        internal long userId;
        private readonly IDataBase dbo;
        public LogicUpdate(IDataBase dbHendler)
        {
            dbo = dbHendler;
        }

        public async Task<Message> SentMessege(ITelegramBotClient botClient, Update update, string message)
        {
            return _ = await botClient.SendTextMessageAsync(update!.Message!.Chat.Id, message);
        }

        public async Task RememberName(Update update)
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

        public async Task<string?> GetUserFromDB(Update update)
        {

            userId = update!.Message!.From!.Id;

            return await dbo.CheckWithDB(userId);
        }

        public async Task<string?> TryToCheckUserFromDB(string fromBd)
        {

            return fromBd != string.Empty ? (userName = fromBd) : (userName = null);
        }

        public async Task<string?> RemoveUser(long id)
        {
            return userName = await dbo.RemoveFromDBbyID(id!);
        }
        
        public async Task<string?> AdminOperation(Update update, int adminsId)
        {
            if (update!.Message!.From!.Id == adminsId)
            {
                return dbo.GetAllData().Result;
            }
            else
            {
                return "нет доступа";
            }
        }
    }
}
