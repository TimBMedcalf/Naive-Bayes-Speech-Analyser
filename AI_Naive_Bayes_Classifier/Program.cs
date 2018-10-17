using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInterface = new UserInterface();
            var fileProcessor = new FileProcessor(userInterface.TrainingPaths());

            Dictionary<string, int> blah = fileProcessor.WordFrequencyList(fileProcessor.Speeches, 0);
            Dictionary<string, int> blahblah = fileProcessor.WordFrequencyListPlusOne(fileProcessor.Speeches, 0);

        }
    }
}
