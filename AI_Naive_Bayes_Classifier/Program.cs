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

            Dictionary<string, int> blah = fileProcessor.WordFrequencyList(fileProcessor.Speeches, 0);
            Dictionary<string, int> blahblah = fileProcessor.WordFrequencyListPlusOne(fileProcessor.Speeches, 0);
            
        }
    }
}
