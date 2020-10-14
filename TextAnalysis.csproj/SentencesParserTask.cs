using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            if (string.IsNullOrEmpty(text))
                return sentencesList;
            text = text.ToLower();
            var sentences = text.Split(new char[] { '.', '!', '?', ';', ':', '(', ')' }, System.StringSplitOptions.RemoveEmptyEntries);
			for (var i = 0; i < sentences.Length; i++)
			{
                var wordList = new List<string>();
				foreach (var sentence in sentences[i])
				{
                    if (!char.IsLetter(sentence) && sentence != '\'')
                        sentences[i] = sentences[i].Replace(sentence, ' ');
                }

                var words = sentences[i].Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
				foreach (var word in words)
				{
                    if (WordIsLetter(word))
                        wordList.Add(word);
				}
                if (wordList.Count > 0)
                    sentencesList.Add(wordList);
            }
            return sentencesList;
        }

        public static bool WordIsLetter(string word)
		{
            var symbols = word.ToCharArray();
			foreach (var symbol in symbols)
                if (!(symbol == '\'' || char.IsLetter(symbol)))
                    return false;
            return true;
		}
    }
}