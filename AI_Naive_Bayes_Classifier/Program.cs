using System;

namespace AI_Naive_Bayes_Classifier
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInterface = new UserInterface();
            var fileProcessor = new FileProcessor(userInterface.TextPaths());
        }
    }
}
