using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    public class Naive_Bayes
    {
        FileProcessor fileProcessor;

        public void CalculateProbablity(List<List<string>> speeches, int index)
        {
            for (int i = 0; i <= speeches[index].Count; i++)
            {
                Dictionary<string, int> frequencyTable = fileProcessor.WordFrequencyList(speeches, index);
            }
        }


        public float SummateWords (List<List<string>> speeches)
        {
            float r = 0;
            return r;
        }
    }
}
