using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            for (int i = 0; i < 2; i++)
			{
                foreach (var sentence in text)
                {
                    string[] wordReserve = new string[2];
                    bool referenceValue = true;
                    foreach (var word in sentence)
                    {
                        if (wordReserve[0] == null)
                        {
                            wordReserve[0] = word;
                            continue;
						}
						else if (i == 0)
						{
							result.Add(wordReserve[i], word);
							wordReserve[i] = word;
							continue;
						}
						else
						{
                            if (referenceValue)
							{
                                wordReserve[1] = word;
                                referenceValue = false;
                                continue;
                            }
							else
							{
                                result.Add(wordReserve[0] + " " + wordReserve[1], word);
                                wordReserve[0] = wordReserve[1];
                                wordReserve[1] = word;
							}
							
						}
					}
				}
            }
            return result;
        }
   }
}