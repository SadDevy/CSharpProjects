using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Entities
{
    [Serializable]
    public class Question
    {
        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public int TestId { get; set; }
        public string Description { get; set; }

        public List<AnswerVariant> AnswerVariants { get; set; }
    }
}
