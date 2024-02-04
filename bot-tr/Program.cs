using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using File = System.IO.File;
using Telegram.Bot.Polling;
using System.Runtime.CompilerServices;
//using  bot_tr;
using bot_tr.interfaces;
using bot_tr.model;
using bot_tr.handlers;
using Moq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bot_tr;
class Program
{
    static void Main(string[] args)
    {
        GetSettings.GetFromApsetting();
        using CancellationTokenSource cts = new();
        var botToken = GetSettings.botToken;
        var botHandler = new BotHandler(botToken, new LogicUpdate(new DbHandler()));
        Console.WriteLine("bottr is started, for exit program tap enter.");
        Console.ReadLine();
        cts.Cancel();
    }
}



