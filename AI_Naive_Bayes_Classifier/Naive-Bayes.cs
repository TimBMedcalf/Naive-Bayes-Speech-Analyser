using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    public class Naive_Bayes
    {
        public int SummateUniqueWords (List<List<string>> labourSpeech, List<List<string>> conservativeSpeech, List<List<string>> coalitionSpeech)
        {
            int summation = 0;

            foreach (var speech in labourSpeech)
            {
                summation += speech.Count; 
            }

            foreach (var speech in conservativeSpeech)
            {
                summation += speech.Count;
            }

            foreach (var speech in coalitionSpeech)
            {
                summation += speech.Count;
            }

            return summation;
        }

        public int SummateWords(List<List<string>> categroySpeech)
        {
            int summation = 0;

            foreach(var speech in categroySpeech)
            {
                summation += speech.Count;
            }

            return summation;
        }

        public List<Word> WordTable(int nCat, int nWords, Dictionary<string, int> FrequencyList)
        {
            List<Word> table = new List<Word>();

            foreach(KeyValuePair<string, int> entry in FrequencyList)
            {
                float probability = entry.Value / (nCat + nWords);
                table.Add(new Word(entry.Key, entry.Value - 1, probability));
            }
            return table;
        }
    }
}
