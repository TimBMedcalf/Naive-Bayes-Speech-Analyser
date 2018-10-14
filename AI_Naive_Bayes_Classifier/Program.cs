using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    class Program
    {
        private readonly List<FileProcessor> speeches = new List<FileProcessor>();

        public List<FileProcessor> Speeches { get => speeches; }

        static void Main(string[] args)
        {
            var userInterface = new UserInterface();
            var fileProcessor = new FileProcessor(userInterface.TextPaths());


            foreach(var text in fileProcessor.speeches)
            {
                fileProcessor.CountWords(text);
            }
        }
    }
}
