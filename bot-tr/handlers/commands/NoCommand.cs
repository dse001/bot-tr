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
    internal class NoCommand : ComandHandler
    {
        public override async Task<bool> Handle(ITelegramBotClient botClient, Update update, ILogicHandling logicUpdate,  bool isFlag)
        {
            await logicUpdate.TryToCheckUserFromDB(await logicUpdate.GetUserFromDB(update));
            string delededUserName = await logicUpdate.RemoveUser(update!.Message!.From!.Id);
            await logicUpdate.SentMessege(botClient, update, $"{delededUserName} ну как хочешь, но мы тебя запомним");
            return false;
        }
    }
}
