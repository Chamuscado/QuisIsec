using System;
using System.Collections.Generic;
using System.Linq;

namespace QuisIsec
{
    public class Question : IComparable<Question>, IEquatable<Question>
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

        public string RightAnswer { get; }
        public string[] OthersAnswer { get; }
        public string Quest { get; }
        public string Category { get; }

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

        public bool Equals(Question other)
        {
            if (other == null) return false;
            var firstNotSecond = OthersAnswer.Except(other.OthersAnswer).ToList();
            var secondNotFirst = other.OthersAnswer.Except(OthersAnswer).ToList();

            return string.Compare(RightAnswer, other.RightAnswer, StringComparison.InvariantCultureIgnoreCase) == 0
                   && !firstNotSecond.Any() && !secondNotFirst.Any()
                   && string.Compare(Quest, other.Quest, StringComparison.InvariantCultureIgnoreCase) == 0
                   && string.Compare(Category, other.Category, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Question) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (RightAnswer != null ? RightAnswer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OthersAnswer != null ? OthersAnswer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Quest != null ? Quest.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Category != null ? Category.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}