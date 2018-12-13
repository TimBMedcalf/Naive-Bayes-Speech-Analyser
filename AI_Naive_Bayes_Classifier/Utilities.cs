using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    public class Utilities
    {
        readonly UserInterface userInterface = new UserInterface();
        readonly FileProcessor fileProcessor = new FileProcessor();
        readonly Naive_Bayes naive_Bayes = new Naive_Bayes();

        double labourProbability;
        double conservativeProbability;
        double coalitionProbability;

        public static bool PRETRAINED;
        public static float N_CAT;
        public static float LABOUR_WORDS;
        public static float CONSERVATIVE_WORDS;
        public static float COALITION_WORDS;

        public void Startup()
        {
            Console.WriteLine("Welcome to the Naive Bayes classifying user interface.");
            Console.WriteLine("Would you like to use pretrained files or use your own training files?");
            Console.WriteLine("Enter 1 to use pre-trained, Enter 2 to use your own files");

            int userInput = Convert.ToInt32(Console.ReadLine());

            if (userInput == 1)
            {
                PRETRAINED = true;
                fileProcessor.ProcessSpeeches(userInterface.PretrainedFile());
            }
            else if (userInput == 2)
            {
                PRETRAINED = false;
                fileProcessor.ProcessSpeeches(userInterface.GetTrainingFiles());
            }

            Train(PRETRAINED);
        }

        //Pass the training files into the training function
        public void Train(bool pretrained)
        {
            if (pretrained)
            {
                naive_Bayes.TrainedLabour = UserInterface.TRAINEDLABOUR;
                naive_Bayes.TrainedConservative = UserInterface.TRAINEDCONSERVATIVE;
                naive_Bayes.TrainedCoalition = UserInterface.TRAINEDCOALITION;
                Classify("pretrained");
            }
            else
            {
                GetNcat(false);
                naive_Bayes.TrainedLabour = TrainLabour();
                naive_Bayes.TrainedConservative = TrainConservative();
                naive_Bayes.TrainedCoalition = TrainCoalition();
                Classify();
            }
        }

        /// <summary>
        /// Creates new training from specified filepaths.
        /// </summary>
        public void CreateTrainingFiles()
        {
            naive_Bayes.TableToFile(TrainLabour(), "labour");
            naive_Bayes.TableToFile(TrainConservative(), "conservative");
            naive_Bayes.TableToFile(TrainCoalition(), "coalition");
        }

        /// <summary>
        /// Calcualtes the amount of unique words across the speeches
        /// </summary>
        /// <returns>The amount of unique words across all categorys</returns>
        public void GetNcat(bool pretrained)
        {
            if (pretrained)
            {
                float uniqueWords = naive_Bayes.TrainedLabour.Count +
                 naive_Bayes.TrainedConservative.Count +
                 naive_Bayes.TrainedCoalition.Count;
                N_CAT = uniqueWords;
            }
            else
            {
                float uniqueWords = naive_Bayes.SummateUniqueWords(fileProcessor.UniqueLabourSpeech,
                 fileProcessor.UniqueConservativeSpeech,
                 fileProcessor.UniqueCoalitionSpeech);
                N_CAT = uniqueWords;

            }
        }

        public void Classify()
        {
            //Loops through each speech that needs to be classified then passes the data through a classifier to give classification
            foreach (var speech in fileProcessor.ClassifySpeech)
            {
                labourProbability = naive_Bayes.Classify(naive_Bayes.TrainedLabour,
                 speech,
                 fileProcessor.UniqueLabourSpeech.Count,
                 fileProcessor.UniqueConservativeSpeech.Count,
                 fileProcessor.UniqueCoalitionSpeech.Count,
                 "labour");
                Console.WriteLine("Labour probility: " + labourProbability);


                conservativeProbability = naive_Bayes.Classify(naive_Bayes.TrainedConservative,
                 speech,
                 fileProcessor.UniqueLabourSpeech.Count,
                 fileProcessor.UniqueConservativeSpeech.Count,
                 fileProcessor.UniqueCoalitionSpeech.Count,
                 "conservative");
                Console.WriteLine("Conservative probility: " + conservativeProbability);


                coalitionProbability = naive_Bayes.Classify(naive_Bayes.TrainedCoalition,
                 speech,
                 fileProcessor.UniqueLabourSpeech.Count,
                 fileProcessor.UniqueConservativeSpeech.Count,
                 fileProcessor.UniqueCoalitionSpeech.Count,
                 "coalition");
                Console.WriteLine("Coalition probility: " + coalitionProbability);

                //Determines which party was in power by checking the probability against each other.
                if (coalitionProbability < labourProbability && coalitionProbability < conservativeProbability)
                {
                    Console.WriteLine("This speech is most likely reffering to a coalition government");
                }

                if (labourProbability < coalitionProbability && labourProbability < conservativeProbability)
                {
                    Console.WriteLine("This speech is most likely reffering to a labour government");
                }

                if (conservativeProbability < coalitionProbability && conservativeProbability < labourProbability)
                {
                    Console.WriteLine("This speech is most likely reffering to a conservative government");
                }

            }
        }

        public void Classify(string pretrained)
        {
            GetNcat(true);
            LABOUR_WORDS = naive_Bayes.TrainedLabour.Count;
            CONSERVATIVE_WORDS = naive_Bayes.TrainedConservative.Count;
            COALITION_WORDS = naive_Bayes.TrainedCoalition.Count;

            //Loops through each speech that needs to be classified then passes the data through a classifier to give classification
            foreach (var speech in fileProcessor.ClassifySpeech)
            {
                labourProbability = naive_Bayes.Classify(naive_Bayes.TrainedLabour,
                 speech,
                 2,
                 2,
                 1,
                 "labour");
                Console.WriteLine("Labour probility: " + labourProbability);


                conservativeProbability = naive_Bayes.Classify(naive_Bayes.TrainedConservative,
                 speech,
                 2,
                 2,
                 1,
                 "conservative");
                Console.WriteLine("Conservative probility: " + conservativeProbability);


                coalitionProbability = naive_Bayes.Classify(naive_Bayes.TrainedCoalition,
                 speech,
                 2,
                 2,
                 1,
                 "coalition");
                Console.WriteLine("Coalition probility: " + coalitionProbability);

                //Determines which party was in power by checking the probability against each other.
                if (coalitionProbability < labourProbability && coalitionProbability < conservativeProbability)
                {
                    Console.WriteLine("This speech is most likely reffering to a coalition government");
                }

                if (labourProbability < coalitionProbability && labourProbability < conservativeProbability)
                {
                    Console.WriteLine("This speech is most likely reffering to a labour government");
                }

                if (conservativeProbability < coalitionProbability && conservativeProbability < labourProbability)
                {
                    Console.WriteLine("This speech is most likely reffering to a conservative government");
                }

            }
        }

        /// <summary>
        /// Returns a dictionairy of the labour speech ready to be used to classify against
        /// </summary>
        /// <returns>Dictionairy of a speech</returns>
        List<Word> TrainLabour()
        {
            LABOUR_WORDS = naive_Bayes.SummateWords(fileProcessor.LabourSpeeches);

            Dictionary<string, int> mergeTable = fileProcessor.MergeFrequencyTables(fileProcessor.LabourTables);

            List<Word> wordTable = naive_Bayes.WordTable(N_CAT, LABOUR_WORDS, mergeTable);

            return wordTable;
        }

        /// <summary>
        /// Returns a dictionairy of the labour speech ready to be used to classify against
        /// </summary>
        /// <returns>Dictionairy of a speech</returns>
        List<Word> TrainConservative()
        {
            CONSERVATIVE_WORDS = naive_Bayes.SummateWords(fileProcessor.ConservativeSpeeches);

            Dictionary<string, int> mergeTable = fileProcessor.MergeFrequencyTables(fileProcessor.ConservativeTables);

            List<Word> wordTable = naive_Bayes.WordTable(N_CAT, CONSERVATIVE_WORDS, mergeTable);

            return wordTable;
        }

        /// <summary>
        /// Returns a dictionairy of the labour speech ready to be used to classify against
        /// </summary>
        /// <returns>Dictionairy of a speech</returns>
        List<Word> TrainCoalition()
        {
            COALITION_WORDS = naive_Bayes.SummateWords(fileProcessor.CoalitionSpeeches);

            Dictionary<string, int> mergeTable = fileProcessor.MergeFrequencyTables(fileProcessor.CoalitionTables);

            List<Word> wordTable = naive_Bayes.WordTable(N_CAT, COALITION_WORDS, mergeTable);

            return wordTable;
        }
    }
}