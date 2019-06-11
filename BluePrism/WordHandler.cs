using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BluePrism
{
    public class WordHandler : IWordHandler
    {
        /// <summary>
        /// Storing possible words
        /// </summary>
        private List<StringBuilder> _possibleStringBuilders;

        public WordHandler()
        {
            _possibleStringBuilders = new List<StringBuilder>();
        }

        /// <summary>
        /// List of possible unique word combinations.
        /// Ex: ("spin", "spot", 4) => "s","sp","spi","spo","spin","spit","spon","spot"
        /// </summary>
        /// <param name="startWord">Starting word</param>
        /// <param name="endWord">Ending word</param>
        /// <param name="wordsLength">Size of the word (ex: 4 for four letter words "spin")</param>
        /// <returns>List of strings</returns>
        public List<string> PossibleWords(string startWord, string endWord, int wordsLength)
        {
            if (string.IsNullOrEmpty(startWord) || string.IsNullOrEmpty(endWord))
            {
                throw new ArgumentNullException($"{nameof(startWord)} or {nameof(endWord)} cannot be empty");
            }

            if (wordsLength < 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(wordsLength)} cannot be less than 1.");
            }

            // Get the possible combination of unique words

            var startWordArray = startWord.ToCharArray();
            var endWordArray = endWord.ToCharArray();

            var possibleWords = new List<string>();

            try
            {
                // Find possible combinations for given word length
                for (int i = 0; i < wordsLength; i++)
                {
                    var letterDiff = SpotDifference(startWordArray[i], endWordArray[i]);

                    // Using String Builder
                    PossibleWordsGenerator(letterDiff);

                    foreach (var stringBuilder in _possibleStringBuilders)
                    {
                        Debug.WriteLine(stringBuilder);
                        possibleWords.Add(stringBuilder.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return possibleWords;
        }

        /// <summary>
        /// Spot the difference between 2 chars.
        /// Returns: Unique char array ex: 'a', 'b' => ['a', 'b'] or ex: 'a', 'a' => ['a']
        /// </summary>
        /// <param name="letter1">Source char</param>
        /// <param name="letter2">Char to compare</param>
        /// <returns>Char array</returns>
        private char[] SpotDifference(char letter1, char letter2)
        {
            try
            {
                return letter1 == letter2 ? new[] {letter1} : new[] {letter1, letter2};
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Create word combinations and modify existing state
        /// </summary>
        /// <param name="combinations">Unique char combinations</param>
        private void PossibleWordsGenerator(char[] combinations)
        {
            try
            {
                // Check for existing string combinations 
                // If not exists! start adding new strings into current list
                // No need to compare with existing char letter 
                if (_possibleStringBuilders.Count == 0)
                {
                    foreach (var combination in combinations)
                    {
                        _possibleStringBuilders.Add(new StringBuilder(combination.ToString()));
                    }
                }
                else
                {
                    // In current state: If string list already have possible word combination in _possibleStringBuilders
                    if (_possibleStringBuilders.Count > 0)
                    {
                        // Make a copy of current values in possible word combination (_possibleStringBuilders)
                        // It's better practice to make a copy than working with original data
                        var copyOfPossibleStringBuilders = _possibleStringBuilders;

                        // If current combination parameter have 1 char. Then, join that char next to the existing string
                        // Ex: Current Stings = "s", combinations parameter = 'p' => add new string "sp"
                        if (combinations.Length == 1)
                        {
                            for (int i = 0; i < _possibleStringBuilders.Count; i++)
                            {
                                copyOfPossibleStringBuilders[i].Append(combinations[0].ToString());
                            }
                        }
                        else if (combinations.Length > 1) // If current combinations parameter contains 2 chars
                        {
                            // Generate new copy by concatenating with existing List string (using _possibleStringBuilders)
                            var localCopy = new List<StringBuilder>();

                            // Ex: combinations = ['a','b'], _possibleStringBuilders = "sp", => List{"spa", "spb"}
                            foreach (var existingPossibleStrings in copyOfPossibleStringBuilders)
                            {
                                for (int i = 0; i < combinations.Length; i++)
                                {
                                    var copy = new StringBuilder(existingPossibleStrings + combinations[i].ToString());
                                    localCopy.Add(copy);
                                }
                            }

                            copyOfPossibleStringBuilders = localCopy;
                        }

                        _possibleStringBuilders = copyOfPossibleStringBuilders;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
