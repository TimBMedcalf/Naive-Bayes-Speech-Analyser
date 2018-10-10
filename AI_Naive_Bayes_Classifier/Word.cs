using System;
namespace AI_Naive_Bayes_Classifier
{
    public class Word
    {
        private string text;
        private int reps;

        public string Text { get => text; set => text = value; }
        public int Reps { get => reps; set => reps = value; }

        public Word(string word, int repititions)
        {
            Text = word;
            Reps = repititions;
        }
    }
}
