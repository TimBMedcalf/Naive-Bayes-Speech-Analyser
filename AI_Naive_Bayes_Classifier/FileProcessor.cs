using System;
using System.IO;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    public class FileProcessor
    {
        private readonly char[] blackListChars = {' ', ',',':','.', ';','\t','\n', '\r'};

        public char[] BlackListChars { get => blackListChars; }

        private List<string> speech = new List<string>();

        public FileProcessor(string[] filePath)
        {
            for (int i = 0; i < 5; i++) 
            {
                if (filePath[i] != null)
                {
                    if(File.Exists(filePath[i]))
                    {
                        //Get all text from file parsed
                        using (StreamReader reader = new StreamReader(filePath[i]))
                        {
                            // Read entire text file with ReadToEnd.
                            string[] contents = reader.ReadToEnd().Split(blackListChars);
                            foreach(var text in contents)
                            {
                                for()
                                Word[] words = new Word[](text, 1);
                            }
                            
                        }
                    }
                }
            }
        }

        public void WordCounter (string text)
        {
            
        }

    }
}
