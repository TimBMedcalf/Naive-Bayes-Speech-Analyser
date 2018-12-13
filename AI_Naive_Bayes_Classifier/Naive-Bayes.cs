using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AI_Naive_Bayes_Classifier
{
    public class Naive_Bayes
    {
        public List<Word> TrainedLabour { get; set; } = new List<Word>();
        public List<Word> TrainedConservative { get; set; } = new List<Word>();
        public List<Word> TrainedCoalition { get; set; } = new List<Word>();

        //Gets the amount of all unique words in all of the files
        public float SummateUniqueWords(List<List<string>> labourSpeech, List<List<string>> conservativeSpeech, List<List<string>> coalitionSpeech)
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

            foreach (var speech in metaUniqueList)
            {
                foreach (var text in speech)
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

            foreach (var speech in categroySpeech)
            {
                summation += speech.Count;
            }
            return summation;
        }

        /// <summary>
        /// Calculates the Probability of words appearing and in the speech and returns a list of word objects
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="nCat">Number of catagories.</param>
        /// <param name="nWords">Number of words.</param>
        /// <param name="frequencyTable">Table that holds the frequency of words.</param>
        public List<Word> WordTable(float nCat, float nWords, Dictionary<string, int> frequencyTable)
        {
            List<Word> table = new List<Word>();

            foreach (KeyValuePair<string, int> entry in frequencyTable)
            {
                float probability = (entry.Value + 1) / (float)(nCat + nWords); //Needs float cast to run even though VS says otherwise.
                table.Add(new Word(entry.Key, entry.Value, probability));
            }
            return table;
        }


        public void TableToFile(List<Word> wordTable, string catagory)
        {
            using (StreamWriter file =
             new StreamWriter(@catagory + "training.txt"))
            {
                foreach (Word word in wordTable)
                {
                    file.WriteLine(word.Value + " " + word.Frequency + " " + word.Probability);
                }
            }
        }

        //Compare all of the words in the new file with all of the words in catagory a then take the Probability of that word then summate all of the
        //probabilities together then times it by the total amount of the catagory over the amount of files given.
        //Highest value is the catagory it is associeted with.

        public double Classify(List<Word> categorySet, List<string> classifyingSet, int numOfLabour, int numOfConservative, int numOfCoalition, string category)
        {
            int totalSpeeches = numOfLabour + numOfCoalition + numOfConservative;
            double categoryProbability = 0;
            double summatedProbability = 0;

            switch (category)
            {
                case ("labour"):

                    foreach (var word in categorySet)
                    {
                        if (classifyingSet.Contains(word.Value))
                        {
                            summatedProbability += Math.Log(word.Probability);
                        }
                        else
                        {
                            //If the word does not exist in the current classifying set then add the word with a default fequency of 1...
                            // then calculate the probability of that word and add it to the running total of probability
                            summatedProbability += 1 / (Utilities.N_CAT + Utilities.LABOUR_WORDS);
                        }

                    }

                    categoryProbability = numOfLabour / (float)totalSpeeches;
                    return Math.Log(categoryProbability) + summatedProbability;

                case ("conservative"):

                    foreach (var word in categorySet)
                    {
                        if (classifyingSet.Contains(word.Value))
                        {
                            summatedProbability += Math.Log(word.Probability);
                        }
                        else
                        {
                            //If the word does not exist in the current classifying set then add the word with a default fequency of 1...
                            // then calculate the probability of that word and add it to the running total of probability
                            summatedProbability += 1 / (Utilities.N_CAT + Utilities.CONSERVATIVE_WORDS);
                        }
                    }
                    categoryProbability = numOfConservative / (float)totalSpeeches;
                    return Math.Log(categoryProbability) + summatedProbability;

                case ("coalition"):

                    foreach (var word in categorySet)
                    {
                        if (classifyingSet.Contains(word.Value))
                        {
                            summatedProbability += Math.Log(word.Probability);
                        }
                        else
                        {
                            //If the word does not exist in the current classifying set then add the word with a default fequency of 1...
                            // then calculate the probability of that word and add it to the running total of probability
                            summatedProbability += 1 / (Utilities.N_CAT + Utilities.COALITION_WORDS);
                        }
                    }
                    categoryProbability = numOfCoalition / (float)totalSpeeches;
                    return Math.Log(categoryProbability) + summatedProbability;
                default:
                    throw new ArgumentException("Classification type not defined", "catagory");
            }
        }
    }
}