using System;
namespace AI_Naive_Bayes_Classifier
{
    public class UserInterface
    {
        private string[] filepaths = new string[5];

        public string[] Filepaths { get => filepaths; set => filepaths = value; }

        public string[] TextPaths()
        {
            //Introduce the program to user
            Console.WriteLine("Welcome to the Naive Bayes training user interface.");
            Console.WriteLine("Please specify the files below in which you would like to train the AI with.");


            //Loop through the file path and store each one into an array of filepaths
            for (int i = 0; i < 5; i++){
                Console.WriteLine("File path " + i + ":");
                filepaths[i] = Console.ReadLine();
            }
            return Filepaths;
        }


    }
}
