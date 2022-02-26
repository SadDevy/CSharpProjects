using System;
using System.Xml.Serialization;

namespace Entities
{
    [Serializable]
    public class AnswerVariant
    {
        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public bool IsCorrected { get; set; }
    }
}
