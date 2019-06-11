using System.Collections.Generic;
using NUnit.Framework;

namespace BluePrism.Test
{
    [TestFixture]
    class FileHandlerTest
    {
        [Test]
        public void ProcessFourLetterWords_WhenExecuted_GenerateShortestListOfFourLetterWords()
        {
            // Arrange
            var fileHandler = new FileHandler(4, new WordHandler());

            // Act
            fileHandler.ProcessWords("words-english", "spin", "spot", "Test-Results");

            // Assert
            Assert.That(fileHandler.FinalResult, Is.EquivalentTo(new List<string> {"spin", "spit", "spot"}));
        }
    }
}
