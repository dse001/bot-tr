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


namespace bot_tr;
class Program
{
    static void Main(string[] args)
    {


        using CancellationTokenSource cts = new();
        var botToken = "6872155143:AAEVSYg6avTl15uqB650WKM6-u30XqOuR5s";
        var botHandler = new BotHandler(botToken);


        Console.WriteLine("Бот запущен. Нажмите Enter для выхода.");
        Console.ReadLine();

  
        cts.Cancel();
    }
}



