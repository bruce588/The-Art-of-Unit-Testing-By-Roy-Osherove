using FluentAssertions;
using NSubLab.Targets;
using NSubstitute;
using NUnit.Framework;

namespace NSubLab.Tests
{
    [TestFixture]
    public class MyNSubBasic
    {
        [Test]
        public void GetFileNameIndex_FakeLab_Returns()
        {
            //Arrange
            //設定假物件的輸入與回傳值
            var fileNameRules = Substitute.For<IFileNameRules>();
            fileNameRules.GetFileNameIndex(Arg.Any<int>()).Returns("file1");
            

            //Act
            string result=fileNameRules.GetFileNameIndex(3);

            //Assert
            result.Should().Be("file1");
            
        }
    }
}