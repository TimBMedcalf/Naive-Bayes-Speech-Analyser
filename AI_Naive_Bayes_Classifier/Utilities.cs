using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    public class Utilities
    {
        UserInterface userInterface = new UserInterface();
        readonly FileProcessor fileProcessor = new FileProcessor();
        readonly Naive_Bayes naive_Bayes = new Naive_Bayes();

        double labourProbability;
        double conservativeProbability;
        double coalitionProbability;

        public void Startup()
        {
            fileProcessor.ProcessSpeeches(userInterface.GetTrainingFiles());
            Train();
        }
        //Pass the training files into the training function

        public void Train()
        {
            //Sets the training speeches to be trained
            naive_Bayes.TrainedLabour = TrainLabour();
            naive_Bayes.TrainedConservative = TrainConservative();
            naive_Bayes.TrainedCoalition = TrainCoalition();
            Classify();
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

        /// <summary>
        /// Returns a dictionairy of the labour speech ready to be used to classify against
        /// </summary>
        /// <returns>Dictionairy of a speech</returns>
        List<Word> TrainLabour()
        {
            float nWords = naive_Bayes.SummateWords(fileProcessor.LabourSpeeches);

            float nCat = naive_Bayes.SummateUniqueWords(fileProcessor.UniqueLabourSpeech,
                                                        fileProcessor.UniqueConservativeSpeech,
                                                        fileProcessor.UniqueCoalitionSpeech);

            Dictionary<string, int> mergeTable = fileProcessor.MergeFrequencyTables(fileProcessor.LabourTables);

            List<Word> wordTable = naive_Bayes.WordTable(nCat, nWords, mergeTable);

            return wordTable;
        }

        /// <summary>
        /// Returns a dictionairy of the labour speech ready to be used to classify against
        /// </summary>
        /// <returns>Dictionairy of a speech</returns>
        List<Word> TrainConservative()
        {
            float nWords = naive_Bayes.SummateWords(fileProcessor.ConservativeSpeeches);

            float nCat = naive_Bayes.SummateUniqueWords(fileProcessor.UniqueLabourSpeech,
                                                        fileProcessor.UniqueConservativeSpeech,
                                                        fileProcessor.UniqueCoalitionSpeech);

            Dictionary<string, int> mergeTable = fileProcessor.MergeFrequencyTables(fileProcessor.ConservativeTables);

            List<Word> wordTable = naive_Bayes.WordTable(nCat, nWords, mergeTable);

            return wordTable;
        }

        /// <summary>
        /// Returns a dictionairy of the labour speech ready to be used to classify against
        /// </summary>
        /// <returns>Dictionairy of a speech</returns>
        List<Word> TrainCoalition()
        {
            float nWords = naive_Bayes.SummateWords(fileProcessor.CoalitionSpeeches);

            float nCat = naive_Bayes.SummateUniqueWords(fileProcessor.UniqueLabourSpeech,
                                                        fileProcessor.UniqueConservativeSpeech,
                                                        fileProcessor.UniqueCoalitionSpeech);

            Dictionary<string, int> mergeTable = fileProcessor.MergeFrequencyTables(fileProcessor.CoalitionTables);

            List<Word> wordTable = naive_Bayes.WordTable(nCat, nWords, mergeTable);

            return wordTable;
        }
    }
}
