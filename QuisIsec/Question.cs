using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuisIsec
{
    public class Question : IComparable<Question>
    {
        public List<string> Answers
        {
            get
            {
                var list = new List<string> {RightAnswer};
                list.AddRange(OthersAnswer);
                list.Shuffle();
                return list;
            }
        }

        public string RightAnswer { get; private set; }
        public string[] OthersAnswer { get; private set; }
        public string Quest { get; private set; }
        public string Category { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="category">categoria da pregunta</param>
        /// <param name="quest">pregunta</param>
        /// <param name="answer">lista de respostas em que a primeira é a resposta certa</param>
        public Question(string category, string quest, List<string> answer)
        {
            Category = category;
            Quest = quest;
            if (answer.Count > 0)
            {
                RightAnswer = answer[0];
                answer.RemoveAt(0);
            }

            OthersAnswer = answer.ToArray();
        }

        public int CompareTo(Question other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var rightAnswerComparison = string.Compare(RightAnswer, other.RightAnswer, StringComparison.Ordinal);
            if (rightAnswerComparison != 0) return rightAnswerComparison;
            return string.Compare(Quest, other.Quest, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return Quest;
        }
    }
}