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
    internal class AdminCommand : ComandHandler
    {
        public override async Task<bool> Handle(ITelegramBotClient botClient, Update update, ILogicHandling logicUpdate,  bool isFlag)
        {
            string allDataForAdmin = await logicUpdate.AdminOperation(update, (int)GetSettings.adminId);
            await logicUpdate.SentMessege(botClient, update, $"{allDataForAdmin}");
            return false;
        }
    }
}
