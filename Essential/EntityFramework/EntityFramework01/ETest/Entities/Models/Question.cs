using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable]
    public partial class Question
    {
        public Question()
        {
        }

        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public int TestId { get; set; }
        public string Description { get; set; }

        [XmlIgnore]
        public virtual Test Test { get; set; }
        public virtual List<AnswerVariant> AnswerVariants { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is Question))
                return false;

            Question a = (Question)obj;
            return Description == a.Description 
                   && AnswerVariants.SequenceEqual(a.AnswerVariants);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ TestId.GetHashCode() ^ Description.GetHashCode() ^ AnswerVariants.GetHashCode();
        }
    }
}
