using Telegram.Bot;
using Telegram.Bot.Types;
using bot_tr.interfaces;
namespace bot_tr.hendlers
{
    public class LogicUpdate : ILogicHandling

    {
        internal string? userName;
        internal long userId;
        internal string? accountName;

        private static bool waitingForConfirmation = false;

        public bool isUserThere
        {
            get; set;
        }
        public string forCheckMessage
        {
            get; set;
        }
        DbHendler dbo = new DbHendler();
        public async Task SentMessege(ITelegramBotClient botClient, Update update, CancellationToken token, string message)
        {
            var chatId = update!.Message!.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, message);
        }

        public async Task RememberName(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = update!.Message!.Text;
                userId = update!.Message!.From!.Id;
                accountName = update.Message.From.Username;

                Console.WriteLine($"Имя пользователя установлено: {userName}, ID: {userId}, {accountName}");
                await dbo.AddToDB(userName!, userId, accountName!);
                FileHandler.SaveUserData(userName!, userId, accountName!);
            }
        }

        public async Task CheckUser(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            userId = update!.Message!.From!.Id;
            string fromBd = await dbo.CheckWithDB(userId);
            _ = fromBd != string.Empty ? (userName = fromBd) : (userName = null);
        }
        public async Task RemoveByID(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            userId = update!.Message!.From!.Id;
            string fromBd = await dbo.RemoveFromDBbyID(userId!);
            if (fromBd != string.Empty)
            {
                userName = fromBd;
            }
        }
    }
}
