using bot_tr.hendlers;
using Telegram.Bot.Types;
using Telegram.Bot;
using bot_tr.model;


namespace bottrNunit.tests


{
    public class Tests
    {

        private int dbID;
        
        [Test]
        public async Task Integration_DB_Operations_Tests()
        {
            RememberMessage_Integration_test();
            GetUserFromDB_integration_test();
            RemoveUser_Integration_test();

        }

        [Test]
        public async Task TryToCheckUserFromDB_Unit_Negative_test()
        {
            var logicUpdate = new LogicUpdate(new DbHendler());
            var call = logicUpdate.TryToCheckUserFromDB(String.Empty);
            Assert.That(call.Result, Is.Null);
        }
        
        [Test]
        public async Task TryToCheckUserFromDB_Unit_Positive_test()
        {
            var logicUpdate = new LogicUpdate(new DbHendler());
            var call = logicUpdate.TryToCheckUserFromDB("Test");
            Assert.That(call.Result.ToString(), Is.EqualTo("Test"));
        }
        
        [Test]
        public async Task Checking_Then_SendTextMessageAsync_is_Called_in_test_SentMessege()
        {
            var botClient = new TelegramBotClient("MyTestBot - API - TOKEN");
            var logicUpdate = new LogicUpdate(new DbHendler());
            var update = new Update
            {
                Message = new Message
                {
                    Chat = new Chat { Id = 1234567890, Username = "tester1" },
                    Text = "testText",

                },
            };
            var call = logicUpdate.SentMessege(botClient, update, "testText");
            Assert.That(call.IsCompletedSuccessfully, Is.EqualTo(false));
        }
       
        public async Task RememberMessage_Integration_test()
        {
            var logicUpdate = new LogicUpdate(new DbHendler());
            UserData userdata = new UserData();
            Random random = new Random();
            dbID = random.Next(100, 999999999);
            var update = new Update
            {
                Message = new Message
                {
                    From = new User { Id = dbID, Username = "tester2" },
                    Text = $"testText",

                },
            };
            var call = logicUpdate.RememberName(update);
            Assert.That(call.IsCompleted, Is.True);
            Assert.That(call.Status.ToString(), Is.EqualTo("RanToCompletion"));
            Assert.That(call, Is.Not.Null);
        }
        
        public async Task GetUserFromDB_integration_test()
        {
            var logicUpdate = new LogicUpdate(new DbHendler());
            UserData userdata = new UserData();
            var update = new Update
            {
                Message = new Message
                {
                    From = new User { Id = dbID, Username = "tester2" },
                    Text = $"testText",

                },
            };
            var call = logicUpdate.GetUserFromDB(update);
            Assert.That(call.Result, Is.EqualTo("testText"));
            Assert.That(call.Status.ToString(), Is.EqualTo("RanToCompletion"));
        }
        
        public async Task RemoveUser_Integration_test()
        {
            var logicUpdate = new LogicUpdate(new DbHendler());
            var call = logicUpdate.RemoveUser(dbID);
            Assert.That(call.IsCompletedSuccessfully, Is.True);
            Assert.That(call.Result, Is.EqualTo("testText"));
        }
    } 
}