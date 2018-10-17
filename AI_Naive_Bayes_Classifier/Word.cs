using System;
namespace AI_Naive_Bayes_Classifier
{
    public struct Word
    {
        private string value;
        private float probability;
        private int frequency;

        public string Value { get => value; set => this.value = value; }
        public float Probability { get => probability; set => probability = value; }
        public int Frequency { get => frequency; set => frequency = value; }

        public Word(string word, int occurance, float chance)
        {
            value = word;
            frequency = occurance;
            probability = chance;
        }
    }
}
