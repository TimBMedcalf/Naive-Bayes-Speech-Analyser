using System;
using System.IO;
using System.Collections.Generic;
using System.Linq; 
using System.Text.RegularExpressions;

namespace AI_Naive_Bayes_Classifier
{
    public class FileProcessor
    {
        readonly char[] blackListChars = { ' ', ',', ':', '.', ';', '\t', '\n', '\r' };
        readonly List<List<string>> speeches = new List<List<string>>();
        List<List<string>> trainingSpeech = new List<List<string>>();
        List<List<string>> uniqueLabourSpeech = new List<List<string>>();
        List<List<string>> uniqueConservativeSpeech = new List<List<string>>();
        List<List<string>> uniqueCoalitionSpeech = new List<List<string>>();
        List<Dictionary<string, int>> labourTables = new List<Dictionary<string, int>>();
        List<Dictionary<string, int>> conservativeTables = new List<Dictionary<string, int>>();
        List<Dictionary<string, int>> coalitionTables = new List<Dictionary<string, int>>();

        char[] BlackListChars { get => blackListChars; }
        public List<List<string>> Speeches { get => speeches; }
        public List<List<string>> ClassifySpeech { get => trainingSpeech; set => trainingSpeech = value; }
        public List<List<string>> UniqueLabourSpeech { get => uniqueLabourSpeech; set => uniqueLabourSpeech = value; }
        public List<List<string>> UniqueConservativeSpeech { get => uniqueConservativeSpeech; set => uniqueConservativeSpeech = value; }
        public List<List<string>> UniqueCoalitionSpeech { get => uniqueCoalitionSpeech; set => uniqueCoalitionSpeech = value; }
        public List<Dictionary<string, int>> LabourTables { get => labourTables; set => labourTables = value; }
        public List<Dictionary<string, int>> ConservativeTables { get => conservativeTables; set => conservativeTables = value; }
        public List<Dictionary<string, int>> CoalitionTables { get => coalitionTables; set => coalitionTables = value; }

        public void ProcessSpeeches(List<List<string>> filepaths)
        {
            foreach(var category in filepaths)
            {
                foreach(var filepath in category)
                {
                    List<string> parseSpeech = new List<string>();

                    if (filepath != null && File.Exists(filepath))
                    {
                        using (StreamReader reader = new StreamReader(filepath))
                        {
                            string[] contents = reader.ReadToEnd().Split(blackListChars);

                            foreach(var text in contents)
                            {
                                parseSpeech.Add(text);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The " + filepath + " filepath was not found.");
                    }
                    Speeches.Add(SanatizeText(parseSpeech, GetBlackListWords("stopwords.txt")));
                }
            }
            ProcessForBayes(speeches);
        }

        void ProcessForBayes(List<List<string>> speech )
        {
            int j = 0;
            int z = 0;
            int y = 0;

            for (int i = 0; i < speech.Count; i++)
            {
                if (i < UserInterface.NUMOFLABOURFILES)
                {
                    //creates a parser list and then passes the list to the unique labour speech file
                    List<List<string>> uniqueSpeech = new List<List<string>>();
                    List<string> parserList = speech[i].Distinct().ToList();
                    UniqueLabourSpeech.Add(parserList);

                    //Create a table with the frequency of the words as the value of the dicitionairy
                    var uniqueList = speeches[i].GroupBy(word => word)
                                                .Select(g => new { Value = g.Key, Count = g.Count() });

                    //adds a table to the tables so when referencing the labourtables[i] it does not call a index out of bounds error
                    labourTables.Add(new Dictionary<string, int>());
                    foreach (var x in uniqueList)
                    {
                        labourTables[j].Add(x.Value, x.Count);
                    }
                    j++;
                }

                if(i >= UserInterface.NUMOFLABOURFILES && i < UserInterface.NUMOFLABOURFILES + UserInterface.NUMOFCONSERVATIVEFILES)
                {
                   
                    List<List<string>> uniqueSpeech = new List<List<string>>();
                    List<string> parserList = speech[i].Distinct().ToList();
                    UniqueConservativeSpeech.Add(parserList);

                    //Create a table with the frequency of the words as the value of the dicitionairy
                    var uniqueList = speeches[i].GroupBy(word => word)
                                                    .Select(g => new { Value = g.Key, Count = g.Count() });

                    conservativeTables.Add(new Dictionary<string, int>());

                    foreach (var x in uniqueList)
                    {
                        conservativeTables[z].Add(x.Value, x.Count);
                    }
                    z++;
                }

                if(i >= UserInterface.NUMOFLABOURFILES + UserInterface.NUMOFCONSERVATIVEFILES &&
                   i < UserInterface.NUMOFLABOURFILES + UserInterface.NUMOFCONSERVATIVEFILES + UserInterface.NUMOFCOALITIONFILES)
                {
                    List<List<string>> uniqueSpeech = new List<List<string>>();
                    List<string> parserList = speech[i].Distinct().ToList();
                    UniqueCoalitionSpeech.Add(parserList);

                    //Create a table with the frequency of the words as the value of the dicitionairy
                    var uniqueList = speeches[i].GroupBy(word => word)
                                                    .Select(g => new { Value = g.Key, Count = g.Count() });

                    coalitionTables.Add(new Dictionary<string, int>());
                    foreach (var x in uniqueList)
                    {
                        coalitionTables[y].Add(x.Value, x.Count);
                    }
                    y++;
                }
                if(i >= UserInterface.NUMOFLABOURFILES + UserInterface.NUMOFCOALITIONFILES + UserInterface.NUMOFCONSERVATIVEFILES)
                {
                    List<List<string>> uniqueSpeech = new List<List<string>>();
                    List<string> parserList = speech[i].Distinct().ToList();
                    ClassifySpeech.Add(parserList);
                }
            }
        }

        string[] GetBlackListWords(string blacklistfilepath)
        {
            if (File.Exists(blacklistfilepath))
            {
                using (StreamReader reader = new StreamReader(blacklistfilepath))
                {
                    string[] blackListWords = reader.ReadToEnd().Split(BlackListChars);
                    return blackListWords;
                }
            }
            return null;
        }

        //Takes a list of speed and the blacklisted words compares and matches the two variables then returns sanatized text
        private List<string> SanatizeText(List<string> speech, string[] blackListWords)
        {
            for (int i = 0; i < blackListWords.Length; i++)
            {
                speech.RemoveAll(words => words == blackListWords[i]);
            }
            return speech;
        }

        public Dictionary<string, int> MergeFrequencyTables(List<Dictionary<string, int>> tableList)
        {
            Dictionary<string, int> mergeTable = new Dictionary<string, int>();

            //Gets the first table as a base table to compare to and set value to 0, as the foreach loop will do the addition of the frequencys
            foreach (KeyValuePair<string, int> entry in tableList[0])
            {
                mergeTable.Add(entry.Key, 0);
            }

            foreach (var table in tableList)
            {
                foreach (KeyValuePair<string, int> entry in table)
                {
                    if (mergeTable.ContainsKey(entry.Key))
                    {
                        mergeTable[entry.Key] += entry.Value;
                    }
                    else
                    {
                        mergeTable.Add(entry.Key, entry.Value);
                    }
                }
            }

            return mergeTable;
        }
    }
}
