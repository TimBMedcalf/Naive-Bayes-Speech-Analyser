using System;
using System.Collections.Generic;

namespace AI_Naive_Bayes_Classifier
{
    public class Naive_Bayes
    {
        FileProcessor fileProcessor;
        private List<string[,,]> speechTable = new List<string[,,]>();
        public List<string[,,]> SpeechTable { get => speechTable; set => speechTable = value; }

        public List<string[,,]> MakeTable(List<List<string>> speeches, int index)
        {
            SpeechTable[0][0,0,] = fileProcessor.CountUniqueWords(speeches, index);

        }
    }
}
