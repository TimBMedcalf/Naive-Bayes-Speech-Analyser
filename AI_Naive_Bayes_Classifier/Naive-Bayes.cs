using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    public class Naive_Bayes
    {
        List<Word> trainedLabour = new List<Word>();
        List<Word> trainedConservative = new List<Word>();
        List<Word> trainedCoalition = new List<Word>();

        public List<Word> TrainedLabour { get => trainedLabour; set => trainedLabour = value; }
        public List<Word> TrainedConservative { get => trainedConservative; set => trainedConservative = value; }
        public List<Word> TrainedCoalition { get => trainedCoalition; set => trainedCoalition = value; }

        //Gets the amount of all unique words in all of the files
        public float SummateUniqueWords (List<List<string>> labourSpeech, List<List<string>> conservativeSpeech, List<List<string>> coalitionSpeech)
        {
            float summation = 0;

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

        //Gets the amount of the words in a category for instance the amount of words in the labour file
        public float SummateWords(List<List<string>> categroySpeech)
        {
            float summation = 0;

            foreach(var speech in categroySpeech)
            {
                summation += speech.Count;
            }
            return summation;
        }


        public List<Word> WordTable(float nCat, float nWords, Dictionary<string, int> frequencyTable)
        {
            List<Word> table = new List<Word>();

            foreach(KeyValuePair<string, int> entry in frequencyTable)
            {
                float probability = (float)entry.Value / (nCat + nWords);
                table.Add(new Word(entry.Key, entry.Value + 1, probability));
            }
            return table;
        }

        public Dictionary<string, int> MergeFrequencyTable(Dictionary<string, int> tableOne, Dictionary<string, int> tableTwo)
        {
            Dictionary<string, int> mergeTable = new Dictionary<string, int>();

            foreach (KeyValuePair<string, int> entry in tableOne)
            {
                mergeTable.Add(entry.Key, entry.Value);
            }

            foreach (KeyValuePair<string, int> entry in tableTwo)
            {
                if (mergeTable.ContainsKey(entry.Key))
                {
                    mergeTable[entry.Key] += entry.Value;
                }
                else
                {
                    mergeTable.Add(entry.Key, entry.Value);
                }

            }

            return mergeTable;
            // Put all of table one in merge table then compare merge table with containskey with table two
            // if it contains the entry.key then add the key into the merge table while adding up the frequencies
            //if it does not contain then add the entry.value and entry.key into the table
        }



        //Compare all of the words in the new file with all of the words in catagory a then take the Probability of that word then summate all of the
        //probabilities together then times it by the total amount of the catagory over the amount of files given.
        //Highest value is the catagory it is associeted with.

        public float Classify(List<Word> categorySet, List<string> classifySet, int numOfLabour, int numOfConservative, int numOfCoalition, string category)
        {
            float summatedProbability = 0;
            int totalSpeeches = numOfLabour + numOfCoalition + numOfConservative;
            float categoryProbability = 0;

            switch (category)
            {
                case("labour"):

                    foreach (var word in categorySet)
                    {
                        if (classifySet.Contains(word.Value))
                        {
                            summatedProbability += word.Probability;
                        }
                    }
                    categoryProbability = numOfLabour / (float)totalSpeeches;
                    return categoryProbability * summatedProbability;

                case("conservative"):

                    foreach (var word in categorySet)
                    {
                        if (classifySet.Contains(word.Value))
                        {
                            summatedProbability += word.Probability;
                        }
                    }
                    categoryProbability = numOfConservative / (float)totalSpeeches;
                    return categoryProbability * summatedProbability;

                case("coalition"):

                    foreach (var word in categorySet)
                    {
                        if (classifySet.Contains(word.Value))
                        {
                            summatedProbability += word.Probability;
                        }
                    }
                    categoryProbability = numOfCoalition / (float)totalSpeeches;
                    return categoryProbability * summatedProbability;

                 default:
                    return categoryProbability * summatedProbability;
                }
            }

    }
}
