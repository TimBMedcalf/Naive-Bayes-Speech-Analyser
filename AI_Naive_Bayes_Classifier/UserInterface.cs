using System;
namespace AI_Naive_Bayes_Classifier
{
    public class UserInterface
    {
        private string[] filepaths = new string[5];

        public string[] Filepaths { get => filepaths; set => filepaths = value; }

        public string[] TrainingPaths()
        {
            //Introduce the program to user
            Console.WriteLine("Welcome to the Naive Bayes training user interface.");
            Console.WriteLine("Please specify the files below in which you would like to train the AI with.");


            //Loop through the file path and store each one into an array of filepaths
            for (int i = 0; i < 5; i++){
                if(i <= 1) {
                    Console.WriteLine("Labour training file " + i + ":");
                    filepaths[i] = Console.ReadLine();
                }
                else if (i > 1 && i <= 3) {
                    Console.WriteLine("Conservative training file " + i + ":");
                    filepaths[i] = Console.ReadLine();
                }
                else if(i == 4) {
                    Console.WriteLine("Coalition training file " + i + ":");
                    filepaths[i] = Console.ReadLine();
                }

            }
            return Filepaths;
        }
       

    }
}
