using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize the user interface
            UserInterface userInterface = new UserInterface();
            
            //Pass the training files into the training function
            Train(userInterface.GetTrainingFiles());
        }

        static void Train(string[] trainingFiles)
        {
            //Creates an object of fileprocessor that takes in the file paths
            FileProcessor fileProcessor = new FileProcessor(trainingFiles);
            Naive_Bayes naive_Bayes = new Naive_Bayes();

            float nWords = naive_Bayes.SummateWords(fileProcessor.UniqueLabourSpeech);
            float nCat = naive_Bayes.SummateUniqueWords(fileProcessor.UniqueLabourSpeech, fileProcessor.UniqueConservativeSpeech, fileProcessor.UniqueCoalitionSpeech);
            Dictionary<string, int> mergeTable = naive_Bayes.MergedFrequencyTable(fileProcessor.WordFrequencyList(fileProcessor.Speeches, 0), fileProcessor.WordFrequencyList(fileProcessor.Speeches, 1));

            List<Word> wordTable = naive_Bayes.WordTable(nCat, nWords, mergeTable);
            //TODO Classification done.
            foreach(Word word in wordTable)
            {
                Console.WriteLine(word.Value, word.Frequency, word.Probability);
            }
        }
    }
}