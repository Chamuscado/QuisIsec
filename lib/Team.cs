using System;
using System.Drawing;

namespace lib
{
    [Serializable]
    public class Team
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public Color @Color { get; set; }
        public Answer @Answer { get; set; }
    }
}