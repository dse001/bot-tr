using bot_tr.hendlers;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using bot_tr.interfaces;
using Moq;

using Telegram.Bot.Requests.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using NUnit.Framework;
namespace test_bot_tr
{

    public class UnitTest1
    {
 
        private LogicUpdate _logicUpdate;
        //private ITelegramBotClient mokClient;
        private Update moktUpdate;
        private CancellationToken token;
        //private IDataBase idatabase;
        [Test]
        [TestFixure]
        public async Task RememberName_Should_AddToDB_And_SaveUserData_When_UserName_IsNullOrEmpty()
        {
            //private BotHandler _botHendler;


            var dbo = new Mock<IDataBase>();
        var botClientMock = new Mock<ITelegramBotClient>();
            var botHandler = new LogicUpdate();
            var update = new Update
            {
                Message = new Message
                {
                    Text = "TestText",
                    From = new User
                    {
                        Id = 123,
                        Username = "TestUser"
                    }
                }
            };
            // Act
            await botHandler.RememberName(null, update, CancellationToken.None);

            // Assert
            dbo.Verify(dbo => dbo.AddToDB("TestText", 123, "TestUser"), Times.Once);
            //// Assert
            //botClientMock.Verify(
            //    client => client.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()),
            //    Times.Once);

            //botClientMock.Verify(
            //    client => client.SendTextMessageAsync(123, "Hello!"),
            //    Times.Once);
        }
    }

    internal class TestFixureAttribute : Attribute
    {
    }
}