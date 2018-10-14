using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AI_Naive_Bayes_Classifier
{
    public class FileProcessor
    {
        private readonly char[] blackListChars = { ' ', ',', ':', '.', ';', '\t', '\n', '\r' };
        public char[] BlackListChars { get => blackListChars; }
        public readonly List<List<string>> speeches = new List<List<string>>();

        private List<string> speech = new List<string>();

        public FileProcessor(string[] filePath)
        {
            for (int i = 0; i < 5; i++)
            {
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
                speeches.Add(BlackListWordsMatch(speech, GetBlackListWords("stopwords.txt")));
            }
        }

        internal void CountWords(List<string> text)
        {
            throw new NotImplementedException();
        }

        public string[] GetBlackListWords(string blacklistfilepath)
        {
            if (File.Exists(blacklistfilepath))
            {
                using (StreamReader reader = new StreamReader(blacklistfilepath))
                {
                    string[] blackListWords = reader.ReadToEnd().Split(' ');
                    return blackListWords;
                }
            }
            return null;
        }

        //Takes a list of speed and the blacklisted words compares and matches the two variables then returns sanatized text
        public List<string> BlackListWordsMatch(List<string> speech, string[] blackListWords)
        {
            for (int i = 0; i < blackListWords.Length; i++)
            {
                speech.RemoveAll(words => words == blackListWords[i]);
            }
            return speech;
        }
    }
}
