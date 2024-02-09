using Telegram.Bot.Types;
using Telegram.Bot;
using bot_tr.interfaces;

namespace bot_tr.handlers
{
    public class BotHandler
    {
        public readonly ILogicHandling logicUpdate;
        public readonly ITelegramBotClient botClient;
        public readonly ComandHandler comandHandler;
        public bool isFlag { get; set; }
        public BotHandler(string botToken, ILogicHandling logicUpdate)

        {
            this.comandHandler = comandHandler;
            this.logicUpdate = logicUpdate;
            botClient = new TelegramBotClient(botToken);
            botClient.StartReceiving(HandleUpdate, ErrorHandler.HandlePollingErrorAsync);
        }
        private readonly Dictionary<string, ComandHandler> commandHandlers = new Dictionary<string, ComandHandler>
        {
            { "/start", new StartCommand() },
            { "/yes", new YesCommand() },
            { "/no", new NoCommand() },
            { "/admin", new AdminCommand() }
        };
        public async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Message?.Text != null && commandHandlers.TryGetValue(update.Message.Text, out var handler) && isFlag==false )
            {
                isFlag = await handler.Handle(botClient, update, logicUpdate, isFlag);
                return;
            }
            if (update.Message?.Text != null && isFlag == true)
            {
                await logicUpdate.RememberName(update);
                await logicUpdate.SentMessege(botClient, update, $"Приятно познакомиться, {update!.Message!.Text}! Твой ID: {update!.Message!.From!.Id} {update.Message.From.Username} мы вас найдем, вы записаны, за вами выехали..");
                isFlag = false;
                return;
            }
            else
            {
                string userName = await logicUpdate.TryToCheckUserFromDB(await logicUpdate.GetUserFromDB(update));
                if (userName == null)
                {
                    await logicUpdate.SentMessege(botClient, update, $" тебя мы не знаем , можешь записаться на наше чудесное мероприятие  нажав /yes");
                }
                else
                {
                    await logicUpdate.SentMessege(botClient, update, $"Привет {userName} если хочешь удалить упоминание о себе нажми /no");
                }
            }
        }
    }
}
