
using bot_tr.handlers;

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



