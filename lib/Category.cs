using System;
using System.Collections.Generic;

namespace lib
{
    [Serializable]
    public class Category : IComparable<Category>
    {
        public string Name { get; }
        public List<Question> Questions { get; }

        public Category(string name)
        {
            Name = name;
            Questions = new List<Question>();
        }

        public void AddQuestion(Question quest)
        {
            Questions.Add(quest);
        }

        public int CompareTo(Category other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"{Name}[{Questions.Count}]";
        }
    }
}