using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Entities
{
    [Serializable]
    public class Test
    {
        [XmlIgnore]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Theory { get; set; }
        public string TheoryUrl { get; set; }
        public byte[] Image { get; set; }
        public bool TheoryIsShown { get; set; }

        public int TestTime { get; set; }
        public int QuestionsCount { get; set; }
        public int RightCount { get; set; }
        public List<Question> Questions { get; set; }
    }
}
