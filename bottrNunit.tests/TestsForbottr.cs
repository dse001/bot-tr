
using bot_tr.hendlers;
using Moq;
using Telegram.Bot.Types;
using Telegram.Bot;
using bot_tr.interfaces;
using Newtonsoft.Json.Linq;

namespace bottrNunit.tests


{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        
        }

        [Test]
        public async Task SentMessege_Intgration_Invalid_ID__test()
        {
            var botClient = new TelegramBotClient("6872155143:AAEVSYg6avTl15uqB650WKM6-u30XqOuR5s");
            var update = new Update
            {
                Message = new Message
                {
                    Chat = new Chat { Id = 123 },
                },
            };
            var logicUpdate = new LogicUpdate(new DbHendler());


            // Act
           var t=  logicUpdate.SentMessege(botClient, update, default, "Hello, world!");

            // Assert

            Assert.That(t.IsCompletedSuccessfully,Is.EqualTo(false));
        }
    }

}