using System;
using System.Collections.Generic;
namespace AI_Naive_Bayes_Classifier
{
    public class UserInterface
    {
        public static int NUMOFCONSERVATIVEFILES;
        public static int NUMOFLABOURFILES;
        public static int NUMOFCOALITIONFILES;
        public static int NUMOFTRAININGFILES;

        List<List<string>> filepathList = new List<List<string>>();
        public List<List<string>> FilePathList { get => filepathList; set => filepathList = value; }

        public List<List<string>> GetTrainingFiles()
        {
            //Introduce the program to user
            List<string> labourFilePaths = new List<string>();
            FilePathList.Add(labourFilePaths);

            List<string> conservativeFilePaths = new List<string>();
            FilePathList.Add(conservativeFilePaths);

            List<string> coalitionFilePaths = new List<string>();
            FilePathList.Add(coalitionFilePaths);

            List<string> trainingFilePaths = new List<string>();
            FilePathList.Add(trainingFilePaths);

            Console.WriteLine("Welcome to the Naive Bayes training user interface.");
            Console.WriteLine("Please specify the files below in which you would like to train the AI with.");


            Console.WriteLine("How many Labour files would you like to train on?");

            NUMOFLABOURFILES = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How many Conservative files would you like to train on?");

            NUMOFCONSERVATIVEFILES = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How many Coalition files would you like to train on?");

            NUMOFCOALITIONFILES = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How many files would you like to have classified?");

            NUMOFTRAININGFILES = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < NUMOFLABOURFILES; i++)
            {
                Console.WriteLine("Enter Labour filepath {0}:", i);
                FilePathList[0].Add(Console.ReadLine());
            }

            for (int i = 0; i < NUMOFCONSERVATIVEFILES; i++)
            {
                Console.WriteLine("Enter Conservative filepath {0}:", i);
                FilePathList[1].Add(Console.ReadLine());
            }

            for (int i = 0; i < NUMOFCOALITIONFILES; i++)
            {
                Console.WriteLine("Enter Coalition filepath {0}:", i);
                FilePathList[2].Add(Console.ReadLine());
            }

            for (int i = 0; i < NUMOFTRAININGFILES; i++)
            {
                Console.WriteLine("Enter a training filepath {0}:",i);
                FilePathList[3].Add(Console.ReadLine());
            }

            return FilePathList;
        }
      
    }
}
