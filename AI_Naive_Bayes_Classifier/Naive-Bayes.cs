using System;
using System.Collections.Generic;
using System.Linq;

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
            List<List<string>> metaUniqueList = new List<List<string>>();
            List<string> speechesList = new List<string>();
            float summation = 0;

            foreach (var speech in labourSpeech)
            {
                metaUniqueList.Add(speech);
            }

            foreach (var speech in conservativeSpeech)
            {
                metaUniqueList.Add(speech);
            }

            foreach (var speech in coalitionSpeech)
            {
                metaUniqueList.Add(speech);
            }

            foreach(var speech in metaUniqueList)
            {
                foreach(var text in speech)
                {
                    speechesList.Add(text);
                }
            }

            List<string> distinct = speechesList.Distinct().ToList();
            summation += distinct.Count();
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
                float probability = (entry.Value + 1) / (float)(nCat + nWords);
                table.Add(new Word(entry.Key, entry.Value, probability));
            }
            return table;
        }

        //Compare all of the words in the new file with all of the words in catagory a then take the Probability of that word then summate all of the
        //probabilities together then times it by the total amount of the catagory over the amount of files given.
        //Highest value is the catagory it is associeted with.

        public float Classify(List<Word> categorySet, List<string> trainingSet, int numOfLabour, int numOfConservative, int numOfCoalition, string category)
        {
            float summatedProbability = 0;
            int totalSpeeches = numOfLabour + numOfCoalition + numOfConservative;
            float categoryProbability = 0;

            switch (category)
            {
                case("labour"):

                    foreach (var word in categorySet)
                    {
                        if (trainingSet.Contains(word.Value))
                        {
                            summatedProbability += word.Probability;
                        }
                    }
                    categoryProbability = numOfLabour / (float)totalSpeeches;
                    return categoryProbability * summatedProbability;

                case("conservative"):

                    foreach (var word in categorySet)
                    {
                        if (trainingSet.Contains(word.Value))
                        {
                            summatedProbability += word.Probability;
                        }
                    }
                    categoryProbability = numOfConservative / (float)totalSpeeches;
                    return categoryProbability * summatedProbability;

                case("coalition"):

                    foreach (var word in categorySet)
                    {
                        if (trainingSet.Contains(word.Value))
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
