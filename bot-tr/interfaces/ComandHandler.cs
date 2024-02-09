using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bot_tr.handlers;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace bot_tr.interfaces
{
    public abstract class ComandHandler 
    {

        //public bool? isFlag
        //{
        //    get; set;
        //}
        public abstract Task<bool> Handle(ITelegramBotClient botClient, Update update, ILogicHandling logicUpdate, bool isFlag);

    }
    }
