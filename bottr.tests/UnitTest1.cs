using Microsoft.VisualStudio.TestTools.UnitTesting;
using bot_tr.hendlers;
using Moq;
using bot_tr.interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace bottr.tests
{
    [TestClass]
    public class BotttrTests
    {
     //   private BotHandler _botHendler;

        [TestMethod]
        public async Task SentMessege_ValidStringIn_ValidStringOut()
        {
            Debug.WriteLine("METHOD");
            // var dbo = new Mock<IDataBase>();
            botClient = new TelegramBotClient(botToken);
            var botHandler = new LogicUpdate();
            //var token = new Mock<CancellationToken>();


            // Теперь вызов SentMessage будет использовать реальный объект ITelegramBotClient
            var result = await botHandler.SentMessage(yourRealBotClient, yourUpdate, CancellationToken.None, "YourMessage");

            // Проверка, что метод возвращает ожидаемый объект Message
            Assert.AreEqual("YourExpectedMessageText", result.Text);
        }
    }
}
