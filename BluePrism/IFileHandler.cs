namespace BluePrism
{
    interface IFileHandler
    {
        void ProcessWords(string dictionaryFile, string startWord, string endWord, string resultFile);
    }
}
