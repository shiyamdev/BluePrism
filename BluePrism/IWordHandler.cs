using System.Collections.Generic;

namespace BluePrism
{
    public interface IWordHandler
    {
        List<string> PossibleWords(string startWord, string endWord, int wordsLength);

    }
}