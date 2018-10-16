using System;
using System.IO;
using System.Collections.Generic;
using System.Linq; 
using System.Text.RegularExpressions;

namespace AI_Naive_Bayes_Classifier
{
    public class FileProcessor
    {
        private readonly char[] blackListChars = { ' ', ',', ':', '.', ';', '\t', '\n', '\r' };
        private readonly List<List<string>> speeches = new List<List<string>>();
        private List<List<string>> labourSpeech = new List<List<string>>();
        private List<List<string>> conservativeSpeech = new List<List<string>>();
        private List<List<string>> coalitionSpeech = new List<List<string>>();

        char[] BlackListChars { get => blackListChars; }
        public List<List<string>> Speeches { get => speeches; }
        public List<List<string>> LabourSpeech { get => labourSpeech; set => labourSpeech = value; }
        public List<List<string>> ConservativeSpeech { get => conservativeSpeech; set => conservativeSpeech = value; }
        public List<List<string>> CoalitionSpeech { get => coalitionSpeech; set => coalitionSpeech = value; }

        public FileProcessor(string[] filePath)
        {
            for (int i = 0; i < 5; i++)
            {
                List<string> speech = new List<string>();
                if (filePath[i] != null && File.Exists(filePath[i]))
                {
                    //Get all text from file parsed
                    using (StreamReader reader = new StreamReader(filePath[i]))
                    {
                        // Read entire text file with ReadToEnd.
                        string[] contents = reader.ReadToEnd().Split(blackListChars);

                        foreach (var text in contents)
                        {
                            speech.Add(text);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The " + i + " filepath not found.");
                }
                Speeches.Add(SanatizeText(speech, GetBlackListWords("stopwords.txt")));

            }

        }

        public void ToUniqueWords(List<List<string>> Speeches)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < 2)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        List<string> parserList = speeches[i].Distinct().ToList();
                        LabourSpeech.Add(parserList);
                    }
                }

                if(i >= 2 && i < 4)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        List<string> parserList = speeches[i].Distinct().ToList();
                        ConservativeSpeech.Add(parserList);
                    }
                }

                if (i == 4)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        List<string> parserList = speeches[i].Distinct().ToList();
                        CoalitionSpeech.Add(parserList);
                   }
                }
            }
        }

        public Dictionary<string, int> CountUniqueWords(List<List<string>> speeches, int index)
        {
            Dictionary<string, int> uniqueWordSpeech = new Dictionary<string, int>();
            var uniqueList = speeches[index].GroupBy(word => word)
                                            .Select(g => new { Value = g.Key, Count = g.Count() });
            
            foreach (var x in uniqueList)
            {
                Console.WriteLine("Value: " + x.Value + " Count: " + x.Count);
                uniqueWordSpeech.Add(x.Value, x.Count);

            }
            return uniqueWordSpeech;
        }

        public string[] GetBlackListWords(string blacklistfilepath)
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
    }
}
