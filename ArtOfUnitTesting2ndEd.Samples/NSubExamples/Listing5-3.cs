using System;
using Chapter5.LogAn;
using NSubstitute;
using NUnit.Framework;

namespace NSubExamples
{
    [TestFixture]
    class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            ILogger logger = Substitute.For<ILogger>();

            LogAnalyzer analyzer = new LogAnalyzer(logger);

            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");

           logger.Received().LogError("Filename too short: a.txt");//不傳入參數,比對完全一致
           
        }
        
        [Test]
        public void Analyze_BruceLab_CallLogger()
        {
            ILogger logger = Substitute.For<ILogger>();

            LogAnalyzer analyzer = new LogAnalyzer(logger);

            analyzer.MinNameLength = 6;
            string testFileName = "a.txt";
            analyzer.Analyze(testFileName);

            //測試Received()方法在呼叫LogError時,傳入的參數
            logger.Received().LogError(Arg.Is<string>(x=>x.Contains(testFileName)));
           
        }

        [Test]
        public void Analyze_TooShortFileName_CallLoggerArgMatchers()
        {
            ILogger logger = Substitute.For<ILogger>();

            LogAnalyzer analyzer = new LogAnalyzer(logger);

            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");

           logger.Received().LogError(Arg.Is<string>(s => s.Contains("too short")));
        }

        [Test]
        public void RecursiveFakes()
        {
            IPerson fake = Substitute.For<IPerson>();

            Assert.IsNotNull(fake.GetManager());
            Assert.IsNotNull(fake.GetManager().GetManager().GetManager());
        }
    }


    public interface IPerson
    {
        IPerson GetManager();
    }
}
