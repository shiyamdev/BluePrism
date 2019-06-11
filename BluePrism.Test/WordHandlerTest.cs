using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BluePrism.Test
{
    [TestFixture]
    class WordHandlerTest
    {
        private IWordHandler _wordHandler;

        [SetUp]
        public void SetUp()
        {
            _wordHandler = new WordHandler();
        }

        [Test]
        public void PossibleWords_StartAndEndPassed_ReturnsPossibleCombinations1()
        {
            // Arrange 
            var startWord = "spin";
            var endWord = "spot";

            var data = new List<string>
            {
                "s",
                "sp",
                "spi",
                "spo",
                "spin",
                "spit",
                "spon",
                "spot"
            };

            // Act
            var result = _wordHandler.PossibleWords(startWord, endWord, 4);

            Assert.That(result, Is.EquivalentTo(data));

        }

        [Test]
        public void PossibleWords_StartAndEndPassed_ReturnsPossibleCombinations2()
        {
            // Arrange 
            var startWord = "baku";
            var endWord = "baku";

            var data = new List<string>
            {
                "b",
                "ba",
                "bak",
                "baku"
            };

            // Act
            var result = _wordHandler.PossibleWords(startWord, endWord, 4);

            //Assert
            Assert.That(result, Is.EquivalentTo(data));
        }

        [Test]
        public void PossibleWords_StartAndEndPassed_ReturnsPossibleCombinations3()
        {
            // Arrange 
            var startWord = "xxxx";
            var endWord = "yyyy";

            var data = new List<string>
            {
                "x",
                "y",
                "xx",
                "xy",
                "yx",
                "yy",
                "xxx",
                "xxy",
                "xyx",
                "xyy",
                "yxx",
                "yxy",
                "yyx",
                "yyy",
                "xxxx",
                "xxxy",
                "xxyx",
                "xxyy",
                "xyxx",
                "xyxy",
                "xyyx",
                "xyyy",
                "yxxx",
                "yxxy",
                "yxyx",
                "yxyy",
                "yyxx",
                "yyxy",
                "yyyx",
                "yyyy"
            };

            // Act
            var result = _wordHandler.PossibleWords(startWord, endWord, 4);

            Assert.That(result, Is.EquivalentTo(data));
        }

        [Test]
        public void PossibleWords_WhenPassedNullOrEmptyString_ThrowsArgumentNullException()
        {
            // Assert
            Assert.That(()=>_wordHandler.PossibleWords(null, null, 4), Throws.ArgumentNullException);

        }

        [Test]
        public void PossibleWords_WhenPassedWordsLegthLessThanZero_ArgumentOutOfRangeException()
        {
            // Assert
            Assert.That(() => _wordHandler.PossibleWords("spin", "spot", 0), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }


        //[Test]
        //[TestCase('s', 's', new[] { 's' })]
        //[TestCase('i', 'o', new[] { 'i', 'o' })]
        //public void SpotDifference_When2CharPassed_ReturnsUniqueCombination(char letter1, char letter2, char[] expectedResult)
        //{
        //    // Act
        //    var result = _wordHandler.SpotDifference(letter1, letter2);

        //    //
        //    Assert.That(result, Is.EquivalentTo(expectedResult));
        //}
    }
}
