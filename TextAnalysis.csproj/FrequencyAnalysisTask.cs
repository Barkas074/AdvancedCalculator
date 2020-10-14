using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
	static class FrequencyAnalysisTask
	{
		public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
		{
			var result = new Dictionary<string, string>();
			var frequencyPhrases = new Dictionary<string, int>();
			for (int i = 0; i < 2; i++)
			{
				foreach (List<string> sentence in text)
				{
					if (i == 0)
						CalculateBigrams(sentence, result, frequencyPhrases, out result, out frequencyPhrases);
					else
						CalculateTrigrams(sentence, result, frequencyPhrases, out result, out frequencyPhrases);
				}
			}
			//result = EditDictionary(result, frequencyPhrases);
			return result;
		}

		static void CalculateBigrams(List<string> sentence, Dictionary<string, string> resultBigrams, 
			Dictionary<string, int> frequencyPhrases, 
			out Dictionary<string, string> result, out Dictionary<string, int> frequencyPhrasesNew)
		{
			string key;
			for (int i = 0; i < sentence.Count - 1; i++)
			{
				key = sentence[i];
				CalculateNgrams(sentence, resultBigrams, key, i, frequencyPhrases, out resultBigrams, out frequencyPhrases);
			}
			result = resultBigrams;
			frequencyPhrasesNew = frequencyPhrases;
		}

		static void CalculateTrigrams(List<string> sentence, Dictionary<string, string> resultTrigrams, Dictionary<string, int> frequencyPhrases, out Dictionary<string, string> result, out Dictionary<string, int> frequencyPhrasesNew)
		{
			string key;
			for (int i = 1; i < sentence.Count - 1; i++)
			{
				key = sentence[i - 1] + " " + sentence[i];
				CalculateNgrams(sentence, resultTrigrams, key, i, frequencyPhrases, out resultTrigrams, out frequencyPhrases);
			}
			result = resultTrigrams;
			frequencyPhrasesNew = frequencyPhrases;
		}

		static void CalculateNgrams(List<string> sentence, Dictionary<string, string> resultNgrams, string key, int i, Dictionary<string, int> frequencyPhrases, out Dictionary<string, string> result, out Dictionary<string, int> frequencyPhrasesNew)
		{
			string value = "";
			bool isValue = resultNgrams.TryGetValue(key, out value);
			int compareString = string.CompareOrdinal(value, sentence[i + 1]);
			if (!isValue)
			{
				resultNgrams.Add(key, sentence[i + 1]);
				frequencyPhrases.Add(key + ":" + sentence[i + 1], 1);
			}
			else
			{
				if (frequencyPhrases.ContainsKey(key + ":" + sentence[i + 1]))
				{
					frequencyPhrases[key + ":" + sentence[i + 1]] += 1;
					UpdateResult(sentence, resultNgrams, key, i, frequencyPhrases, value);
				}
				else
				{
					frequencyPhrases.Add(key + ":" + sentence[i + 1], 1);
					UpdateResult(sentence, resultNgrams, key, i, frequencyPhrases, value);
				}
			}
			result = resultNgrams;
			frequencyPhrasesNew = frequencyPhrases;
		}

		static Dictionary<string, string> UpdateResult(List<string> sentence, Dictionary<string, string> resultNgrams, string key, int i, Dictionary<string, int> frequencyPhrases, string value)
		{
			int compareString = string.CompareOrdinal(value, sentence[i + 1]);
			bool maxValue = frequencyPhrases[key + ":" + sentence[i + 1]] > frequencyPhrases[key + ":" + value];
			bool equallyValue = frequencyPhrases[key + ":" + sentence[i + 1]] == frequencyPhrases[key + ":" + value];
			if (maxValue || equallyValue && compareString > 0)
			{
				resultNgrams.Remove(key);
				resultNgrams.Add(key, sentence[i + 1]);
			}
			return resultNgrams;
		}
	}
}