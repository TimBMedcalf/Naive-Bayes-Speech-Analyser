using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AI_Naive_Bayes_Classifier
{
    public class FileProcessor
    {
        readonly char[] blackListChars = {
   ' ',
   ',',
   ':',
   '.',
   ';',
   '\t',
   '\n',
   '\r',
   '\'',
   '\\'
  };
        readonly List<List<string>> speeches = new List<List<string>>();
        List<List<string>> trainingSpeech = new List<List<string>>();
        List<List<string>> labourSpeeches = new List<List<string>>();
        List<List<string>> conservativeSpeeches = new List<List<string>>();
        List<List<string>> coalitionSpeeches = new List<List<string>>();
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
        public List<List<string>> LabourSpeeches { get => labourSpeeches; set => labourSpeeches = value; }
        public List<List<string>> ConservativeSpeeches { get => conservativeSpeeches; set => conservativeSpeeches = value; }
        public List<List<string>> CoalitionSpeeches { get => coalitionSpeeches; set => coalitionSpeeches = value; }

        /// <summary>
        /// Processes the speeches. Takes in a list of filepaths specified from the user
        /// </summary>
        /// <param name="filepaths">Filepaths.</param>
        public void ProcessSpeeches(List<List<string>> filepaths)
        {
            foreach (var category in filepaths)
            {
                foreach (var filepath in category)
                {
                    List<string> parseSpeech = new List<string>();

                    if (filepath != null && File.Exists(filepath))
                    {
                        using (StreamReader reader = new StreamReader(filepath))
                        {
                            string[] contents = reader.ReadToEnd().Split(blackListChars);

                            foreach (var text in contents)
                            {
                                parseSpeech.Add(text.ToLower());
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

        /// <summary>
        ///  Processes the files ready to be used from the niave-bayes formula.
        /// </summary>
        /// <param name="speech">Speech.</param>
        void ProcessForBayes(List<List<string>> speech)
        {
            //Creates ints to be used for holding the initial index for the labour tables dictionairy
            int j = 0;
            int z = 0;
            int y = 0;

            for (int i = 0; i < speech.Count; i++)
            {
                if (i < UserInterface.NUMOFLABOURFILES)
                {
                    //Adds a total word list to labour speeches to calculate the summation of all of the words in a category
                    labourSpeeches.Add(speeches[i]);

                    //creates a parser list and then passes the list to the unique labour speech file
                    List<List<string>> uniqueSpeech = new List<List<string>>();
                    List<string> parserList = speech[i].Distinct().ToList();
                    UniqueLabourSpeech.Add(parserList);

                    //Create a table with the frequency of the words as the value of the dicitionairy
                    var uniqueList = speeches[i].GroupBy(word => word)
                     .Select(g => new {
                         Value = g.Key,
                         Count = g.Count()
                     });

                    //adds a table to the tables so when referencing the labourtables[i] it does not call a index out of bounds error
                    labourTables.Add(new Dictionary<string, int>());
                    foreach (var x in uniqueList)
                    {
                        labourTables[j].Add(x.Value, x.Count);
                    }
                    j++;
                }

                //Checks if the for loop is at the correct index before processing the file

                if (i >= UserInterface.NUMOFLABOURFILES && i < UserInterface.NUMOFLABOURFILES + UserInterface.NUMOFCONSERVATIVEFILES)
                {
                    conservativeSpeeches.Add(speeches[i]);
                    List<List<string>> uniqueSpeech = new List<List<string>>();
                    List<string> parserList = speech[i].Distinct().ToList();
                    UniqueConservativeSpeech.Add(parserList);

                    //Create a table with the frequency of the words as the value of the dicitionairy
                    var uniqueList = speeches[i].GroupBy(word => word)
                     .Select(g => new {
                         Value = g.Key,
                         Count = g.Count()
                     });

                    conservativeTables.Add(new Dictionary<string, int>());

                    foreach (var x in uniqueList)
                    {
                        conservativeTables[z].Add(x.Value, x.Count);
                    }
                    z++;
                }

                //Checks if the for loop is at the correct index before processing the file
                if (i >= UserInterface.NUMOFLABOURFILES + UserInterface.NUMOFCONSERVATIVEFILES &&
                 i < UserInterface.NUMOFLABOURFILES + UserInterface.NUMOFCONSERVATIVEFILES + UserInterface.NUMOFCOALITIONFILES)
                {
                    coalitionSpeeches.Add(speeches[i]);
                    List<List<string>> uniqueSpeech = new List<List<string>>();
                    List<string> parserList = speech[i].Distinct().ToList();
                    UniqueCoalitionSpeech.Add(parserList);

                    //Create a table with the frequency of the words as the value of the dicitionairy
                    var uniqueList = speeches[i].GroupBy(word => word)
                     .Select(g => new {
                         Value = g.Key,
                         Count = g.Count()
                     });

                    coalitionTables.Add(new Dictionary<string, int>());
                    foreach (var x in uniqueList)
                    {
                        coalitionTables[y].Add(x.Value, x.Count);
                    }
                    y++;
                }
                if (i >= UserInterface.NUMOFLABOURFILES + UserInterface.NUMOFCOALITIONFILES + UserInterface.NUMOFCONSERVATIVEFILES)
                {
                    List<List<string>> uniqueSpeech = new List<List<string>>();
                    List<string> parserList = speech[i].Distinct().ToList();
                    ClassifySpeech.Add(parserList);
                }
            }
        }

        //this returns a list of string of the stop words for comparison against the speeches
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


        // Put all of table one in merge table then compare merge table with containskey with table two
        // if it contains the entry.key then add the key into the merge table while adding up the frequencies
        //if it does not contain then add the entry.value and entry.key into the table
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