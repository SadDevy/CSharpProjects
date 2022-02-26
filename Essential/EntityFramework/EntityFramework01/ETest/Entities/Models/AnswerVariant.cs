using System;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable]
    public partial class AnswerVariant
    {
        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public bool IsCorrected { get; set; }

        [XmlIgnore]
        public virtual Question Question { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is AnswerVariant))
                return false;

            AnswerVariant a = (AnswerVariant)obj;
            return Description == a.Description && IsCorrected == a.IsCorrected;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ QuestionId.GetHashCode() ^ Description.GetHashCode() ^ IsCorrected.GetHashCode();
        }
    }
}
