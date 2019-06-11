using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BluePrism
{
    public class FileHandler : IFileHandler
    {
        private readonly int _wordsLength;
        private readonly IWordHandler _wordHandler;

        public FileHandler(int wordsLength, IWordHandler wordHandler)
        {
            _wordsLength = wordsLength;
            _wordHandler = wordHandler;
        }

        public List<string> FinalResult = new List<string>();

        /// <summary>
        /// Returns shortest list of four letter words.
        /// </summary>
        /// <param name="dictionaryFile">Source file</param>
        /// <param name="startWord">4 letter starting word</param>
        /// <param name="endWord">4 letter ending word</param>
        /// <param name="resultFile">Final output file</param>
        public void ProcessWords(string dictionaryFile, string startWord, string endWord,
            string resultFile)
        {
            try
            {
                var possibleWordCombinations = _wordHandler.PossibleWords(startWord, endWord, _wordsLength);

                PossibleWordCombinationsSearch(dictionaryFile, possibleWordCombinations);

                GenerateResultFile(resultFile);
            }
            catch (Exception e)
            {
                // Log
                Console.WriteLine(e);
                throw;
            }
        }

        private void PossibleWordCombinationsSearch(string dictionaryFile, List<string> possibleWordCombinations)
        {
            if (possibleWordCombinations == null)
            {
                throw new ArgumentNullException($"{nameof(possibleWordCombinations)} parameter cannot be null");
            }

            if (string.IsNullOrEmpty(dictionaryFile))
            {
                throw new ArgumentNullException($"{nameof(dictionaryFile)} parameter cannot be empty");
            }

            var possibleWordCombinationsToSearch =
                possibleWordCombinations.Where(w => w.Length == _wordsLength).ToList();

            foreach (var possibleWordCombination in possibleWordCombinationsToSearch)
            {
                SearchTheFile(dictionaryFile, possibleWordCombination);
            }
        }

        private void SearchTheFile(string dictionaryFile, string possibleString)
        {
            try
            {
                var file = new FileStream($@"../../../{dictionaryFile}.txt", FileMode.Open);

                using (var reader = new StreamReader(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        //words.Add(line);
                        if (line.Length == _wordsLength)
                        {
                            if (line.IndexOf(possibleString, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            {
                                FinalResult.Add(line);
                                break;
                            }
                        }
                    }
                }
            }
            catch (IOException ioException)
            {
                // Log 
                Console.WriteLine(ioException);
                throw;
            }
        }

        private void GenerateResultFile(string resultFile)
        {
            var file = $@"../../../{resultFile}.txt";

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    foreach (var result in FinalResult)
                    {
                        writer.WriteLine(result);
                    }
                }
            }
            catch (IOException ioException)
            {
                // Log 
                Console.WriteLine(ioException);
                throw;
            }
        }
    }
}

