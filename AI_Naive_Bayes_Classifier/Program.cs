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

            //foreach(var words in fileProcessor.ConservativeSpeech[0])
            //{
            //    Console.WriteLine(words);
            //}

        }
    }
}
