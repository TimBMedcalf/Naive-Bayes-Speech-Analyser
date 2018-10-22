using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    public class Utilities
    {
        UserInterface userInterface = new UserInterface();
        readonly FileProcessor fileProcessor = new FileProcessor();
        readonly Naive_Bayes naive_Bayes = new Naive_Bayes();


        public void Startup()
        {
            fileProcessor.ProcessSpeeches(userInterface.GetTrainingFiles());
        }
        //Pass the training files into the training function

        public void Train()
        {

            List<Word> trainedLabour = TrainLabour();
            List<Word> trainedConservative = TrainConservative();
            List<Word> trainCoalition = TrainCoalition();

            foreach (var word in trainedLabour)
            {
                Console.Write(word.Value + ": " + word.Frequency + " " + word.Probability + "\n");
            }
        }

        List<Word> TrainLabour()
        {
            float nWords = naive_Bayes.SummateWords(fileProcessor.UniqueLabourSpeech);

            float nCat = naive_Bayes.SummateUniqueWords(fileProcessor.UniqueLabourSpeech,
                                                        fileProcessor.UniqueConservativeSpeech,
                                                        fileProcessor.UniqueCoalitionSpeech);

            Dictionary<string, int> tableOne = fileProcessor.WordFrequencyList(fileProcessor.Speeches, 0);
            Dictionary<string, int> tableTwo = fileProcessor.WordFrequencyList(fileProcessor.Speeches, 1);

            Dictionary<string, int> mergeTable = naive_Bayes.MergeFrequencyTable(tableOne, tableTwo);

            List<Word> wordTable = naive_Bayes.WordTable(nCat, nWords, mergeTable);

            return wordTable;
        }

        List<Word> TrainConservative()
        {
            float nWords = naive_Bayes.SummateWords(fileProcessor.UniqueConservativeSpeech);

            float nCat = naive_Bayes.SummateUniqueWords(fileProcessor.UniqueLabourSpeech,
                                                        fileProcessor.UniqueConservativeSpeech,
                                                        fileProcessor.UniqueCoalitionSpeech);

            Dictionary<string, int> tableOne = fileProcessor.WordFrequencyList(fileProcessor.Speeches, 2);
            Dictionary<string, int> tableTwo = fileProcessor.WordFrequencyList(fileProcessor.Speeches, 3);

            Dictionary<string, int> mergeTable = naive_Bayes.MergeFrequencyTable(tableOne, tableTwo);

            List<Word> wordTable = naive_Bayes.WordTable(nCat, nWords, mergeTable);

            return wordTable;
        }

        List<Word> TrainCoalition()
        {
            float nWords = naive_Bayes.SummateWords(fileProcessor.UniqueCoalitionSpeech);

            float nCat = naive_Bayes.SummateUniqueWords(fileProcessor.UniqueLabourSpeech,
                                                        fileProcessor.UniqueConservativeSpeech,
                                                        fileProcessor.UniqueCoalitionSpeech);

            Dictionary<string, int> tableOne = fileProcessor.WordFrequencyList(fileProcessor.Speeches, 4);

            List<Word> wordTable = naive_Bayes.WordTable(nCat, nWords, tableOne);

            return wordTable;
        }
    }
}
