using bot_tr.hendlers;

using bot_tr.interfaces;
using Moq;
using ServiceStack;

namespace test_bot_tr
{
    public class Tests
    {
        private BotHandler _botHendler;
        //private ILogic _logic

        [SetUp]
        public void Setup()
        {
            var mok = new Mock<ILogicHandling>();
            mok.Setups(sendMes=>sendMes.SentMessege(
                )
            _botHendler = new BotHandler("");
        }

        [Test]
        public void Test1()
        {
           
            Assert.Pass();
        }
    }
}